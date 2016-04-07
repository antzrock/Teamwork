(function (app) {
    'use strict';

    app.factory('projectService', projectService);

    projectService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function projectService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {
        var service = {
            registerProject: registerProject,
            getTeamProjects: getTeamProjects,
            getGroupProjects: getGroupProjects,
            getAllProjects: getAllProjects,
            getProject: getProject,
            getDefaultProjectStatus: getDefaultProjectStatus,
            loadProjectStatus: loadProjectStatus,
            loadMemberStates: loadMemberStates,
            loadScheduleStates: loadScheduleStates,
            loadActivityStates: loadActivityStates,
            getDefaultMemberStatus: getDefaultMemberStatus,
            getDefaultScheduleStatus: getDefaultScheduleStatus,
            getDefaultActivityStatus: getDefaultActivityStatus,
            loadProjectCategories: loadProjectCategories,
            getDefaultProjectCategory: getDefaultProjectCategory,
            getProjectProductBacklog: getProjectProductBacklog
        }
        
        function getProjectProductBacklog(projectId, completed, failed)
        {
            apiService.get('api/productbacklog?projectId=' + projectId, null, completed, failed);
        }

        function getDefaultProjectCategory(completed, failed)
        {
            apiService.get('api/projects/categories/default', null, completed, failed);
        }

        function loadProjectCategories(completed, failed)
        {
            apiService.get('api/projects/categories', null, completed, failed);
        }

        function getDefaultActivityStatus(completed, failed)
        {
            apiService.get('api/projects/schedule/activity/status/default', null, completed, failed);
        }

        function getDefaultScheduleStatus(completed, failed)
        {
            apiService.get('api/projects/schedule/status/default', null, completed, failed);
        }

        function getDefaultMemberStatus(completed, failed)
        {
            apiService.get('api/projects/member/status/default', null, completed, failed);
        }

        function loadActivityStates(completed, failed)
        {
            apiService.get('api/projects/activity/states', null, completed, failed);
        }

        function loadScheduleStates(completed, failed)
        {
            apiService.get('api/projects/schedule/states', null, completed, failed);
        }

        function loadMemberStates(completed, failed)
        {
            apiService.get('api/projects/member/states', null, completed, failed);
        }

        function loadProjectStatus(completed, failed)
        {
            apiService.get('api/projects/states', null, completed, failed);
        }

        function getDefaultProjectStatus(completed, failed)
        {
            apiService.get('api/projects/status/default', null, completed, failed);
        }

        function getProject(projectId, completed, failed)
        {
            apiService.get('api/projects/' + projectId, null, completed, failed);
        }

        function getAllProjects(userId, completed, failed)
        {
            apiService.get('api/projects/all?userId=' + userId, null, completed, failed);
        }

        function getGroupProjects(userId, completed, failed)
        {
            apiService.get('api/projects/group?userId=' + userId, null, completed, failed);
        }

        function getTeamProjects(userId, completed, failed)
        {
            apiService.get('api/projects/team?userId=' + userId, null, completed, failed);
        }

        function registerProject(pVm, completed, failed)
        {
            apiService.post('api/projects/register', pVm, completed, failed);
        }

        return service;
    }

})(angular.module('common.core'));