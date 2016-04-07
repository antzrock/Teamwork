(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'localStorageService'];

    function loginCtrl($scope, membershipService, notificationService, $rootScope, $location, localStorageService) {
        $scope.pageClass = 'page-login';
        $scope.login = login;
        $scope.user = {};

        function login() {
            notificationService.displayPleaseWaitInfo();
            membershipService.login($scope.user, loginCompleted)
        }

        function loginCompleted(result) {
            if (result.data.success) {
                membershipService.saveCredentials($scope.user);
                notificationService.displaySuccess('Hello ' + result.data.nickname);

                $scope.userData.displayUserInfo(result.data.userid, result.data.fullname, result.data.avatarpicpath, result.data.profilepicpath, result.data.profilequote, result.data.nickname);
                $scope.userData.setUserPermissions(result.data.appPermissions, result.data.groupPermissions, result.data.teamPermissions, result.data.projectPermissions, result.data.isUserAdmin);
                $scope.userData.setDefaultGroup(result.data.defaultGroup);
                $scope.userData.setDefaultTeam(result.data.defaultTeam);
                $scope.checkIsUserLoggedIn();
                                
                if ($rootScope.previousState)
                    $location.path($rootScope.previousState);
                else
                    $location.path('/dashboard');
            }
            else {
                notificationService.displayError('Login failed. Try again.');
            }
        }

        $scope.checkIsUserLoggedInForLoginPage();
    }

})(angular.module('common.core'));