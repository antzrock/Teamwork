(function (app) {
    'use strict';

    app.controller('homeProjectCtrl', homeProjectCtrl);

    function homeProjectCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, baseUrl, $filter, $timeout, projectService) {
        $scope.project = {};
        $scope.isGroupProject = 0;
        $scope.getExistingProject = getExistingProject;
        $scope.displayLogo = baseUrl.web + "Content/images/project/default_project_logo.png?_ts='" + new Date().getTime();
        $scope.checkUserHasPermission = checkUserHasPermission;

        function checkUserHasPermission()
        {
            var hasPermission = false;

            if ($scope.isGroupProject = 1)
            {
                hasPermission = $scope.userHasProjectPermission($scope.project.ProjectID, 'UPDATE_GROUP_PROJECT');
            }
            else
            {
                hasPermission = $scope.userHasProjectPermission($scope.project.ProjectID, 'UPDATE_TEAM_PROJECT');
            }

            return hasPermission;
        }

        function getExistingProject() {
            projectService.getProject($routeParams.id, getExistingProjectCompleted, getExistingProjectFailed);
        }

        function getExistingProjectCompleted(response) {
            $scope.project = response.data;
            $scope.displayLogo = baseUrl.web + "Content/images/project/" + $scope.project.LogoPath + "?_ts='" + new Date().getTime();

            if ($scope.project.isGroupProject)
            {
                $scope.isGroupProject = 1;
                $scope.setShowMyGroupProjects(true);
            }
            else
            {
                $scope.isGroupProject = 0;
                $scope.setDoShowMyTeamProjects(true);
            }

            angular.forEach($scope.project.TeamMembers, function (m) {
                m.AvatarPicUrl = baseUrl.web + "Content/images/profile/" + m.AvatarPicUrl + "?_ts=" + new Date().getTime();
            });
        }

        function getExistingProjectFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.checkIsUserLoggedIn();
        $scope.getExistingProject();
    }
})(angular.module('common.core'));