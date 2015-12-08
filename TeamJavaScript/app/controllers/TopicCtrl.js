angular.module('myApp')
    .controller('TopicCtrl', function ($scope, $http, $routeParams, $cookies, BaseUrl) {
        $scope.project = { project_id : $routeParams.project_id };
        $scope.topic = { topic_id : $routeParams.topic_id };

        $http.defaults.headers.common.Authorization = 'Bearer ' + $cookies.getObject('token').access_token;

        $http.get(BaseUrl + '/bcf/1.0/projects/' + $routeParams.project_id + '/topics/' + $routeParams.topic_id)
        .success(function(data) {
            $scope.topic = data;
            $scope.topic.topic_id = data.guid;
        })
        .error(function(data) {
            $scope.error = data;
        });


    });
