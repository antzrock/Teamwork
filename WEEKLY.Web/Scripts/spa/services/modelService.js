(function (app) {
    'use strict';

    app.factory('modelService', modelService);

    modelService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function modelService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {
                
        var service = {
            loadGroups: loadGroups,
            loadDefaultGroup: loadDefaultGroup,
            loadTeams: loadTeams,
            loadDefaultTeam: loadDefaultTeam,
            loadWeekData: loadWeekData,
            getServiceDeskStatistics: getServiceDeskStatistics,
            getSpecificWeekData: getSpecificWeekData,
            getYearList: getYearList,
            getWeekNoList: getWeekNoList,
            getSysAidRequests: getSysAidRequests,
            getSysAidIncidents: getSysAidIncidents,
            getSysAidProblems: getSysAidProblems,
            getSysAidProjectsTasks: getSysAidProjectsTasks,
            loadGroupsForUser: loadGroupsForUser,
            loadTeamsForUser: loadTeamsForUser,
            loadGroup: loadGroup,
            loadGroupStatus: loadGroupStatus,
            loadGroupRoles: loadGroupRoles,
            loadGroupMemberStates: loadGroupMemberStates,
            registerGroup: registerGroup,
            getGroupForUpdate: getGroupForUpdate,
            loadTeam: loadTeam,
            loadTeamStatus: loadTeamStatus,
            loadTeamRoles: loadTeamRoles,
            registerTeam: registerTeam,
            getTeamForUpdate: getTeamForUpdate,
            loadGroupForMaintenance: loadGroupForMaintenance,
            loadTeamsForMaintenance: loadTeamsForMaintenance,
            loadUsersForMaintenance: loadUsersForMaintenance
        }

        function loadUsersForMaintenance(page, pageSize, fieldFilter, completed, failed)
        {
            apiService.get('api/Account/maintenance?page=' + page + '&pageSize=' + pageSize + '&fieldFilter=' + fieldFilter, null, completed, failed);
        }

        function loadTeamsForMaintenance(page, pageSize, fieldFilter, completed, failed)
        {
            apiService.get('api/teams/maintenance?page=' + page + '&pageSize=' + pageSize + '&fieldFilter=' + fieldFilter, null, completed, failed);
        }

        function loadGroupForMaintenance(page, pageSize, fieldFilter, completed, failed)
        {
            apiService.get('api/groups/maintenance?page=' + page + '&pageSize=' + pageSize + '&fieldFilter=' + fieldFilter, null, completed, failed);
        }

        function getTeamForUpdate(teamId, completed, failed)
        {
            apiService.get('api/teams?teamId=' + teamId, null, completed, failed);
        }

        function loadTeamRoles(completed, failed)
        {
            apiService.get('api/teams/members/roles', null, completed, failed);
        }

        function loadTeamStatus(completed, failed)
        {
            apiService.get('api/teams/states', null, completed, failed);
        }

        function loadTeam(teamId, completed, failed)
        {
            apiService.get('api/teams?id=' + teamId, null, completed, failed);
        }

        function getGroupForUpdate(groupId, completed, failed)
        {
            apiService.get('api/groups?groupId=' + groupId, null, completed, failed);
        }

        function registerGroup(groupVm, completed, failed)
        {
            apiService.post('api/groups/register', groupVm, completed, failed);
        }

        function registerTeam(teamVm, completed, failed)
        {
            apiService.post('api/teams/register', teamVm, completed, failed);
        }

        function loadGroupMemberStates(completed, failed)
        {
            apiService.get('api/groups/members/states', null, completed, failed);
        }

        function loadGroupRoles(completed, failed)
        {
            apiService.get('api/groups/members/roles', null, completed, failed);
        }

        function loadGroupStatus(completed, failed)
        {
            apiService.get('api/groups/states', null, completed, failed);
        }

        function getSysAidProjectsTasks(userId, dateFilter, completed, failed)
        {
            apiService.get('api/servicedesk/projects/tasks?userId=' + userId + '&startDate=' + dateFilter.sysAidStartDate + '&endDate=' + dateFilter.sysAidEndDate, null, completed, failed);
        }

        function getSysAidProblems(userId, dateFilter, completed, failed)
        {
            apiService.get('api/servicedesk/problems?userId=' + userId + '&startDate=' + dateFilter.sysAidStartDate + '&endDate=' + dateFilter.sysAidEndDate, null, completed, failed);
        }

        function getSysAidIncidents(userId, dateFilter, completed, failed)
        {
            apiService.get('api/servicedesk/incidents?userId=' + userId + '&startDate=' + dateFilter.sysAidStartDate + '&endDate=' + dateFilter.sysAidEndDate, null, completed, failed);
        }

        function getSysAidRequests(userId, dateFilter, completed, failed)
        {
            apiService.get('api/servicedesk/requests?userId=' + userId + '&startDate=' + dateFilter.sysAidStartDate + '&endDate=' + dateFilter.sysAidEndDate, null, completed, failed);
        }

        function getWeekNoList(yearNo, completed, failed)
        {
            apiService.get('api/week/weeknos/' + yearNo, null, completed, failed);
        }

        function getYearList(completed, failed)
        {
            apiService.get('api/week/years', null, completed, failed);
        }

        function getServiceDeskStatistics(userId, completed, failed)
        {
            apiService.get('api/servicedesk/stats/today?userId=' + userId, null, completed, failed)
        }

        function getSpecificWeekData(weekFilter, completed, failed)
        {
            apiService.get('api/week/' + weekFilter.Year + '/' + weekFilter.WeekNo, null, completed, failed);
        }

        function loadWeekData(completed, failed)
        {
           apiService.get('api/week/current', null, completed, failed);
        }

        function loadDefaultTeam(completed)
        {
            apiService.get('api/teams/default',
                           null,
                           completed,
                           loadDefaultTeamFailed);
        }

        function loadDefaultTeamFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadTeamsForUser(groupId, userId, completed)
        {
            apiService.get('api/teams?groupId=' + groupId + "&userId=" + userId, null, completed, loadTeamsFailed);
        }

        function loadTeams(groupId, completed)
        {
            apiService.get('api/teams?groupId=' + groupId,
                           null,
                           completed,
                           loadTeamsFailed);
        }

        function loadTeamsFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadDefaultGroup(completed)
        {
            apiService.get('api/groups/default',
                           null,
                           completed,
                           defaultGroupLoadFailed);
        }

        function defaultGroupLoadFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function loadGroup(groupId, completed, failed)
        {
            apiService.get('api/groups?id=' + groupId, null, completed, failed);
        }

        function loadGroupsForUser(userId, completed)
        {
            apiService.get('api/groups/' + userId, null, completed, groupsLoadFailed);
        }

        function loadGroups(completed)
        {
            apiService.get('api/groups',
                           null,
                           completed,
                           groupsLoadFailed);
        }

        function groupsLoadFailed(response)
        {
            notificationService.displayError(response.data);
        }

        return service;
    }

})(angular.module('common.core'));