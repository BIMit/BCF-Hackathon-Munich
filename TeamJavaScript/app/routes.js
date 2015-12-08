angular.module('myApp')
    .config(function($routeProvider) {
        $routeProvider

            // route for the home page
            .when('/', {
                templateUrl : 'partials/ProjectList.html',
                controller  : 'ProjectCtrl'
            })

            // route for the topics page
            .when('/topics', {
                templateUrl : 'partials/TopicList.html',
                controller  : 'TopicCtrl'
            })

            // route for the comments page
            .when('/comments', {
                templateUrl : 'partials/CommentList.html',
                controller  : 'CommentCtrl'
            })

            // route for the viewpoints page
            .when('/viewpoints', {
                templateUrl : 'partials/ViewpointList.html',
                controller  : 'ViewpointCtrl'
            })

            // route for the extension detail page
            .when('/extension', {
                templateUrl : 'partials/ExtensionDetail.html',
                controller  : 'ExtensionDetailCtrl'
            });
    });