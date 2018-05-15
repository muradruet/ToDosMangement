(function () {

    'use strict';

    angular
        .module('app').run(run);
    run.$inject = ['authService'];
    function run(authService){
        authService.handleAuthentication();
    }

})();