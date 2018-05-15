(function () {

    'use strict';

    angular.module('app').service('authService', authService);

    authService.$inject = ['$state', 'angularAuth0', '$timeout', '$http'];

    function authService($state, angularAuth0, $timeout, $http) {
        function login() {
            angularAuth0.authorize();
        }

        function handleAuthentication() {
            angularAuth0.parseHash(function (err, authResult) {
                    if (err) {
                        console.log(err);
                    }
                    if (authResult && authResult.accessToken && authResult.idToken) {
                        console.log(authResult);
                        setUserSession(authResult);
                        $timeout(function () {
                            $state.go('home');
                        });
                    }
                });
        }

        function logout() {
            localStorage.removeItem('access_token');
            localStorage.removeItem('id_token');
            localStorage.removeItem('expires_at');
        }

        function setUserSession(authResult) {
            localStorage.setItem('access_token', authResult.accessToken);
            localStorage.setItem('id_token', authResult.idToken);
            var expiresAt = JSON.stringify(
                authResult.expiresIn * 1000 + new Date().getTime()
            );
            localStorage.setItem('expires_at', expiresAt);
        }

        function isAuthenticated() {
            var expiresAt = JSON.parse(localStorage.getItem('expires_at'));
            return new Date().getTime() < expiresAt;
        }

        function allStatus() {
            $http.get(baseUrl+'/api/status').then(
                function (result) {
                    return result.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        }

        return {
            login: login,
            handleAuthentication: handleAuthentication,
            logout: logout,
            isAuthenticated: isAuthenticated,
            allStatus: allStatus
        }
    }
})();
