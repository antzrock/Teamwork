(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            register: register,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn,
            isUsernameTaken: isUsernameTaken,
            setLoggedInUsersName: setLoggedInUsersName,
            resetPassword: resetPassword,
            getUserProfile: getUserProfile,
            updateProfile: updateProfile,
            getProfilePic: getProfilePic,
            changePassword: changePassword,
            getChangePasswordViewModelForUpdate: getChangePasswordViewModelForUpdate,
            loadUsers: loadUsers,
            loadScrumRoles: loadScrumRoles,
            loadUsersForGroup: loadUsersForGroup,
            loadAllPermissions: loadAllPermissions,
            loadApplicationRoles: loadApplicationRoles,
            saveApplicationRoles: saveApplicationRoles,
            loadGroupRoles: loadGroupRoles,
            getUserPermissions: getUserPermissions,
            saveGroupRoles: saveGroupRoles,
            loadTeamRoles: loadTeamRoles,
            saveTeamRoles: saveTeamRoles,
            loadProjectRoles: loadProjectRoles,
            saveProjectRole: saveProjectRole,
            getUserGroups: getUserGroups,
            getUserTeams: getUserTeams
        }

        function getUserTeams(userId, completed, failed)
        {
            apiService.get('api/teams?userId=' + userId, null, completed, failed);
        }

        function getUserGroups(userId, completed, failed)
        {
            apiService.get('api/account/groups?userId=' + userId, null, completed, failed);
        }

        function getUserPermissions(userId, completed, failed)
        {
            apiService.get('api/account/permissions?userId=' + userId, null, completed, failed);
        }

        function saveProjectRole(projectRoles, completed, failed)
        {
            apiService.post('api/account/project/roles', projectRoles, completed, failed);
        }

        function saveTeamRoles(teamRoles, completed, failed)
        {
            apiService.post('api/account/team/roles', teamRoles, completed, failed);
        }

        function saveGroupRoles(groupRoles, completed, failed)
        {
            apiService.post('api/account/group/roles', groupRoles, completed, failed);
        }

        function saveApplicationRoles(appRoles, completed, failed)
        {
            apiService.post('api/account/roles', appRoles, completed, failed);
        }

        function loadApplicationRoles(completed, failed)
        {
            apiService.get("api/account/roles", null, completed, failed);
        }

        function loadGroupRoles(completed, failed)
        {
            apiService.get("api/account/group/roles", null, completed, failed);
        }

        function loadTeamRoles(completed, failed)
        {
            apiService.get("api/account/team/roles", null, completed, failed);
        }

        function loadProjectRoles(completed, failed)
        {
            apiService.get("api/account/project/roles", null, completed, failed);
        }

        function loadAllPermissions(completed, failed)
        {
            apiService.get('api/account/permissions', null, completed, failed);
        }

        function loadScrumRoles(completed, failed)
        {
            apiService.get('api/account/scrum/roles', null, completed, failed);
        }

        function loadUsersForGroup(groupId, completed, failed)
        {
            apiService.get('api/account/group?groupId=' + groupId, null, completed, failed)
        }

        function loadUsers(completed, failed)
        {
            apiService.get('api/account/all', null, completed, failed);
        }

        function getChangePasswordViewModelForUpdate(userid, completed, failed)
        {
            apiService.get('api/account/changepassword?userId=' + userid, null, completed, failed)
        }

        function getProfilePic(pic, completed, failed)
        {
            apiService.get('api/files?filePath=profile/' + pic, { responseType: "application/octet-stream" }, completed, failed);
        }

        function updateProfile(uservm, completed, failed)
        {
            apiService.post('api/account/update', uservm, completed, failed);
        }

        function getUserProfile(userid, completed, failed)
        {
            apiService.get('/api/account/' + userid, null, completed, failed);
        }

        function resetPassword(user, completed, failed)
        {
            apiService.post('/api/account/resetpassword', user, completed, failed);
        }

        function changePassword(user, completed, failed)
        {
            apiService.post('/api/account/changepassword', user, completed, failed);
        }

        function setLoggedInUsersName(username, completed, failed)
        {
            apiService.get('/api/account/getbyusername/?username=' + username, null,
                completed,
                failed);
        }

        function isUsernameTaken(username, completed, failed)
        {
            apiService.get('/api/account/getbyusername/?username=' + username, null,
                completed,
                failed);
        }

        function login(user, completed) {
            apiService.post('/api/account/authenticate', user,
            completed,
            loginFailed);
        }

        function register(user, completed) {
            apiService.post('/api/account/register', user,
            completed,
            registrationFailed);
        }

        function saveCredentials(user) {
            var membershipData = $base64.encode(user.userName + ':' + user.password);

            $rootScope.repository = {
                loggedUser: {
                    username: user.username,
                    authdata: membershipData
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
            $cookieStore.put('repository', $rootScope.repository);
        }

        function removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        };

        function loginFailed(response) {
            notificationService.displayError(response.data);
        }

        function registrationFailed(response) {

            if (response.data)
            {
                notificationService.displayError(response.data);
            }
            else
            {
                notificationService.displayError('Registration failed. Try again.');
            }
        }

        function isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        }

        return service;
    }



})(angular.module('common.core'));