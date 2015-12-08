angular.module('myApp')
    .controller('ProjectCtrl', function($scope, $http, $cookies, $routeParams, $location, BaseUrl) {

        $scope.project = $routeParams;

        $http.defaults.headers.common.Authorization = 'Bearer ' + $cookies.getObject('token').access_token;

        $http.get(BaseUrl + '/bcf/1.0/projects/' + $routeParams.project_id + '/topics')
        .success(function(data) {
            $scope.topics = data;
        })
        .error(function(data) {
            $scope.error = data;
        });

    });
