(function () {

    'use strict';

    angular.module('app').controller('UserController', userController);

    userController.$inject = ['$http', 'authService', '$state'];

    function userController($http, authService, $state) {
        var vm = this;
        vm.auth = authService;
        if (!vm.auth.isAuthenticated()) {
            $state.go('home');
        }
        vm.userMessage = 'This is user controller';
        vm.error ={
            name: false,
            email:false
        };
        vm.user = {
            name: '',
            email: ''
        };

        vm.CancelAddUser = function () {

            $state.go('home');
        }

        vm.AddUser = function () {
            vm.error.name = !vm.user.name ? true : false;
            vm.error.email = !vm.user.email ? true : false;

            var isAnyError = testAllProperties(vm.error, false);
            if (isAnyError === false)
                vm.setUser();
        }

        function testAllProperties(obj, val) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (obj[key] !== val) {
                        return true;
                    }
                }
            }
            return false;
        }

        vm.setUser = function () {
            var url = baseUrl +'/api/users';
            var data = { UserName: vm.user.name, UserEmail: vm.user.email };
            $http.post(url, data).then(
                function (msg) {
                    if (msg.status === 200) {
                        confirm("User have been created");
                        $state.go('home');
                    }
                },
                function (error) {
                    vm.addUserErrorMessage = error.data;
                    console.log(error);
                });
               
        }
        
    }

})();