(function () {

  'use strict';

  angular.module('app').controller('HomeController', homeController);

  homeController.$inject = ['$http', 'authService'];

	function homeController( $http, authService) {
        var vm = this;
        vm.auth = authService;
        vm.taskMessage = '';

    vm.getTasts = function () {
        debugger;
        $http.get('http://localhost:3000/api/tasks').then(
            function (result) {
                vm.taskMessage = result.data;
            },
            function (error) {
                console.log(error);
            }
        );
    };
    

  }

})();