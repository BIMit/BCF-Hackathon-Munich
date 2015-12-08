angular.module('myApp')
    .controller('ProjectCtrl', function($scope, $http, $cookies, $routeParams, $location, BaseUrl) {

        $scope.params = $routeParams;

    });
