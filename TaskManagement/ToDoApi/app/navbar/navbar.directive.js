(function () {
    angular.module('app').directive('navbar', navbar);

    function navbar() {
        return {
            templateUrl: 'app/navbar/navbar.html',
            controller: navbarController,
            controllerAs: 'vm'
        }
    }
    navbarController.$inject = ['authService', '$state'];
    function navbarController(authService, $state) {
        var vm = this;
        vm.auth = authService;

       
    }
})();