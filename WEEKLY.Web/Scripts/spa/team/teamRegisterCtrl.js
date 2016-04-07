(function (app) {
    'use strict';

    app.controller('teamRegisterCtrl', teamRegisterCtrl);

    teamRegisterCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function teamRegisterCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {
        $scope.isNewTeam = true;
        $scope.team = {};
        $scope.registerTeam = registerTeam;
        $scope.selectedTeamStatus = {};
        $scope.teamStates = [];
        $scope.onTeamStatusSelected = onTeamStatusSelected;
        $scope.loadTeamStatus = loadTeamStatus;
        $scope.displayTeam = displayTeam;
        $scope.initializeNewTeam = initializeNewTeam;
        $scope.getExistingTeam = getExistingTeam;
        $scope.loadUsers = loadUsers;
        $scope.users = [];
        $scope.showUser = showUser;
        $scope.roles = [];
        $scope.loadRoles = loadRoles;
        $scope.showRole = showRole;
        $scope.memberStates = [];
        $scope.showMemberStatus = showMemberStatus;
        $scope.loadTeamMemberStates = loadTeamMemberStates;
        $scope.defaultMemberStatus = '';
        $scope.removeUser = removeUser;
        $scope.addUser = addUser;
        $scope.membersHasError = false;
        $scope.atLeastOneMember = false;
        $scope.atLeastOneProjectAdmin = false;
        $scope.groups = [];
        $scope.loadGroups = loadGroups;
        $scope.selectedGroup = {};
        $scope.onGroupSelected = onGroupSelected;

        function onGroupSelected(grp)
        {
            $scope.selectedGroup = grp;
            $scope.team.Group = grp;
        }

        function loadGroups() {
            if (!$scope.isUserAdmin) {
                modelService.loadGroupsForUser($scope.userId, loadGroupsCompleted);
            }
            else {
                modelService.loadGroups(loadGroupsCompleted);
            }
        }

        function loadGroupsCompleted(response) {
            $scope.groups = response.data;

            //set selected group to first element
            $scope.selectedGroup = $scope.groups[0];
        }

        function addUser() {
            $scope.inserted = {
                id: $scope.team.Members.length + 1,
                TeamMemberID: 0,
                UserID: 0,
                Fullname: $scope.username,
                Roles: [],
                Status: $scope.defaultMemberStatus,
                IsUserDefault: false
            };
            $scope.team.Members.push($scope.inserted);
        }

        function removeUser(index) {
            $scope.team.Members.splice(index, 1);
        }

        function loadTeamMemberStates() {
            modelService.loadGroupMemberStates(loadTeamMemberStatesCompleted, requestFailed);
        }

        function loadTeamMemberStatesCompleted(response) {
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

        function showMemberStatus(member) {

            if (member.UserID && $scope.memberStates.length) {
                var selected = $filter('filter')($scope.memberStates, { Status: member.Status });
                return selected.length ? selected[0].Status : 'Member status is required';
            } else {
                return member.Status || 'Member status is required';
            }
        }

        function showRole(member) {
            var selected = [];
            angular.forEach($scope.roles, function (s) {
                if (member.Roles.indexOf(s.TeamRoleID) >= 0) {
                    selected.push(s.Name);
                }
            });

            return selected.length ? selected.join(', ') : 'At least one (1) role is required';
        }

        function loadRoles() {
            modelService.loadTeamRoles(loadRolesCompleted, requestFailed);
        }

        function loadRolesCompleted(response) {
            $scope.roles = response.data;

            var continueFindingProjectAdmin = true;

            angular.forEach($scope.roles, function (r) {
                if (continueFindingProjectAdmin) {
                    if (r.Name == "TeamAdmin") {
                        $scope.projectAdminId = r.TeamRoleID;
                        continueFindingProjectAdmin = false;
                    }
                }
            })
        }

        function showUser(member) {
            if (member.UserID && $scope.users.length) {
                var selected = $filter('filter')($scope.users, { Fullname: member.Fullname });
                return selected.length ? selected[0].Fullname : 'User is required';
            } else {
                return member.Fullname || 'User is required';
            }
        }

        function loadUsers() {
            membershipService.loadUsers(loadUsersCompleted, requestFailed);
        }

        function loadUsersCompleted(response) {
            $scope.users = response.data;
        }

        function getExistingTeam()
        {
            modelService.getTeamForUpdate($routeParams.id, getExistingTeamCompleted, requestFailed);
        }

        function getExistingTeamCompleted(response)
        {
            $scope.team = response.data;

            $scope.selectedTeamStatus = $scope.team.Status;
            $scope.selectedGroup = $scope.team.Group;
        }

        function initializeNewTeam()
        {
            $scope.team.TeamID = 0;
            $scope.team.Name = '';
            $scope.team.Code = '';
            $scope.team.Description = '';
            $scope.team.Members = [];
            $scope.team.Status = $scope.selectedTeamStatus;
            $scope.team.UserID = 0;
            $scope.team.Group = $scope.selectedGroup;
        }

        function displayTeam()
        {
            if ($routeParams.id == 0) {
                $scope.isNewTeam = true;
                $scope.initializeNewTeam();
            }
            else {
                $scope.isNewTeam = false;
                $scope.getExistingTeam();
            }
        }

        function loadTeamStatus()
        {
            modelService.loadTeamStatus(loadTeamStatusCompleted, requestFailed);
        }

        function loadTeamStatusCompleted(response)
        {
            $scope.teamStates = response.data;
            var continueLoop = true;

            angular.forEach($scope.teamStates, function (s) {
                if (continueLoop) {
                    if (s.Name == 'Active') {
                        continueLoop = false;
                        $scope.selectedTeamStatus = s;
                    }
                }
            });

            $scope.displayTeam();
        }

        function onTeamStatusSelected(state)
        {
            $scope.selectedTeamStatus = state;
            $scope.team.Status = state;
        }

        function registerTeam()
        {
            $scope.membersHasError = false;

            if ($scope.team.Members.length <= 0) {
                $scope.atLeastOneMember = false;
            }
            else {
                $scope.atLeastOneMember = true;
            }

            //Check members....
            angular.forEach($scope.team.Members, function (m) {

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
                notificationService.displayError("Team members has errors. Please check!");
            };

            if (!$scope.atLeastOneMember) {
                notificationService.displayError("Please add at least one project member!");
            }

            if (!$scope.atLeastOneProjectAdmin) {
                notificationService.displayError("Please configure at least one team member as a Team Admin!");
            }

            if (!$scope.membersHasError && $scope.atLeastOneMember && $scope.atLeastOneProjectAdmin) {
                $scope.team.UserID = $scope.userId;
                modelService.registerTeam($scope.team, registerTeamCompleted, requestFailed);
            }
        }

        function registerTeamCompleted(response) {
            if (response.data.success) {
                $scope.getUserTeams();
                notificationService.displaySuccess("Team saved successfully!");
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

        $scope.setDoShowMyTeams(true);
        $scope.checkIsUserLoggedIn();
        $scope.loadGroups();
        $scope.loadUsers();
        $scope.loadRoles();
        $scope.loadTeamMemberStates();
        $scope.loadTeamStatus();
    }
})(angular.module('common.core'));