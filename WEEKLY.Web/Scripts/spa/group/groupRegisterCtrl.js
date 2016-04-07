(function (app) {
    'use strict';

    app.controller('groupRegisterCtrl', groupRegisterCtrl);

    groupRegisterCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function groupRegisterCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {
        $scope.isNewGroup = true;
        $scope.group = {};
        $scope.registerGroup = registerGroup;
        $scope.displayGroup = displayGroup;
        $scope.initializeNewGroup = initializeNewGroup;
        $scope.getExistingGroup = getExistingGroup;
        $scope.loadUsers = loadUsers;
        $scope.users = [];
        $scope.selectedGroupStatus = {};
        $scope.groupStates = [];
        $scope.onGroupStatusSelected = onGroupStatusSelected;
        $scope.loadGroupStatus = loadGroupStatus;
        $scope.showUser = showUser;
        $scope.roles = [];
        $scope.loadRoles = loadRoles;
        $scope.showRole = showRole;
        $scope.memberStates = [];
        $scope.showMemberStatus = showMemberStatus;
        $scope.loadGroupMemberStates = loadGroupMemberStates;
        $scope.removeUser = removeUser;
        $scope.addUser = addUser;
        $scope.defaultMemberStatus = '';
        $scope.membersHasError = false;
        $scope.atLeastOneMember = false;
        $scope.atLeastOneProjectAdmin = false;
        $scope.projectAdminId = 0;

        function showMemberStatus(member) {

            if (member.UserID && $scope.memberStates.length) {
                var selected = $filter('filter')($scope.memberStates, { Status: member.Status });
                return selected.length ? selected[0].Status : 'Member status is required';
            } else {
                return member.Status || 'Member status is required';
            }
        }

        function addUser()
        {
            $scope.inserted = {
                id: $scope.group.Members.length + 1,
                GroupMemberID: 0,
                UserID: 0,
                Fullname: $scope.username,
                Roles: [],
                Status: $scope.defaultMemberStatus,
                IsUserDefault: false
            };
            $scope.group.Members.push($scope.inserted);
        }

        function removeUser(index)
        {
            $scope.group.Members.splice(index, 1);
        }

        function loadGroupMemberStates()
        {
            modelService.loadGroupMemberStates(loadGroupMemberStatesCompleted, requestFailed);
        }

        function loadGroupMemberStatesCompleted(response)
        {
            $scope.memberStates = response.data;

            var continueLoop = true;

            angular.forEach($scope.memberStates, function (s) {
                if (continueLoop) {
                    if (s.Status == 'Active') {
                        continueLoop = false;
                        $scope.defaultMemberStatus = s.Status;
                    }
                }
            });
        }

        function showRole(member)
        {
            var selected = [];
            angular.forEach($scope.roles, function (s) {
                if (member.Roles.indexOf(s.GroupRoleID) >= 0) {
                    selected.push(s.Name);
                }
            });

            return selected.length ? selected.join(', ') : 'At least one (1) role is required';
        }

        function loadRoles()
        {
            modelService.loadGroupRoles(loadRolesCompleted, requestFailed);
        }

        function loadRolesCompleted(response)
        {
            $scope.roles = response.data;

            var continueFindingProjectAdmin = true;

            angular.forEach($scope.roles, function (r) {
                if (continueFindingProjectAdmin) {
                    if (r.Name == "GroupAdmin") {
                        $scope.projectAdminId = r.GroupRoleID;
                        continueFindingProjectAdmin = false;
                    }
                }
            })
        }

        function showUser(member)
        {
            if (member.UserID && $scope.users.length) {
                var selected = $filter('filter')($scope.users, { Fullname: member.Fullname });
                return selected.length ? selected[0].Fullname : 'User is required';
            } else {
                return member.Fullname || 'User is required';
            }
        }

        function loadGroupStatus()
        {
            modelService.loadGroupStatus(loadGroupStatusCompleted, requestFailed);
        }

        function loadGroupStatusCompleted(response)
        {
            $scope.groupStates = response.data;
            var continueLoop = true;

            angular.forEach($scope.groupStates, function (s) {
                if (continueLoop)
                {
                    if (s.Name == 'Active')
                    {
                        continueLoop = false;
                        $scope.selectedGroupStatus = s;
                    }
                }
            });

            $scope.displayGroup();
        }

        function onGroupStatusSelected(state)
        {
            $scope.selectedGroupStatus = state;
            $scope.group.Status = state;
        }

        function loadUsers()
        {
            membershipService.loadUsers(loadUsersCompleted, requestFailed);
        }

        function loadUsersCompleted(response)
        {
            $scope.users = response.data;
        }

        function getExistingGroup()
        {
            modelService.getGroupForUpdate($routeParams.id, getExistingGroupCompleted, requestFailed);
        }

        function getExistingGroupCompleted(response)
        {
            $scope.group = response.data;

            $scope.selectedGroupStatus = $scope.group.Status;

        }

        function initializeNewGroup()
        {
            $scope.group.GroupID = 0;
            $scope.group.Name = '';
            $scope.group.Code = '';
            $scope.group.Description = '';
            $scope.group.Members = [];
            $scope.group.Status = $scope.selectedGroupStatus;
            $scope.group.UserID = 0;
        }

        function displayGroup()
        {
            if ($routeParams.id == 0) {
                $scope.isNewGroup = true;
                $scope.initializeNewGroup();
            }
            else {
                $scope.isNewGroup = false;
                $scope.getExistingGroup();
            }
        }

        function registerGroup()
        {
            $scope.membersHasError = false;

            if ($scope.group.Members.length <= 0) {
                $scope.atLeastOneMember = false;
            }
            else {
                $scope.atLeastOneMember = true;
            }

            //Check members....
            angular.forEach($scope.group.Members, function (m) {

                // Check roles...
                if (m.Roles.length === 0) {
                    $scope.membersHasError = true;
                }

                // Check at least one Project Admin registered...
                var continueRoleCheck = true;

                angular.forEach(m.Roles, function (r) {
                    if (continueRoleCheck) {
                        if (r == $scope.projectAdminId) {
                            $scope.atLeastOneProjectAdmin = true;
                            continueRoleCheck = false;
                        }
                    }
                });
            });

            if ($scope.membersHasError) {
                notificationService.displayError("Group members has errors. Please check!");
            };

            if (!$scope.atLeastOneMember) {
                notificationService.displayError("Please add at least one project member!");
            }

            if (!$scope.atLeastOneProjectAdmin) {
                notificationService.displayError("Please configure at least one group member as a Group Admin!");
            }

            if (!$scope.membersHasError && $scope.atLeastOneMember && $scope.atLeastOneProjectAdmin) {
                $scope.group.UserID = $scope.userId;
                modelService.registerGroup($scope.group, registerGroupCompleted, requestFailed);
            }
        }

        function registerGroupCompleted(response)
        {
            if (response.data.success) {
                $scope.getUserGroups();
                notificationService.displaySuccess("Group saved successfully!");
                notificationService.displayWarning("Role and permission changes affecting your user will take effect after next login.");
            }
            else {
                notificationService.displayError(response.data);
            }
        }

        function requestFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.setShowMyGroups(true);
        $scope.checkIsUserLoggedIn();
        $scope.loadRoles();
        $scope.loadUsers();
        $scope.loadGroupMemberStates();
        $scope.loadGroupStatus();
    }

})(angular.module('common.core'));