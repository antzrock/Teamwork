(function (app) {
    'use strict';

    app.controller('teamHomeCtrl', teamHomeCtrl);

    teamHomeCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function teamHomeCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {

        $scope.team = {};
        $scope.getExistingTeam = getExistingTeam;

        function getExistingTeam()
        {
            modelService.loadTeam($routeParams.id, getExistingTeamCompleted, requestFailed);
        }

        function getExistingTeamCompleted(response)
        {
            $scope.team = response.data;

            angular.forEach($scope.team.Members, function (m) {
                m.AvatarPicPath = baseUrl.web + "Content/images/profile/" + m.AvatarPicPath + "?_ts=" + new Date().getTime();
            });
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.setDoShowMyTeams(true);
        $scope.checkIsUserLoggedIn();
        $scope.getExistingTeam();
    }
})(angular.module('common.core'));