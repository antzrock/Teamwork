(function (app) {
    'use strict';

    app.controller('roleCtrl', roleCtrl);

    roleCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl', '$filter', '$timeout', 'projectService'];

    function roleCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, baseUrl, $filter, $timeout, projectService) {
        $scope.applicationRoles = [];
        $scope.groupRoles = [];
        $scope.teamRoles = [];
        $scope.projectRoles = [];
        $scope.updateAppRoles = {};
        $scope.updateGroupRoles = {};
        $scope.updateTeamRoles = {};
        $scope.updateProjectRoles = {};
        $scope.permissions = [];
        $scope.showApplicationRolePermission = showApplicationRolePermission;
        $scope.showGroupRolePermission = showGroupRolePermission;
        $scope.showTeamRolePermission = showTeamRolePermission;
        $scope.showProjectRolePermission = showProjectRolePermission;
        $scope.removeApplicationRole = removeApplicationRole;
        $scope.removeGroupRole = removeGroupRole;
        $scope.removeTeamRole = removeTeamRole;
        $scope.removeProjectRole = removeProjectRole;
        $scope.addApplicationRole = addApplicationRole;
        $scope.addGroupRole = addGroupRole;
        $scope.addTeamRole = addTeamRole;
        $scope.addProjectRole = addProjectRole;
        $scope.getPermissions = getPermissions;
        $scope.getApplicationRoles = getApplicationRoles;
        $scope.getGroupRoles = getGroupRoles;
        $scope.getTeamRoles = getTeamRoles;
        $scope.getProjectRoles = getProjectRoles;
        $scope.saveApplicationRoleChanges = saveApplicationRoleChanges;
        $scope.saveGroupRoleChanges = saveGroupRoleChanges;
        $scope.saveTeamRoleChanges = saveTeamRoleChanges;
        $scope.saveProjectRoleChanges = saveProjectRoleChanges;

        function saveProjectRoleChanges()
        {
            //Check if project role has no errors...
            var continueProjectRoleChecking = true;
            var projectRoleHasError = false;

            angular.forEach($scope.projectRoles, function (r) {
                if(continueProjectRoleChecking)
                {
                    if(!r.Name)
                    {
                        projectRoleHasError = true;
                        continueProjectRoleChecking = false;
                    }
                }
            })

            $scope.updateProjectRoles = {
                UserID: $scope.userId,
                ProjectRoles: $scope.projectRoles
            }

            if (projectRoleHasError)
            {
                notificationService.displayError("Error in Project Roles! Please check!");
            }

            if (!projectRoleHasError)
            {
                membershipService.saveProjectRole($scope.updateProjectRoles, saveProjectRoleChangesCompleted, requestFailed);
            }
        }

        function saveProjectRoleChangesCompleted(response)
        {
            if (response.data.success) {
                notificationService.displayInfo("Project roles updated!");
                notificationService.displayWarning("Project Role and permission changes affecting your user will take effect after next login.");
                $scope.getProjectRoles();
            }
            else {
                notificationService.displayError("Error in updating project roles!");
            }
        }

        function saveTeamRoleChanges()
        {
            //Check if team role has no errors...
            var continueTeamRoleChecking = true;
            var teamRoleHasError = false;

            angular.forEach($scope.teamRoles, function (r) {
                if (continueTeamRoleChecking)
                {
                    if(!r.Name)
                    {
                        teamRoleHasError = true;
                        continueTeamRoleChecking = false;
                    }
                }
            })

            $scope.updateTeamRoles = {
                UserID: $scope.userId,
                TeamRoles: $scope.teamRoles
            }

            if (teamRoleHasError)
            {
                notificationService.displayError("Error in Team Roles! Please check!");
            }

            if (!teamRoleHasError)
            {
                membershipService.saveTeamRoles($scope.updateTeamRoles, saveTeamRoleChangesCompleted, requestFailed);
            }
        }
        
        function saveTeamRoleChangesCompleted(response)
        {
            if (response.data.success) {
                notificationService.displayInfo("Team roles updated!");
                notificationService.displayWarning("Team Role and permission changes affecting your user will take effect after next login.");
                $scope.getTeamRoles();
            }
            else {
                notificationService.displayError("Error in updating team roles!");
            }
        }

        function saveGroupRoleChanges()
        {
            //Check if group role has no errors...
            var continueGroupRoleChecking = true;
            var groupRoleHasError = false;

            angular.forEach($scope.groupRoles, function (r) {
                if (continueGroupRoleChecking)
                {
                    if (!r.Name)
                    {
                        groupRoleHasError = true;
                        continueGroupRoleChecking = false;
                    }
                }
            })

            $scope.updateGroupRoles = {
                UserID: $scope.userId,
                GroupRoles: $scope.groupRoles
            }

            if (groupRoleHasError)
            {
                notificationService.displayError("Error in Group Roles! Please check!");
            }

            if (!groupRoleHasError)
            {
                membershipService.saveGroupRoles($scope.updateGroupRoles, saveGroupRoleChangesCompleted, requestFailed);
            }
        }

        function saveGroupRoleChangesCompleted(response)
        {
            if (response.data.success) {
                notificationService.displayInfo("Group roles updated!");
                notificationService.displayWarning("Group Role and permission changes affecting your user will take effect after next login.");
                $scope.getGroupRoles();
            }
            else {
                notificationService.displayError("Error in updating group roles!");
            }
        }

        function saveApplicationRoleChanges()
        {
            //Check if group role has no errors...
            var continueAppRoleChecking = true;
            var appRoleHasError = false;

            angular.forEach($scope.applicationRoles, function (r) {
                if (continueAppRoleChecking) {
                    if (!r.Name) {
                        appRoleHasError = true;
                        continueAppRoleChecking = false;
                    }
                }
            })

            $scope.updateAppRoles = {
                UserID:  $scope.userId,
                ApplicationRoles: $scope.applicationRoles
            }
            
            if (appRoleHasError)
            {
                notificationService.displayError("Error in Application Roles! Please check!");
            }

            if (!appRoleHasError)
            {
                membershipService.saveApplicationRoles($scope.updateAppRoles, saveApplicationRoleChangesCompleted, requestFailed);
            }
        }

        function saveApplicationRoleChangesCompleted(response)
        {
            if (response.data.success)
            {
                notificationService.displayInfo("Application roles updated!");
                notificationService.displayWarning("Role and permission changes affecting your user will take effect after next login.");
                $scope.getApplicationRoles();
            }
            else
            {
                notificationService.displayError("Error in updating application roles!");
            }
        }

        function getProjectRoles()
        {
            membershipService.loadProjectRoles(getProjectRolesCompleted, requestFailed);
        }

        function getProjectRolesCompleted(response)
        {
            $scope.projectRoles = response.data;
        }

        function getTeamRoles()
        {
            membershipService.loadTeamRoles(getTeamRolesCompleted, requestFailed);
        }

        function getTeamRolesCompleted(response)
        {
            $scope.teamRoles = response.data;
        }

        function getGroupRoles()
        {
            membershipService.loadGroupRoles(getGroupRolesCompleted, requestFailed);
        }

        function getGroupRolesCompleted(response)
        {
            $scope.groupRoles = response.data;
        }

        function getApplicationRoles()
        {
            membershipService.loadApplicationRoles(getApplicationRolesCompleted, requestFailed);
        }

        function getApplicationRolesCompleted(response)
        {
            $scope.applicationRoles = response.data;
        }

        function getPermissions()
        {
            membershipService.loadAllPermissions(getPermissionsCompleted, requestFailed);
        }

        function getPermissionsCompleted(response)
        {
            $scope.permissions = response.data;
            $scope.getApplicationRoles();
            $scope.getGroupRoles();
            $scope.getTeamRoles();
            $scope.getProjectRoles();
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        function addGroupRole()
        {
            $scope.inserted = {
                id: $scope.groupRoles.length + 1,
                GroupRoleID: 0,
                Name: '',
                DefaultPermissions: [],
                Permissions: [],
                isSystemGenerated: false
            };
            $scope.groupRoles.push($scope.inserted);
        }

        function addTeamRole()
        {
            $scope.inserted = {
                id: $scope.teamRoles.length + 1,
                TeamRoleID: 0,
                Name: '',
                DefaultPermissions: [],
                Permissions: [],
                isSystemGenerated: false
            };
            $scope.teamRoles.push($scope.inserted);
        }

        function addProjectRole()
        {
            $scope.inserted = {
                id: $scope.projectRoles.length + 1,
                ProjectRoleID: 0,
                Name: '',
                DefaultPermissions: [],
                Permissions: [],
                isSystemGenerated: false
            };
            $scope.projectRoles.push($scope.inserted);
        }

        function addApplicationRole()
        {
            $scope.inserted = {
                id: $scope.applicationRoles.length + 1,
                RoleID: 0,
                Name: '',
                DefaultPermissions: [],
                Permissions: [],
                isSystemGenerated: false
            };
            $scope.applicationRoles.push($scope.inserted);
        }

        function removeApplicationRole(index)
        {
            $scope.applicationRoles.splice(index, 1);
        }

        function removeGroupRole(index)
        {
            $scope.groupRoles.splice(index, 1);
        }

        function removeTeamRole(index)
        {
            $scope.teamRoles.splice(index, 1);
        }

        function removeProjectRole(index)
        {
            $scope.projectRoles.splice(index, 1);
        }

        function showApplicationRolePermission(role)
        {
            var selected = [];
            angular.forEach($scope.permissions, function (p) {
                if (role.Permissions.indexOf(p.PermissionID) >= 0) {
                    selected.push(p.Name);
                }
            });
            
            return selected.join(', ');
        }

        function showGroupRolePermission(role)
        {
            var selected = [];
            angular.forEach($scope.permissions, function (p) {
                if (role.Permissions.indexOf(p.PermissionID) >= 0) {
                    selected.push(p.Name);
                }
            });

            return selected.join(', ');
        }

        function showTeamRolePermission(role)
        {
            var selected = [];
            angular.forEach($scope.permissions, function (p) {
                if (role.Permissions.indexOf(p.PermissionID) >= 0) {
                    selected.push(p.Name);
                }
            });

            return selected.join(', ');
        }

        function showProjectRolePermission(role)
        {
            var selected = [];
            angular.forEach($scope.permissions, function (p) {
                if (role.Permissions.indexOf(p.PermissionID) >= 0) {
                    selected.push(p.Name);
                }
            });

            return selected.join(', ');
        }

        $scope.setIsRoleMaintenanceActive(true);
        $scope.checkIsUserLoggedIn();
        $scope.getPermissions();
    }

})(angular.module('common.core'));