/**
 * Created by sauter on 12/7/15.
 */
 
 angular.module('myApp', ['ngCookies', 'ngRoute'])
 .config(function($locationProvider) {
	 $locationProvider.html5Mode(true);	 
	})
	 .config(function($routeProvider) {
		 $routeProvider

			 // route for the home page
			 .when('/', {
				 templateUrl : 'partials/ProjectList.html',
				 controller  : 'ProjectCtrl'
			 })

			 // route for the about page
			 .when('/topics', {
				 templateUrl : 'partials/TopicList.html',
				 controller  : 'TopicCtrl'
			 })

			 // route for the contact page
			 .when('/comments', {
				 templateUrl : 'partials/CommentList.html',
				 controller  : 'CommentCtrl'
			 });
	 })
	.controller('ProjectCtrl', function($scope, $http, $cookies, $routeParams, $location ) {
		//$http.defaults.headers.post["Content-Type"] = "text/plain";
		
		var baseUrl = 'http://bim--it-dev.iabi.biz';
		
		if ($location.search().code) {
			// Exchange code for token
			var client = $cookies.getObject("client");
			var endpoints = $cookies.getObject("endpoints");
			var base64ClientAuthorization = btoa(client.client_id + ":" + client.client_secret);
			$http.defaults.headers.common.Authorization = 'Basic ' + base64ClientAuthorization;
			$http.post(endpoints.oauth2_token_url, "?grant_type=authorization_code&code=" + $location.search().code)
			.then(function(response) {
				// Save token to cookie	
				var token = response.data;
				$cookies.putObject("token", token);
				
				$http({ "method" : "GET",
				"url": baseUrl + '/bcf/1.0/projects',
							"headers" : { "Authorization" : "Bearer " + token.access_token }})
				.success(function(data) {
					$scope.projects = data;
					console.log(data);
				});
				
				console.log($cookies.getObject('token'));
			});		
		}
		else {
		var InfoUrl = baseUrl + '/bcf/auth';
		 $http.get(InfoUrl)
		 .then(function(response) {
			 $cookies.putObject("endpoints", response.data);
			 $scope.serverEndpoints = response.data;
			 var DynRegEndpoint = response.data.oauth2_dynamic_client_reg_url;	 
			 // POST client data
			 var ClientRegistration = {
				 "client_name": "Team JavaScript Client",
				 "client_description": "Awesome client",
				 "client_url":"http://www.example.com",
				 "redirect_url":"http://localhost:8000/app/"
			 };
			 $http.post(DynRegEndpoint, ClientRegistration).then(function(response) {
				 $cookies.putObject("client", response.data);
				 $scope.client = $cookies.getObject("client");	
					
				var redirectUrl = $scope.serverEndpoints.oauth2_auth_url + '?response_type=code'
				+ '&client_id=' + $scope.client.client_id
				+ '&state=D98F9B4F-5B0E-4948-B8B5-59F4FE23B8E0';
				window.location = redirectUrl;
				
			 });	 
		 });
		}
	});
 
