angular.module('myApp')
    .controller('ProjectListCtrl', function($scope, $http, $cookies, $routeParams, $location, BaseUrl) {

    //var baseUrl = 'http://bim--it-dev.iabi.biz';

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
                    "url": BaseUrl + '/bcf/1.0/projects',
                    "headers" : { "Authorization" : "Bearer " + token.access_token }})
                    .success(function(data) {
                        $scope.projects = data;
                        $scope.baseUrl = BaseUrl;
                    });
            });
    }
    else {
        var InfoUrl = BaseUrl + '/bcf/auth';
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
})
