(function () {

    'use strict';

    angular.module('app').controller('EditTaskController', editTaskController);

    editTaskController.$inject = ['$http', 'authService', '$state','$stateParams'];

    function editTaskController($http, authService, $state, $stateParams) {
        var vm = this;
        vm.message= 'this is edit task'
        vm.auth = authService;

        if (!vm.auth.isAuthenticated())
            $state.go('home');

        vm.error = {
            name: false,
            description: false,
            status: false,
            assignedUsers: false,
            dueDate: false
        };


        vm.allUserCall = function () {
            $http.get(baseUrl+'/api/users').then(
                function (result) {
                    vm.allUser = result.data;
                    createUserOptionList();
                },
                function (error) {
                    console.log(error);
                }
            );
        }

        vm.allUserCall();
        vm.allStatus = $stateParams.myParam.Status;
        if ($stateParams.myParam.task) {
            vm.task = $stateParams.myParam.task;
           
            vm.selectedStatus = vm.allStatus.find(x => x.id === vm.task.status);
        }
        else {
            vm.task = {
                taskId: null,
                name: '',
                description: '',
                status: null,
                dueDate: '',
                assignedUsersId: []
            };
        }
        

        function createUserOptionList()
        {
            vm.UserOptionList = [];
            for (var user in vm.allUser) {
                var userOption = { id: vm.allUser[user].userId, name: vm.allUser[user].userName }
                vm.UserOptionList.push(userOption);
            }

            vm.selectedUsers = [];
            for (var id in vm.task.assignedUsersId) {
                var selectedUser = vm.UserOptionList.find(x => x.id == vm.task.assignedUsersId[id]);
                vm.selectedUsers.push(selectedUser);
            }
        }

        vm.CancelEditTask = function () {
            $state.go("home");
        }

        vm.AddorUpdateTask = function ()
        {
            validateTask();
            var isAnyError = testAllProperties(vm.error, false);
            if (isAnyError === false) {
                {
                    vm.task.status = vm.selectedStatus.id;
                    vm.task.assignedUsersId = [];
                    for (var i in vm.selectedUsers) {
                        var userId = vm.selectedUsers[i].id;
                        vm.task.assignedUsersId.push(userId);
                    }
                    if (vm.task.taskId == null)
                    {
                        vm.postTask();
                    }
                    else {
                        vm.updateTask();
                    }
                }
                // create the update or new task dto to post
            }
        }
        vm.postTask = function () {
            var url = baseUrl+'/api/tasks';
            var data = vm.task;
            $http.post(url, data).then(
                function (msg) {
                    if (msg.status === 200) {
                        $state.go("home");

                    }
                },
                function (error) {
                    vm.taskErrorMessage = error.data;
                    console.log(error);
                });

        }

        vm.updateTask = function () {
            var url = baseUrl+'/api/tasks/'+vm.task.taskId;
            var data = vm.task;
            $http.put(url, data).then(
                function (msg) {
                    if (msg.status === 200) {
                        $state.go("home");

                    }
                },
                function (error) {
                    vm.taskErrorMessage = error.data;
                    console.log(error);
                });

        }

        function validateTask()
        {
            if (!vm.task.name)
                vm.error.name = true;

            if (!vm.task.description)
                vm.error.description = true;

            if (!vm.selectedStatus)
                vm.error.status = true;

            if (!vm.task.dueDate)
                vm.error.dueDate = true;

            if (!vm.selectedUsers)
                vm.error.assignedUsers = true;

            
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

    }

})();