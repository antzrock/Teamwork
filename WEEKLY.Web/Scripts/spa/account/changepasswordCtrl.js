(function (app) {
    'use strict';

    app.controller('changePasswordCtrl', changePasswordCtrl);

    changePasswordCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl'];

    function changePasswordCtrl($scope, membershipService, notificationService, $rootScope, $location, modelService, baseUrl) {
        $scope.user = {};
        $scope.changeOldPassword = changeOldPassword;
        $scope.getChangePasswordViewModelForUpdate = getChangePasswordViewModelForUpdate;
        
        function getChangePasswordViewModelForUpdate()
        {
            membershipService.getChangePasswordViewModelForUpdate($scope.userId, getChangePasswordViewModelForUpdateCompleted, getChangePasswordViewModelForUpdateFailed);
        }
        
        function getChangePasswordViewModelForUpdateCompleted(response)
        {
            $scope.user = response.data;
        }

        function getChangePasswordViewModelForUpdateFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function changeOldPassword()
        {
            notificationService.displayPleaseWaitInfo();
            membershipService.changePassword($scope.user, changePasswordCompleted, changePasswordFailed);
        }

        function changePasswordCompleted(response)
        {
            if (response.data.success) {
                //Password changed force user to login using new credentials...
                $scope.logout();
                notificationService.displaySuccess("User password updated!");
            }
            else {
                notificationService.displayError("User password update failed!");
            }
        }

        function changePasswordFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.checkIsUserLoggedIn();
        $scope.getChangePasswordViewModelForUpdate();
        
    }

})(angular.module('common.core'));