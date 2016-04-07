(function (app) {
    'use strict';

    app.controller('groupCtrl', groupCtrl);

    groupCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'projectService', 'baseUrl'];

    function groupCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, projectService, baseUrl) {

        $scope.group = {};
        $scope.getExistingGroup = getExistingGroup;
        $scope.teamLogoUrl = '';

        function getExistingGroup()
        {
            modelService.loadGroup($routeParams.id, getExistingGroupCompleted, requestFailed);
        }

        function getExistingGroupCompleted(response)
        {
            $scope.group = response.data;
            $scope.teamLogoUrl = baseUrl.web + "Content/images/team_default_icon.png" + "?_ts=" + new Date().getTime();

            angular.forEach($scope.group.Members, function (m) {
                m.AvatarPicPath = baseUrl.web + "Content/images/profile/" + m.AvatarPicPath + "?_ts=" + new Date().getTime();
            });

            angular.forEach($scope.group.Projects, function (t) {
                t.LogoPath = baseUrl.web + "Content/images/project/" + t.LogoPath + "?_ts=" + new Date().getTime();
            });
        }

        function requestFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.setShowMyGroups(true);
        $scope.checkIsUserLoggedIn();
        $scope.getExistingGroup();
    }

})(angular.module('common.core'));