(function () {

	'use strict';

    angular
      .module('app')
	  .controller('CallbackController', callbackController);

	callbackController.$inject = [  '$http'];

	function callbackController($http) {
		var vm = this;
	}

})();