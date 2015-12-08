angular.module('myApp')
    .config(function($routeProvider) {
        $routeProvider

            // route for the home page
            .when('/', {
                templateUrl : 'partials/ProjectList.html',
                controller  : 'ProjectListCtrl'
            })
            .when('/projects', {
                templateUrl : 'partials/ProjectList.html',
                controller  : 'ProjectListCtrl'
            })

            // route for the project specific pages
            .when('/project/:project_id', {
                templateUrl : 'partials/Project.html',
                controller  : 'ProjectCtrl'
            })

            // route for the specific topic page
            .when('/project/:project_id/topic/:topic_id', {
                templateUrl : 'partials/Topic.html',
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
