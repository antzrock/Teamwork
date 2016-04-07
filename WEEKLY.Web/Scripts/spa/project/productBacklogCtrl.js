(function (app) {
    'use strict';

    app.controller('productBacklogCtrl', productBacklogCtrl);

    productBacklogCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl', '$filter', '$timeout', 'projectService'];

    function productBacklogCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, baseUrl, $filter, $timeout, projectService) {
        $scope.productBacklog = {};
        $scope.getExistingProject = getExistingProject;

        function getExistingProject()
        {
            projectService.getProjectProductBacklog($routeParams.id, getExistingProjectCompleted, requestFailed);
        }

        function getExistingProjectCompleted(response)
        {
            $scope.productBacklog = response.data;
        }

        function requestFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.getExistingProject();
    }
})(angular.module('common.core'));