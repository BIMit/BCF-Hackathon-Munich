angular.module('myApp', ['ngCookies', 'ngRoute'])
 	.config(function($locationProvider) {
		$locationProvider.html5Mode(true);
	})
	.value('BaseUrl', 'http://bim--it-dev.iabi.biz');