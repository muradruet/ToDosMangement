(function () {

    'use strict';

    angular.module('app').service('dataService', dataService);

    dataService.$inject = ['$state'];

    function dataService($state) {
        function allTasks() {
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

        function allTasks() {
            
            $http.get('http://localhost:3000/api/tasks').then(
                function (result) {
                    return result.data;
                },
                function (error) {
                    console.log(error);
                });
            };
           

        return {
            allTasks: allTasks
        }
    }
})();
