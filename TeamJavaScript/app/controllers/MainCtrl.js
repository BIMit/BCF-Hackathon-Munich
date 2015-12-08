angular.module('myApp')
    .controller('MainCtrl', function ($scope, $http, $cookies, $location) {
        // setting the persisted data from the cookie if present
        $scope.BaseUrl = $cookies.get("baseUrl");
        $scope.client = $cookies.getObject("client");
        var token = $cookies.getObject("token");
        $scope.token = token.access_token;
        $scope.refresh_token = token.refresh_token;

        // if authorization_code is passed in from BCF server on controller start -> get a token from it
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
                    $scope.token = token.access_token;
                    $scope.refresh_token = token.refresh_token;
                    $cookies.putObject("token", token);
                    $http({ "method" : "GET",
                        "url": $scope.BaseUrl + '/bcf/1.0/projects',
                        "headers" : { "Authorization" : "Bearer " + token.access_token }})
                        .success(function(data) {
                            $scope.projects = data;
                        });
                });
        }

        // dynamically registers and client and redirects the user to the BCF api OAuth2 grant Url
        $scope.connectToServer = function() {
            var baseUrl = $scope.BaseUrl;
            $cookies.put("baseUrl", baseUrl);
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
        };

        //
        $scope.refreshToken = function() {
            var refreshRequest = "grant_type=refresh_token&refresh_token=" + $scope.refresh_token;
            var refreshTokenUrl = $cookies.getObject("endpoints").oauth2_token_url;
            var base64ClientAuthorization = btoa($scope.client.client_id + ":" + $scope.client.client_secret);
            $http.defaults.headers.common.Authorization = 'Basic ' + base64ClientAuthorization;
            $http.post(refreshTokenUrl, refreshRequest)
                .then(function(response){
                    // Save token to cookie
                    var token = response.data;
                    $scope.token = token.access_token;
                    $scope.refresh_token = token.refresh_token;
                    $cookies.putObject("token", token);
                })
        };
    });