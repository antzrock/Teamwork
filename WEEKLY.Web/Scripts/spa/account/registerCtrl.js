(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService'];

    function registerCtrl($scope, membershipService, notificationService, $rootScope, $location, modelService) {
        $scope.pageClass = 'page-login';
        $scope.register = register;
        $scope.isUsernameTaken = isUsernameTaken;
        $scope.user = {};
        $scope.groups = [];
        $scope.loadGroups = loadGroups;
        $scope.onGroupSelect = onGroupSelect;
        $scope.teams = [];
                                
        function onGroupSelect($item, $model, $label) {
            modelService.loadTeams($item.GroupID, loadTeamsCompleted);
        }

        function loadTeamsCompleted(response) {
            $scope.teams = [];
            $scope.teams = response.data;
        }

        function loadGroups()
        {
            modelService.loadGroups(loadGroupsCompleted);
        }

        function loadGroupsCompleted(response)
        {
            $scope.groups = response.data;
        }
        

        function register() {
            notificationService.displayPleaseWaitInfo();
            membershipService.register($scope.user, registerCompleted)
        }

        function registerCompleted(result) {
            if (result.data.success) {
                //membershipService.saveCredentials($scope.user);
                //notificationService.displaySuccess('Hello ' + $scope.user.Username);
                //$scope.userData.displayUserInfo($scope.user.userName);
                //Let user login....
                $location.path('/');
            }
        }

        function isUsernameTaken() {

            if (!$scope.user.Username)
            {
                membershipService.isUsernameTaken($scope.user.Username, isUsernameTakenCompleted, isUsernameTakenFailed)
            }
        }

        function isUsernameTakenCompleted(response) {
            return false;
        }

        function isUsernameTakenFailed(response)
        {
            return true;
        }

        $scope.setHideTopBar(true);
        $scope.setHideSideBar(true);
        $scope.setIsUserLoggedIn(false);
        $scope.loadGroups();

    }

})(angular.module('common.core'));