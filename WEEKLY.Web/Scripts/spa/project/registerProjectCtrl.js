(function (app) {
    'use strict';

    app.controller('registerProjectCtrl', registerProjectCtrl);

    registerProjectCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl', '$filter', '$timeout', 'projectService'];

    function registerProjectCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, baseUrl, $filter, $timeout, projectService) {
        $scope.project = {};
        $scope.logoFlow = {};
        $scope.isNewProject = true;
        $scope.isTeamProject = true;
        $scope.displayProject = displayProject;
        $scope.registerProject = registerProject;
        $scope.logopicUploaded = logopicUploaded;
        $scope.initializeNewProject = initializeNewProject;
        $scope.getExistingProject = getExistingProject;
        $scope.users = [];
        $scope.roles = [];
        $scope.groups = [];
        $scope.teams = [];
        $scope.loadGroups = loadGroups;
        $scope.onGroupSelect = onGroupSelect;
        $scope.loadUsers = loadUsers;
        $scope.showUser = showUser;
        $scope.loadRoles = loadRoles;
        $scope.showRole = showRole;
        $scope.removeUser = removeUser;
        $scope.addUser = addUser;
        $scope.addActivity = addActivity;
        $scope.removeActivity = removeActivity;
        $scope.picker = { opened: false };
        $scope.formSubmit = formSubmit;
        $scope.membersHasError = false;
        $scope.activitiesHasError = false;
        $scope.atLeastOneMember = false;
        $scope.displayLogo = baseUrl.web + "Content/images/project/default_project_logo.png?_ts='" + new Date().getTime();
        $scope.getDefaultProjectStatus = getDefaultProjectStatus;
        $scope.projectStates = [];
        $scope.loadProjectStatus = loadProjectStatus;
        $scope.memberStates = [];
        $scope.loadMemberStates = loadMemberStates;
        $scope.showMemberStatus = showMemberStatus;
        $scope.scheduleStates = [];
        $scope.loadScheduleStates = loadScheduleStates;
        $scope.showProjectScheduleState = showProjectScheduleState;
        $scope.activityStates = [];
        $scope.loadActivityStates = loadActivityStates;
        $scope.showActivityState = showActivityState;
        $scope.getDefaultMemberStatus = getDefaultMemberStatus;
        $scope.defaultMemberStatus = '';

        $scope.getDefaultScheduleStatus = getDefaultScheduleStatus;
        $scope.defaultScheduleStatus = '';

        $scope.getDefaultActivityStatus = getDefaultActivityStatus;
        $scope.defaultActivityStatus = '';

        $scope.getDefaultProjectCategory = getDefaultProjectCategory;
        $scope.defaultProjectCategory = {};
        $scope.loadProjectCategories = loadProjectCategories;
        $scope.projectCategories = [];
        $scope.selectedCategory = {};
        $scope.onProjectCategorySelect = onProjectCategorySelect;

        $scope.onGroupSelect2 = onGroupSelect2;
        $scope.selectedGroup = {};
        $scope.selectedTeam = {};
        $scope.onTeamSelected = onTeamSelected;
        $scope.selectedProjectStatus = {};
        $scope.onProjectStatusSelected = onProjectStatusSelected;

        $scope.atLeastOneProjectAdmin = false;
        $scope.projectAdminId = 0;

        function onProjectCategorySelect(category)
        {
            $scope.selectedCategory = category;
            $scope.project.Category = category;
        }

        function onProjectStatusSelected(state)
        {
            $scope.project.Status = state;
            $scope.selectedProjectStatus = state;
        }

        function loadProjectCategories()
        {
            projectService.loadProjectCategories(loadProjectCategoriesCompleted, loadProjectCategoriesFailed);
        }

        function loadProjectCategoriesCompleted(response)
        {
            $scope.projectCategories = response.data;
        }

        function loadProjectCategoriesFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getDefaultProjectCategory()
        {
            projectService.getDefaultProjectCategory(getDefaultProjectCategoryCompleted, getDefaultProjectCategoryFailed)
        }

        function getDefaultProjectCategoryCompleted(response)
        {
            $scope.defaultProjectCategory = response.data;
            $scope.selectedCategory = $scope.defaultProjectCategory;
            $scope.project.Category = $scope.defaultProjectCategory;
        }

        function getDefaultProjectCategoryFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getDefaultActivityStatus()
        {
            projectService.getDefaultActivityStatus(getDefaultActivityStatusCompleted, getDefaultActivityStatusFailed);
        }

        function getDefaultActivityStatusCompleted(response)
        {
            $scope.defaultActivityStatus = response.data.ActivityStatus;
        }

        function getDefaultActivityStatusFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getDefaultScheduleStatus()
        {
            projectService.getDefaultScheduleStatus(getDefaultScheduleStatusCompleted, getDefaultScheduleStatusFailed);
        }

        function getDefaultScheduleStatusCompleted(response)
        {
            $scope.defaultScheduleStatus = response.data.Status;
        }

        function getDefaultScheduleStatusFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getDefaultMemberStatus()
        {
            projectService.getDefaultMemberStatus(getDefaultMemberStatusCompleted, getDefaultMemberStatusFailed);
        }

        function getDefaultMemberStatusCompleted(response)
        {
            $scope.defaultMemberStatus = response.data.Status;
        }

        function getDefaultMemberStatusFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function showActivityState(activity)
        {
            if (activity.ProjectScheduleID && $scope.activityStates.length) {
                var selected = $filter('filter')($scope.activityStates, { ActivityStatus: activity.ActivityStatus });
                return selected.length ? selected[0].ActivityStatus : 'Activity status is required';
            } else {
                return activity.ActivityStatus || 'Activity status is required';
            }
        }

        function loadActivityStates()
        {
            projectService.loadActivityStates(loadActivityStatesCompleted, loadActivityStatesFailed);
        }

        function loadActivityStatesCompleted(response)
        {
            $scope.activityStates = response.data;
        }

        function loadActivityStatesFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadScheduleStates()
        {
            projectService.loadScheduleStates(loadScheduleStatesCompleted, loadScheduleStatesFailed);
        }

        function loadScheduleStatesCompleted(response)
        {
            $scope.scheduleStates = response.data;
        }

        function loadScheduleStatesFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadMemberStates()
        {
            projectService.loadMemberStates(loadMemberStatesCompleted, loadMemberStatesFailed);
        }

        function loadMemberStatesCompleted(response)
        {
            $scope.memberStates = response.data;
        }

        function loadMemberStatesFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadProjectStatus()
        {
            projectService.loadProjectStatus(loadProjectStatusCompleted, loadProjectStatusFailed);
        }

        function loadProjectStatusCompleted(response)
        {
            $scope.projectStates = response.data;
            $scope.selectedProjectStatus = $scope.projectStates[0];
        }

        function loadProjectStatusFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getDefaultProjectStatus()
        {
            projectService.getDefaultProjectStatus(getDefaultProjectStatusCompleted, getDefaultProjectStatusFailed);
        }
        
        function getDefaultProjectStatusCompleted(response)
        {
            $scope.project.Status = response.data;
        }

        function getDefaultProjectStatusFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function displayProject()
        {
            if ($routeParams.id == 0)
            {
                $scope.isNewProject = true;
                $scope.initializeNewProject();
            }
            else
            {
                $scope.isNewProject = false;
                $scope.getExistingProject();
            }
        }

        function getExistingProject()
        {
            projectService.getProject($routeParams.id, getExistingProjectCompleted, getExistingProjectFailed);
        }

        function getExistingProjectCompleted(response)
        {
            $scope.project = response.data;
            $scope.displayLogo = baseUrl.web + "Content/images/project/" + $scope.project.LogoPath + "?_ts='" + new Date().getTime();
            $scope.project.UpdatedBy = $scope.userId;

            if ($scope.project.isGroupProject === true)
            {
                $scope.isTeamProject = false;
                $scope.setShowMyGroupProjects(true);
            }
            else
            {
                $scope.isTeamProject = true;
                $scope.setDoShowMyTeamProjects(true);
            }

            $scope.selectedGroup = $scope.project.Group;
            $scope.selectedTeam = $scope.project.Team;
            $scope.selectedProjectStatus = $scope.project.Status;
            $scope.selectedCategory = $scope.project.Category;
            //notificationService.displayInfo("Is team project: " + $scope.isTeamProject);
        }

        function getExistingProjectFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function formSubmit()
        {
            var frm = angular.element(document.querySelector('#submitProject'));
            frm.click();
        }

        function onGroupSelect2(grp)
        {
            $scope.project.Group = grp;
            $scope.selectedGroup = grp;

            if($scope.isUserAdmin)
            {
                modelService.loadTeams(grp.GroupID, loadTeamsCompleted);
            }
            else
            {
                modelService.loadTeamsForUser(grp.GroupID, $scope.userId, loadTeamsCompleted);
            }
        }

        function onTeamSelected(team)
        {
            $scope.project.Team = team;
            $scope.selectedTeam = team;
        }

        function onGroupSelect($item, $model, $label)
        {
            //notificationService.displayInfo("Selected Group: " + $item.GroupID);
            if ($scope.isUserAdmin)
            {
                modelService.loadTeams($item.GroupID, loadTeamsCompleted);
            }
            else
            {
                modelService.loadTeamsForUser($item.GroupID, $scope.userId, loadTeamsCompleted);
            }
            
        }

        function loadTeamsCompleted(response)
        {
            $scope.teams = response.data;

            if ($routeParams.id == 0)
            {
                $scope.selectedTeam = $scope.defaultTeam;
            }
        }

        function loadGroups()
        {
            if (!$scope.isUserAdmin)
            {
                modelService.loadGroupsForUser($scope.userId, loadGroupsCompleted);
            }
            else
            {
                modelService.loadGroups(loadGroupsCompleted);
            }
        }

        function loadGroupsCompleted(response)
        {
            $scope.groups = response.data;

            if ($routeParams.id == 0)
            {
                $scope.onGroupSelect2($scope.defaultGroup);
            }
            
        }

        $scope.openPicker = function () {
            $timeout(function () {
                $scope.picker.opened = true;
            });
        };

        $scope.closePicker = function () {
            $scope.picker.opened = false;
        };

        function removeActivity(index)
        {
            $scope.project.Activities.splice(index, 1);
        }

        function addActivity()
        {
            $scope.inserted = {
                id: $scope.project.Activities.length + 1,
                ProjectScheduleID: 0,
                Activity: '',
                PlannedStartDate: new Date(),
                PlannedEndDate: new Date(),
                ActualStartDate: new Date(),
                ActualEndDate: new Date(),
                ProjectID: $scope.project.ProjectID,
                Status: $scope.defaultScheduleStatus,
                ActivityStatus: $scope.defaultActivityStatus
            };
            $scope.project.Activities.push($scope.inserted);
        }

        function addUser()
        {
            $scope.inserted = {
                id: $scope.project.TeamMembers.length + 1,
                ScrumTeamMemberID : 0,
                UserID: 0,
                Fullname: $scope.username,
                Roles: [],
                ProjectID: $scope.project.ProjectID,
                Status: $scope.defaultMemberStatus
            };
            $scope.project.TeamMembers.push($scope.inserted);
        }

        // remove user
        function removeUser(index) {
            $scope.project.TeamMembers.splice(index, 1);
        };

        function showRole(member)
        {
            var selected = [];
            angular.forEach($scope.roles, function (s) {
                if (member.Roles.indexOf(s.ScrumRoleID) >= 0) {
                    selected.push(s.Rolename);
                }
            });
            return selected.length ? selected.join(', ') : 'At least one (1) role is required';
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

        function showMemberStatus(member)
        {
            if (member.UserID && $scope.memberStates.length) {
                var selected = $filter('filter')($scope.memberStates, { Status: member.Status });
                return selected.length ? selected[0].Status : 'Member status is required';
            } else {
                return member.Status || 'Member status is required';
            }
        }

        function showProjectScheduleState(activity)
        {
            if (activity.ProjectScheduleID && $scope.scheduleStates.length) {
                var selected = $filter('filter')($scope.scheduleStates, { Status: activity.Status });
                return selected.length ? selected[0].Status : 'Activity status is required';
            } else {
                return activity.Status || 'Activity status is required';
            }
        }

        function loadUsers()
        {
            membershipService.loadUsers(loadUsersCompleted, loadUsersFailed);
            //membershipService.loadUsersForGroup($scope.project.Group.GroupID, loadUsersCompleted, loadUsersFailed);
        }

        function loadUsersCompleted(response)
        {
            $scope.users = response.data;
        }

        function loadUsersFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadRoles()
        {
            membershipService.loadScrumRoles(loadRolesCompleted, loadRolesFailed);
        }

        function loadRolesCompleted(response)
        {
            $scope.roles = response.data;

            var continueFindingProjectAdmin = true;

            angular.forEach($scope.roles, function (r) {
                if (continueFindingProjectAdmin)
                {
                    if (r.Rolename == "Project Admin")
                    {
                        $scope.projectAdminId = r.ScrumRoleID;
                        continueFindingProjectAdmin = false;
                        //notificationService.displayInfo("Project Admin Role Found: " + $scope.projectAdminId);
                    }
                }
            })
        }

        function loadRolesFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function initializeNewProject()
        {
            $scope.project.ProjectID = 0;
            $scope.project.Name = '';
            $scope.project.Code = '';
            $scope.project.ExecutiveSummary = '';
            $scope.project.LogoPath = "default_project_logo.png";
            $scope.project.MainProjectID = $routeParams.mainProjectId;
            $scope.project.TeamMembers = [];
            $scope.project.Activities = [];
            $scope.project.Group = $scope.defaultGroup;
            $scope.project.Team = $scope.defaultTeam;
            $scope.project.Category = $scope.defaultProjectCategory;

            if ($routeParams.isGroupProject == 0)
            {
                $scope.project.isGroupProject = false;
                $scope.isTeamProject = true;
                $scope.setDoShowMyTeamProjects(true);
            }
            else
            {
                $scope.project.isGroupProject = true;
                $scope.isTeamProject = false;
                $scope.setShowMyGroupProjects(true);;
            }

            $scope.project.CreatedBy = $scope.userId;

            //notificationService.displayInfo("Is team project: " + $scope.isTeamProject);
        }

        function logopicUploaded($file, $message, $flow)
        {
            notificationService.displayInfo("Logo picture: " + $file.name + " Uploaded!");
            $scope.project.LogoPath = $file.name;
        }

        function isUserExists(userName)
        {
            var userExists = false;
            var continueChecking = true;

            angular.forEach($scope.users, function (u) {
                if (continueChecking)
                {
                    if (u.Fullname === userName) {
                        userExists = true;
                        continueChecking = false;
                    }
                }
                
            });

            return userExists;
        }

        
        
        function registerProject()
        {
            $scope.membersHasError = false;
            
            if ($scope.project.TeamMembers.length <= 0)
            {
                $scope.atLeastOneMember = false;
            }
            else
            {
                $scope.atLeastOneMember = true;
            }

            //Check members....
            angular.forEach($scope.project.TeamMembers, function (m) {
                // Check fullname...
                if (!isUserExists(m.Fullname))
                {
                    $scope.membersHasError = true;
                }

                // Check roles...
                if (m.Roles.length === 0)
                {
                    $scope.membersHasError = true;
                }

                // Check at least one Project Admin registered...
                var continueRoleCheck = true;

                angular.forEach(m.Roles, function (r) {
                    if (continueRoleCheck)
                    {
                        if (r == $scope.projectAdminId)
                        {
                            $scope.atLeastOneProjectAdmin = true;
                            continueRoleCheck = false;
                        }
                    }
                });
            });

            if ($scope.membersHasError)
            {
                notificationService.displayError("Project members has errors. Please check!");
            };

            $scope.activitiesHasError = false;

            // Check activities...
            angular.forEach($scope.project.Activities, function (a) {
                if (!a.Activity)
                {
                    $scope.activitiesHasError = true;
                }
            });

            if ($scope.activitiesHasError)
            {
                notificationService.displayError("Project schedule has errors. Please check!");
            }

            if (!$scope.atLeastOneMember)
            {
                notificationService.displayError("Please add at least one project member!");
            }

            if (!$scope.atLeastOneProjectAdmin)
            {
                notificationService.displayError("Please configure at least one project member as a Project Admin!");
            }

            if (!$scope.membersHasError && !$scope.activitiesHasError && $scope.atLeastOneMember && $scope.atLeastOneProjectAdmin)
            {
                projectService.registerProject($scope.project, registerProjectCompleted, registerProjectFailed);
            }

        }

        function registerProjectCompleted(response)
        {
            if (response.data.success) {
                notificationService.displaySuccess("Team project registered!");
                
                //Refresh team projects...
                $scope.getTeamProjects();
                $scope.getGroupProjects();

                notificationService.displayWarning("Role and permission changes affecting your user will take effect after next login.");
                //$timeout(function () { $scope.logout(); }, 3000);
            }
            else {
                notificationService.displayError(response.data);
            }
        }

        function getUserPermissionsCompleted(response) {
            $scope.userData.setUserPermissions(response.ApplicationPermissions, response.GroupPermissions, response.TeamPermissions, response.ProjectPermissions, response.isUserAdmin);
            notificationService.displayInfo("Updated permissions");
        }

        function getUserPermissionsFailed(response) {
            notificationService.displayError("Error in updating user permissions!");
        }

        function registerProjectFailed(response)
        {
            notificationService.displayError(response.data);
        }


        $scope.checkIsUserLoggedIn();
        $scope.getDefaultProjectCategory();
        $scope.getDefaultActivityStatus();
        $scope.getDefaultScheduleStatus();
        $scope.getDefaultMemberStatus();
        $scope.getDefaultProjectStatus();
        
        $scope.loadProjectStatus();
        $scope.loadMemberStates();
        $scope.loadScheduleStates();
        $scope.loadActivityStates();
        $scope.loadProjectCategories();
        $scope.loadGroups();
        $scope.loadUsers();
        $scope.loadRoles();
        
        $scope.displayProject();
    }

})(angular.module('common.core'));