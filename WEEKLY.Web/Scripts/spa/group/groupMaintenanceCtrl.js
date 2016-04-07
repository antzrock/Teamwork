(function (app) {
    'use strict';

    app.controller('groupMaintenanceCtrl', groupMaintenanceCtrl);

    groupMaintenanceCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function groupMaintenanceCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {
        $scope.groups = [];
        $scope.loadAllGroups = loadAllGroups;
        $scope.viewGroup = viewGroup;
        $scope.editGroup = editGroup;

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

        function next() {
            var nextPage = $scope.page + 1;

            if (nextPage <= $scope.pagesCount - 1) {
                $scope.search(nextPage);
            }
        }

        function previous() {
            var previousPage = $scope.page - 1;

            if (previousPage >= 0) {
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

        function filterTextChanged() {
            $scope.search($scope.page);
        }

        function pageSizeChanged() {
            $scope.search($scope.page);
        }

        function search(currentPage) {
            currentPage = currentPage || 0;

            modelService.loadGroupForMaintenance(currentPage, $scope.pageSize, $scope.fieldFilter, loadAllGroupsCompleted, requestFailed);

        }

        function viewGroup(groupID)
        {
            var editPath = '/group/' + groupID;
            $location.path(editPath);
        }

        function editGroup(groupID)
        {
            var editPath = '/group/register/' + groupID;
            $location.path(editPath);
        }

        function loadAllGroups()
        {
            $scope.search();
        }

        function loadAllGroupsCompleted(response)
        {
            $scope.page = parseInt(response.data.Page);
            $scope.pagesCount = parseInt(response.data.TotalPages);
            $scope.totalCount = parseInt(response.data.TotalCount);
            $scope.groups = response.data.Items;

            // Compute page range...
            var tempEnd = $scope.pageSize * ($scope.page + 1);
            $scope.rangeStart = tempEnd - ($scope.pageSize - 1);
            $scope.rangeEnd = $scope.rangeStart + ($scope.groups.length - 1);
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.setIsGroupMaintenanceActive(true);
        $scope.checkIsUserLoggedIn();
        $scope.loadAllGroups();
    }

})(angular.module('common.core'));