(function (app) {
    'use strict';

    app.controller('teamMaintenanceCtrl', teamMaintenanceCtrl);

    teamMaintenanceCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function teamMaintenanceCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {
        $scope.teams = [];
        $scope.loadAllTeams = loadAllTeams;
        $scope.editTeam = editTeam;
        $scope.viewTeam = viewTeam;

        // PAGING
        $scope.fieldFilter = '';
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pageSize = 5;
        $scope.totalCount = 0;
        $scope.search = search;
        $scope.pageSizeChanged = pageSizeChanged;
        $scope.filterTextChanged = filterTextChanged;
        $scope.rangeStart = 1;
        $scope.rangeEnd = 5;
        $scope.range = range;
        $scope.next = next;
        $scope.previous = previous;

        function next()
        {
            var nextPage = $scope.page + 1;

            if (nextPage <= $scope.pagesCount - 1)
            {
                $scope.search(nextPage);
            }
        }

        function previous()
        {
            var previousPage = $scope.page - 1;

            if (previousPage >= 0)
            {
                $scope.search($scope.page - 1);
            }
        }

        function range() {

            if (!$scope.pagesCount) { return []; }
            var step = 2;
            var doubleStep = step * 2;
            var start = Math.max(0, $scope.page - step);
            var end = start + 1 + doubleStep;
            if (end > $scope.pagesCount) { end = $scope.pagesCount; }

            var ret = [];
            for (var i = start; i != end; ++i) {
                ret.push(i);
            }

            return ret;
        };

        function filterTextChanged()
        {
            $scope.search($scope.page);
        }

        function pageSizeChanged()
        {
            $scope.search($scope.page);
        }

        function search(currentPage)
        {
            currentPage = currentPage || 0;

            modelService.loadTeamsForMaintenance(currentPage, $scope.pageSize, $scope.fieldFilter, loadAllTeamsCompleted, requestFailed);
            
        }

        function viewTeam(teamID)
        {
            var editPath = '/team/' + teamID;
            $location.path(editPath);
        }

        function editTeam(teamID)
        {
            var editPath = '/team/register/' + teamID;
            $location.path(editPath);
        }

        function loadAllTeams()
        {
            $scope.search();
        }

        function loadAllTeamsCompleted(response)
        {
            $scope.page = parseInt(response.data.Page);
            $scope.pagesCount = parseInt(response.data.TotalPages);
            $scope.totalCount = parseInt(response.data.TotalCount);
            $scope.teams = response.data.Items;

            // Compute page range...
            var tempEnd = $scope.pageSize * ($scope.page + 1);
            $scope.rangeStart = tempEnd - ($scope.pageSize - 1);
            $scope.rangeEnd = $scope.rangeStart + ($scope.teams.length - 1);
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.setIsTeamMaintenanceActive(true);
        $scope.checkIsUserLoggedIn();
        $scope.loadAllTeams();
    }

})(angular.module('common.core'));