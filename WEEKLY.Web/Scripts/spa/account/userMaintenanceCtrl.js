(function (app) {
    'use strict';

    app.controller('userMaintenanceCtrl', userMaintenanceCtrl);

    userMaintenanceCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function userMaintenanceCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {
        $scope.users = [];
        $scope.loadAllUsers = loadAllUsers;

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
        $scope.activateUser = activateUser;
        $scope.deactivateUser = deactivateUser;
        $scope.changeUserPassword = changeUserPassword;

        function changeUserPassword(userId)
        {
            notificationService.displayInfo("Change password of user: " + userId);
        }

        function deactivateUser(userId)
        {
            notificationService.displayInfo("Deactivate user: " + userId);
        }

        function activateUser(userId)
        {
            notificationService.displayInfo("Activate user: " + userId);
        }

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

            modelService.loadUsersForMaintenance(currentPage, $scope.pageSize, $scope.fieldFilter, loadUsersForMaintenanceCompleted, requestFailed);

        }

        function loadUsersForMaintenanceCompleted(response)
        {
            $scope.page = parseInt(response.data.Page);
            $scope.pagesCount = parseInt(response.data.TotalPages);
            $scope.totalCount = parseInt(response.data.TotalCount);
            $scope.users = response.data.Items;

            angular.forEach($scope.users, function (m) {
                m.AvatarPicPath = baseUrl.web + "Content/images/profile/" + m.AvatarPicPath + "?_ts=" + new Date().getTime();
            });

            // Compute page range...
            var tempEnd = $scope.pageSize * ($scope.page + 1);
            $scope.rangeStart = tempEnd - ($scope.pageSize - 1);
            $scope.rangeEnd = $scope.rangeStart + ($scope.users.length - 1);
        }

        function loadAllUsers() {
            $scope.search();
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.setIsUserMaintenanceActive(true);
        $scope.checkIsUserLoggedIn();
        $scope.loadAllUsers();
    }

})(angular.module('common.core'));