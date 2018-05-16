(function () {

  'use strict';

  angular.module('app').controller('HomeController', homeController);

  homeController.$inject = ['$http', 'authService', 'dataService','$state'];

  function homeController($http, authService, dataService, $state) {
      var vm = this;
      vm.loading = true;
        vm.auth = authService;
        vm.CancleTask = 4;
        vm.getUsers = function () {
            $http.get(baseUrl+'/api/users').then(
                function (result) {
                    vm.users = result.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        };
        vm.getUsers();

        vm.getAllTasts = function () {
            $http.get(baseUrl+'/api/tasks').then(
            function (result) {
                vm.tasks = result.data;
                vm.loading = false;
            },
            function (error) {
                console.log(error);
            }
        );
    };
        vm.getAllTasts();

        vm.getAllStatus = function () {
            $http.get(baseUrl+'/api/status').then(
                function (result) {
                    vm.allStatus = result.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        vm.getAllStatus();

        vm.deleteTask = function (id) {
            $http.delete(baseUrl+'/api/tasks/'+id).then(
                function (result) {
                    removeFromTasks(id);
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        vm.editTask = function (curTask, allTasks) {
            $state.go("edittask", { myParam: { task: curTask, Status: vm.allStatus } })
            
        }

        vm.userNames = function (userIds) {
            var names = '';
            if (vm.users) {
                userIds.forEach(function (id) {
                    names = names.concat(vm.users.find(x => x.userId === id).userName) + ", ";
                });
                names = names.substring(0, names.length - 2);
            }
            return names;
        }
        vm.statusDescription = function (st)
        {
            return vm.allStatus.find(x => x.id === st).description; 
        }

        var removeFromTasks = function (id)
        {
            for (var i = 0; i < vm.tasks.length; i++) {
                if (vm.tasks[i].taskId == id) {
                    vm.tasks.splice(i, 1);  //removes 1 element at position i 
                    break;
                }
            }
        }

        vm.addNewaTask = function () {
            $state.go("edittask", { myParam: { task: null, Status: vm.allStatus } })
        }

        vm.AddUser = function () {

            $state.go('user');
        }
  }

})();