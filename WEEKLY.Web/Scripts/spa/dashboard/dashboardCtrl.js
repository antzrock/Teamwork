(function (app) {
    'use strict';

    app.controller('dashboardCtrl', dashboardCtrl);

    dashboardCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl', 'localStorageService'];

    function dashboardCtrl($scope, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl, localStorageService) {

        $scope.stats = {};
        $scope.getServiceDeskStatistics = getServiceDeskStatistics;
                

        function getServiceDeskStatistics()
        {
            modelService.getServiceDeskStatistics($scope.userId, getServiceDeskStatisticsCompleted, getServiceDeskStatisticsFailed);
        }

        function getServiceDeskStatisticsCompleted(response)
        {
            $scope.stats = response.data;
        }

        function getServiceDeskStatisticsFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.setIsDashboardActive(true);
        $scope.getServiceDeskStatistics();
        $scope.checkIsUserLoggedIn();
    }

})(angular.module('common.core'));