(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', 'baseUrl', 'modelService', 'projectService', 'notificationService', 'localStorageService'];

    function rootCtrl($scope, $location, membershipService, $rootScope, baseUrl, modelService, projectService, notificationService, localStorageService) {

        // Local storage bindings

        // SIDBAR MENU ITEMS

        // USER MAINTENANCE MENU ITEM
        localStorageService.bind($scope, 'isUserMaintenanceActive');
        $scope.setIsUserMaintenanceActive = setIsUserMaintenanceActive;

        function setIsUserMaintenanceActive(value)
        {
            localStorageService.set('isUserMaintenanceActive', value);
            $scope.isUserMaintenanceActive = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
            }
        }

        // TEAM MAINTENANCE MENU ITEM
        localStorageService.bind($scope, 'isTeamMaintenanceActive');
        $scope.setIsTeamMaintenanceActive = setIsTeamMaintenanceActive;

        function setIsTeamMaintenanceActive(value)
        {
            localStorageService.set('isTeamMaintenanceActive', value);
            $scope.isTeamMaintenanceActive = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        // ROLE MAINTENANCE MENU ITEM
        localStorageService.bind($scope, 'isRoleMaintenanceActive');
        $scope.setIsRoleMaintenanceActive = setIsRoleMaintenanceActive;

        function setIsRoleMaintenanceActive(value)
        {
            localStorageService.set('isRoleMaintenanceActive', value);
            $scope.isRoleMaintenanceActive = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        // GROUP MAINTENANCE MENU ITEM
        localStorageService.bind($scope, 'isGroupMaintenanceActive');
        $scope.setIsGroupMaintenanceActive = setIsGroupMaintenanceActive;

        function setIsGroupMaintenanceActive(value)
        {
            localStorageService.set('isGroupMaintenanceActive', value);
            $scope.isGroupMaintenanceActive = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        // DASHBOARD MENU ITEM
        localStorageService.bind($scope, 'isDashboardActive');
        $scope.setIsDashboardActive = setIsDashboardActive;

        function setIsDashboardActive(value)
        {
            localStorageService.set('isDashboardActive', value);
            $scope.isDashboardActive = value;

            if (value)
            {
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        // WEEKLY REPORT MENU ITEM
        localStorageService.bind($scope, 'isWeeklyReportActive');
        $scope.setIsWeeklyReportActive = setIsWeeklyReportActive;

        function setIsWeeklyReportActive(value)
        {
            localStorageService.set('isWeeklyReportActive', value);
            $scope.isWeeklyReportActive = value;

            if (value) {
                $scope.setIsDashboardActive(false);
                
                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        // MY TEAMS MENU ITEMS
        localStorageService.bind($scope, 'doShowMyTeams');
        $scope.setDoShowMyTeams = setDoShowMyTeams;

        function setDoShowMyTeams(value)
        {
            localStorageService.set('doShowMyTeams', value);
            $scope.doShowMyTeams = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        $scope.toggleShowMyTeams = toggleShowMyTeams;

        function toggleShowMyTeams()
        {
            if ($scope.doShowMyTeams)
            {
                $scope.setDoShowMyTeams(false);
            }
            else
            {
                $scope.setDoShowMyTeams(true);
            }
        }

        // MY TEAM PROJECTS MENU ITEMS
        localStorageService.bind($scope, 'doShowMyTeamProjects');
        $scope.setDoShowMyTeamProjects = setDoShowMyTeamProjects;

        function setDoShowMyTeamProjects(value)
        {
            localStorageService.set('doShowMyTeamProjects', value);
            $scope.doShowMyTeamProjects = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                
                $scope.setShowMyGroups(false);
                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        $scope.toggleDoShowMyTeamProjects = toggleDoShowMyTeamProjects;

        function toggleDoShowMyTeamProjects()
        {
            if ($scope.doShowMyTeamProjects)
            {
                $scope.setDoShowMyTeamProjects(false);
            }
            else
            {
                $scope.setDoShowMyTeamProjects(true);
            }
        }

        // MY GROUPS MENU ITEMS
        localStorageService.bind($scope, 'doShowMyGroups');
        $scope.setShowMyGroups = setShowMyGroups;

        function setShowMyGroups(value)
        {
            localStorageService.set('doShowMyGroups', value);
            $scope.doShowMyGroups = value;

            if (value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroupProjects(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        $scope.toggleShowMyGroups = toggleShowMyGroups;

        function toggleShowMyGroups()
        {
            if ($scope.doShowMyGroups)
            {
                $scope.setShowMyGroups(false);
            }
            else
            {
                $scope.setShowMyGroups(true);
            }
        }

        // MY GROUP PROJECTS MENU ITEMS
        localStorageService.bind($scope, 'doShowMyGroupProjects');
        $scope.setShowMyGroupProjects = setShowMyGroupProjects;

        function setShowMyGroupProjects(value)
        {
            localStorageService.set('doShowMyGroupProjects', value);
            $scope.doShowMyGroupProjects = value;

            if(value)
            {
                $scope.setIsDashboardActive(false);
                $scope.setIsWeeklyReportActive(false);

                $scope.setDoShowMyTeams(false);
                $scope.setDoShowMyTeamProjects(false);

                $scope.setShowMyGroups(false);

                $scope.setIsGroupMaintenanceActive(false);
                $scope.setIsRoleMaintenanceActive(false);
                $scope.setIsTeamMaintenanceActive(false);
                $scope.setIsUserMaintenanceActive(false);
            }
        }

        $scope.toggleShowMyGroupProjects = toggleShowMyGroupProjects;

        function toggleShowMyGroupProjects()
        {
            if ($scope.doShowMyGroupProjects)
            {
                $scope.setShowMyGroupProjects(false);
            }
            else
            {
                $scope.setShowMyGroupProjects(true);
            }
        }

        // USER ID
        //$scope.userId = 0;
        localStorageService.bind($scope, 'userId');
        $scope.setUserId = setUserId;

        function setUserId(value)
        {
            localStorageService.set('userId', value);
            $scope.userId = value;
        }
        
        // AVATAR PATH
        //$scope.avatarPath = '';
        localStorageService.bind($scope, 'avatarPath');
        $scope.setAvatarPath = setAvatarPath;

        function setAvatarPath(value)
        {
            localStorageService.set('avatarPath', value);
            $scope.avatarPath = value;
        }

        // PROFILE PIC PATH
        //$scope.profilePicPath = '';
        localStorageService.bind($scope, 'profilePicPath');
        $scope.setProfilePicPath = setProfilePicPath;

        function setProfilePicPath(value)
        {
            localStorageService.set('profilePicPath', value);
            $scope.profilePicPath = value;
        }

        // USERNAME
        //$scope.username = '';
        localStorageService.bind($scope, 'username');
        $scope.setUsername = setUsername;

        function setUsername(value)
        {
            localStorageService.set('username', value);
            $scope.username = value;
        }

        // NICKNAME
        // $scope.nickname = '';
        localStorageService.bind($scope, 'nickname');
        $scope.setNickname = setNickname;

        function setNickname(value)
        {
            localStorageService.set('nickname', value);
            $scope.nickname = value;
        }

        // USER IS ADMIN
        //$scope.isUserAdmin = false;
        localStorageService.bind($scope, 'isUserAdmin');
        $scope.setIsUserAdmin = setIsUserAdmin;

        function setIsUserAdmin(value)
        {
            localStorageService.set('isUserAdmin', value);
            $scope.isUserAdmin = value;
        }

        // PROFILE QUOTE
        // $scope.profileQuote = '';
        localStorageService.bind($scope, 'profileQuote');
        $scope.setProfileQuote = setProfileQuote;

        function setProfileQuote(value)
        {
            localStorageService.set('profileQuote', value);
            $scope.profileQuote = value;
        }

        // USER DATA
        $scope.userData = {};

        // IS USER LOGGED IN
        localStorageService.bind($scope, 'isUserLoggedIn');
        $scope.setIsUserLoggedIn = setIsUserLoggedIn;

        function setIsUserLoggedIn(value)
        {
            localStorageService.set('isUserLoggedIn', value);
            $scope.isUserLoggedIn = value;
        }

        // initialize...
        $scope.setIsUserLoggedIn(false);

        // REFRESH USER INFO
        $scope.refreshUserInfo = refreshUserInfo;

        function refreshUserInfo(userid, fullname, avatarpicpath, profilepicpath, profilequote, nickname) {
            if ($scope.isUserLoggedIn) {
                //$scope.avatarPath = '';
                $scope.setAvatarPath(baseUrl.web + "Content/images/profile/" + avatarpicpath + "?_ts='" + new Date().getTime());

                //$scope.profilePicPath = '';
                $scope.setProfilePicPath(baseUrl.web + "Content/images/profile/" + profilepicpath + "?_ts='" + new Date().getTime());
                
                //$scope.username = fullname;
                $scope.setUsername(fullname);

                //$scope.nickname = nickname;
                $scope.setNickname(nickname);

                //$scope.profileQuote = profilequote;
                $scope.setProfileQuote(profilequote);

                //$scope.userId = userid;
                $scope.setUserId(userid);
            }
        }

        // CHECK USER LOGGED-IN IN LOGIN PAGE
        $scope.checkIsUserLoggedInForLoginPage = checkIsUserLoggedInForLoginPage;

        function checkIsUserLoggedInForLoginPage()
        {
            var isLoggedIn = membershipService.isUserLoggedIn();

            if (isLoggedIn)
            {
                // REDIRECT TO DASHBOARD...
                $location.path('/dashboard');
            }
            
        }

        // CHECK USER IS LOGGED IN...
        $scope.checkIsUserLoggedIn = checkIsUserLoggedIn;

        function checkIsUserLoggedIn()
        {
            var isLoggedIn = membershipService.isUserLoggedIn();

            if (isLoggedIn)
            {
                $scope.setIsUserLoggedIn(membershipService.isUserLoggedIn());
                $scope.setHideTopBar(false);
                $scope.setHideSideBar(false);
            }
            else
            {
                $scope.setHideTopBar(true);
                $scope.setHideSideBar(true);
                $scope.logout();
            }
        }

        // DISPLAY USER INFO
        $scope.userData.displayUserInfo = displayUserInfo;


        function displayUserInfo(userid, fullname, avatarpicpath, profilepicpath, profilequote, nickname) {

            //$scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            $scope.setIsUserLoggedIn(membershipService.isUserLoggedIn());

            $scope.refreshUserInfo(userid, fullname, avatarpicpath, profilepicpath, profilequote, nickname);

            //Refresh team projects...
            $scope.getTeamProjects();

            //Refresh group projects...
            $scope.getGroupProjects();

            //Refresh groups...
            $scope.getUserGroups();

            //Refresh teams...
            $scope.getUserTeams();
        }

        // HIDE TOPBAR
        localStorageService.bind($scope, 'hideTopBar');
        $scope.setHideTopBar = setHideTopBar;

        function setHideTopBar(value)
        {
            localStorageService.set('hideTopBar', value);
            $scope.hideTopBar = value;
        }

        // initialize topbar...
        $scope.setHideTopBar(true);

        // HIDE SIDBAR
        //$scope.hideSideBar = true;
        localStorageService.bind($scope, 'hideSideBar');
        $scope.setHideSideBar = setHideSideBar;
        
        function setHideSideBar(value)
        {
            localStorageService.set('hideSideBar', value);
            $scope.hideSideBar = value;
        }

        // initalize sidebar...
        $scope.setHideSideBar(true);

        $scope.toggleHideSideBar = toggleHideSideBar;

        function toggleHideSideBar() {
            //$scope.hideSideBar = $scope.hideSideBar == false ? true : false;
            var hideValue = $scope.hideSideBar == false ? true : false;
            $scope.setHideSideBar(hideValue);
        }

        // WEEK DATA
        //$scope.weekData = {};
        localStorageService.bind($scope, 'weekData');
        $scope.setWeekData = setWeekData;

        function setWeekData(value) {
            localStorageService.set('weekData', value);
            $scope.weekData = value;
        }
        
        $scope.displayWeekInfo = displayWeekInfo;
        
        function displayWeekInfo() {
            modelService.loadWeekData(displayWeekInfoCompleted, loadWeekDataFailed);
        }

        function displayWeekInfoCompleted(response) {
            //$scope.weekData = response.data;
            $scope.setWeekData(response.data);
        }

        function loadWeekDataFailed(response) {
            notificationService.displayError(response.data);
        }

        // APP PERMISSIONS
        //$scope.appPermissions = [];
        localStorageService.bind($scope, 'appPermissions');
        $scope.setAppPermissions = setAppPermissions;

        function setAppPermissions(value)
        {
            localStorageService.set('appPermissions', value);
            $scope.appPermissions = value;
        }

        // GROUP PERMISSIONS
        // $scope.groupPermissions = [];
        localStorageService.bind($scope, 'groupPermissions');
        $scope.setGroupPermissions = setGroupPermissions;

        function setGroupPermissions(value)
        {
            localStorageService.set('groupPermissions', value);
            $scope.groupPermissions = value;
        }

        // TEAM PERMISSIONS
        // $scope.teamPermissions = [];
        localStorageService.bind($scope, 'teamPermissions');
        $scope.setTeamPermissions = setTeamPermissions;

        function setTeamPermissions(value)
        {
            localStorageService.set('teamPermissions', value);
            $scope.teamPermissions = value;
        }

        // PROJECT PERMISSIONS
        // $scope.projectPermissions = [];
        localStorageService.bind($scope, 'projectPermissions');
        $scope.setProjectPermissions = setProjectPermissions;

        function setProjectPermissions(value)
        {
            localStorageService.set('projectPermissions', value);
            $scope.projectPermissions = value;
        }

        // SET PERMISSIONS
        $scope.userData.setUserPermissions = setUserPermissions;

        function setUserPermissions(appPermissions, groupPermissions, teamPermissions, projectPermissions, isUserAdmin) {
            //$scope.appPermissions = appPermissions;
            $scope.setAppPermissions(appPermissions);

            //$scope.groupPermissions = groupPermissions;
            $scope.setGroupPermissions(groupPermissions);

            //$scope.teamPermissions = teamPermissions;
            $scope.setTeamPermissions(teamPermissions);

            //$scope.projectPermissions = projectPermissions;
            $scope.setProjectPermissions(projectPermissions);

            //$scope.isUserAdmin = isUserAdmin;
            $scope.setIsUserAdmin(isUserAdmin);
        }

        // USER HAS APP PERMISSION
        $scope.userHasAppPermission = userHasAppPermission;

        function userHasAppPermission(permissionCode) {
            var hasPermission = false;
            var continueProcessing = true;

            if ($scope.appPermissions) {
                angular.forEach($scope.appPermissions, function (p) {
                    if (continueProcessing) {
                        if (p.Name == permissionCode) {
                            hasPermission = true;
                            continueProcessing = false;
                        }
                    }
                });
            }

            return hasPermission;
        }

        // USER HAS GROUP PERMISSION
        $scope.userHasGroupPermission = userHasGroupPermission;

        function userHasGroupPermission(groupID, permissionCode) {
            var hasPermission = false;
            var continueProcessing = true;

            //Check first if user has application permission...
            if (userHasAppPermission(permissionCode)) {
                hasPermission = true;
            }
            else {
                if ($scope.groupPermissions) {
                    angular.forEach($scope.groupPermissions, function (g) {
                        if (continueProcessing) {
                            if (g.GroupID == groupID) {
                                angular.forEach(g.Permissions, function (p) {
                                    if (p.Name == permissionCode) {
                                        hasPermission = true;
                                        continueProcessing = false;
                                    }
                                });
                            }
                        }

                    });
                }
            }

            return hasPermission;
        }

        // USER HAS TEAM PERMISSION
        $scope.userHasTeamPermission = userHasTeamPermission;

        function userHasTeamPermission(teamID, permissionCode) {
            var hasPermission = false;
            var continueProcessing = true;

            //Check first if user has application permission...
            if (userHasAppPermission(permissionCode)) {
                hasPermission = true;
            }
            else {
                if ($scope.teamPermissions) {
                    angular.forEach($scope.teamPermissions, function (t) {
                        if (continueProcessing) {
                            if (t.TeamID == teamID) {
                                angular.forEach(t.Permissions, function (p) {
                                    if (p.Name == permissionCode) {
                                        hasPermission = true;
                                        continueProcessing = false;
                                    }
                                });
                            }
                        }
                    });
                }
            }

            return hasPermission;
        }

        // USER HAS PROJECT PERMISSION
        $scope.userHasProjectPermission = userHasProjectPermission;

        function userHasProjectPermission(projectID, permissionCode) {
            var hasPermission = false;
            var continueProcessing = true;

            //Check first if user has application permission...
            if (userHasAppPermission(permissionCode)) {
                hasPermission = true;
            }
            else {
                if ($scope.projectPermissions) {
                    angular.forEach($scope.projectPermissions, function (proj) {
                        if (continueProcessing) {
                            if (proj.ProjectID == projectID) {
                                angular.forEach(proj.Permissions, function (p) {
                                    if (p.Name === permissionCode) {
                                        //notificationService.displayInfo(p.Name + " : " + permissionCode);
                                        hasPermission = true;
                                        continueProcessing = false;
                                    }
                                });
                            }
                        }
                    });
                }
            }

            return hasPermission;
        }

        // LOGOUT
        $scope.logout = logout;
        
        function logout() {
            membershipService.removeCredentials();
            localStorageService.clearAll();
            document.location.href = baseUrl.web;
        }


        // DEFAULT GROUP
        //$scope.defaultGroup = {};
        localStorageService.bind($scope, 'defaultGroup');
        $scope.setDefaultGroup = setDefaultGroup;

        function setDefaultGroup(value) {
            localStorageService.set('defaultGroup', value);
            $scope.defaultGroup = value;
        }

        $scope.userData.setDefaultGroup = setDefaultGroup;

        // DEFAULT TEAM
        // $scope.defaultTeam = {};
        localStorageService.bind($scope, 'defaultTeam');
        $scope.setDefaultTeam = setDefaultTeam;

        function setDefaultTeam(value)
        {
            localStorageService.set('defaultTeam', value);
            $scope.defaultTeam = value;
        }

        $scope.userData.setDefaultTeam = setDefaultTeam;


        // USER GROUPS
        // $scope.userGroups = [];
        localStorageService.bind($scope, 'userGroups');
        $scope.setUserGroups = setUserGroups;

        function setUserGroups(value)
        {
            localStorageService.set('userGroups', value);
            $scope.userGroups = value;
        }

        $scope.getUserGroups = getUserGroups;

        function getUserGroups() {
            membershipService.getUserGroups($scope.userId, getUserGroupsCompleted, getUserGroupsFailed);
        }

        function getUserGroupsCompleted(response) {
            //$scope.userGroups = response.data;
            $scope.setUserGroups(response.data);
        }

        function getUserGroupsFailed(response) {
            notificationService.displayError(response.data);
        }

        // USER TEAMS
        // $scope.userTeams = [];
        localStorageService.bind($scope, 'userTeams');
        $scope.setUserTeams = setUserTeams;

        function setUserTeams(value)
        {
            localStorageService.set('userTeams', value);
            $scope.userTeams = value;
        }

        $scope.getUserTeams = getUserTeams;

        function getUserTeams() {
            membershipService.getUserTeams($scope.userId, getUserTeamsCompleted, getUserTeamsFailed);
        }

        function getUserTeamsCompleted(response) {
            $scope.userTeams = response.data;
        }

        function getUserTeamsFailed(response) {
            notificationService.displayError(response.data);
        }


        // GROUP ID
        // $scope.groupId = 0;
        localStorageService.bind($scope, 'groupId');
        $scope.setGroupId = setGroupId;

        function setGroupId(value)
        {
            localStorageService.set('groupId', value);
            $scope.groupId = value;
        }

        // TEAM ID
        // $scope.teamId = 0;
        localStorageService.bind($scope, 'teamId');
        $scope.setTeamId = setTeamId;

        function setTeamId(value)
        {
            localStorageService.set('teamId', value);
            $scope.teamId = value;
        }

        // PROJECTS PICTURES BASE URL
        $scope.projectsPicBaseUrl = baseUrl.web + "Content/images/project/";

        // GROUP PROJECTS START
        //$scope.groupProjects = {};

        localStorageService.bind($scope, 'groupProjects');
        $scope.setGroupProjects = setGroupProjects;

        function setGroupProjects(value)
        {
            localStorageService.set('groupProjects', value);
            $scope.groupProjects = value;
        }

        $scope.getGroupProjects = getGroupProjects;

        function getGroupProjects() {
            projectService.getGroupProjects($scope.userId, getGroupProjectsCompleted, getGroupProjectsFailed);
        }

        function getGroupProjectsCompleted(response) {
            //$scope.groupProjects = response.data;
            $scope.setGroupProjects(response.data);
        }

        function getGroupProjectsFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.editGroupProject = editGroupProject;

        function editGroupProject(projectId, mainProjectId, isGroupProject) {
            var editPath = '/registerproject/' + projectId + '/' + mainProjectId + '/' + isGroupProject;
            $location.path(editPath);
        }

        $scope.viewGroupProject = viewGroupProject;

        function viewGroupProject(projectId)
        {
            var editPath = '/project/' + projectId;
            $location.path(editPath);
        }
        
        // GROUP PROJECTS END

        // TEAM PROJECTS START
        //$scope.teamProjects = {};
        localStorageService.bind($scope, 'teamProjects');
        $scope.setTeamProjects = setTeamProjects;

        function setTeamProjects(value)
        {
            localStorageService.set('teamProjects', value);
            $scope.teamProjects = value;
        }

        $scope.getTeamProjects = getTeamProjects;

        function getTeamProjects() {
            projectService.getTeamProjects($scope.userId, getTeamProjectsCompleted, getTeamProjectsFailed);
        }

        function getTeamProjectsCompleted(response) {
            //$scope.teamProjects = response.data;
            $scope.setTeamProjects(response.data);
        }

        function getTeamProjectsFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.editTeamProject = editTeamProject;

        function editTeamProject(projectId, mainProjectId, isGroupProject) {
            var editPath = '/registerproject/' + projectId + '/' + mainProjectId + '/' + isGroupProject;
            $location.path(editPath);
        }

        $scope.viewTeamProject = viewTeamProject;

        function viewTeamProject(projectId) {
            var editPath = '/project/' + projectId;
            $location.path(editPath);
        }

        // TEAM PROJECTS END
        $scope.displayWeekInfo();
    }

})(angular.module('weekly'));