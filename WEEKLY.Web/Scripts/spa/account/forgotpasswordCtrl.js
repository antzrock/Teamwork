(function (app) {
    'use strict';

    app.controller('forgotpasswordCtrl', forgotpasswordCtrl);

    forgotpasswordCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl'];

    function forgotpasswordCtrl($scope, membershipService, notificationService, $rootScope, $location, modelService, baseUrl) {
        $scope.resetPassword = resetPassword;
        $scope.user = {};

        function resetPassword()
        {
            notificationService.displayPleaseWaitInfo();
            $scope.user.HomeUrl = baseUrl.web + "/";
            membershipService.resetPassword($scope.user, resetPasswordCompleted, resetPasswordFailed);
        }

        function resetPasswordCompleted(response)
        {
            if (response.data.success)
            {
                notificationService.displaySuccess('Password reset done! Please check your email for your new password!');
            }
            else
            {
                notificationService.displayError(response.data);
            }
        }

        function resetPasswordFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.setHideTopBar(true);
        $scope.setHideSideBar(true);
        $scope.setIsUserLoggedIn(false);
    }

})(angular.module('common.core'));