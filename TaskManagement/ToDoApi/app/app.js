(function () {

    'use strict';

    angular
        .module('app', ['auth0.auth0',
            'angular-jwt',
            'ui.router',
            'angularjs-datetime-picker',
            'multipleSelect'])
        .config(config);

    config.$inject = ['$stateProvider',
        '$locationProvider',
        '$httpProvider',
        'angularAuth0Provider',
        '$urlRouterProvider',
        'jwtOptionsProvider'
    ];

    function config($stateProvider,
        $locationProvider,
        $httpProvider,
        angularAuth0Provider,
        $urlRouterProvider,
        jwtOptionsProvider) {

        $stateProvider
            .state('home', {
                url: '/',
                controller: 'HomeController',
                templateUrl: 'app/home/home.html',
                controllerAs: 'vm'
            })
            .state('callback', {
                url: '/callback',
                controller: 'CallbackController',
                templateUrl: 'app/callback/callback.html',
                controllerAs: 'vm'
            })
            .state('user', {
                url: '/user',
                controller: 'UserController',
                templateUrl: 'app/user/user.html',
                controllerAs: 'vm'
            })
            .state('edittask', {
                url: '/edittask',
                controller: 'EditTaskController',
                templateUrl: 'app/editTask/editTask.html',
                params: { myParam: null },
                controllerAs: 'vm'
            })

        // Initialization for the angular-auth0 library
        angularAuth0Provider.init({
            clientID: '126ZPKWHzyLsERekrEgZutBUP1ZWSDYh',
            domain: 'murad-hossain.au.auth0.com',
            responseType: 'token id_token',
            redirectUri: CallbackURL,
            scope:'openid',
            audience: 'https://murad-hossain.au.auth0.com/api/v2/',
        });

        // Configure a tokenGetter so that the isAuthenticated
        // method from angular-jwt can be used
        jwtOptionsProvider.config({
            tokenGetter: function () {
                return localStorage.getItem('access_token');
            },
            whiteListedDomains: ['localhost','muradruetyahoo-001-site1.ftempurl.com']
        });

        $httpProvider.interceptors.push('jwtInterceptor');

        $urlRouterProvider.otherwise('/');

        // Remove the ! from the hash so that
        // auth0.js can properly parse it
        $locationProvider.hashPrefix('');

        $locationProvider.html5Mode(true);
    }

})();
