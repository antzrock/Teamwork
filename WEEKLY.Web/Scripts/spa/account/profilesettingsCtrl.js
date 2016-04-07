(function (app) {
    'use strict';

    app.controller('profilesettingsCtrl', profilesettingsCtrl);

    profilesettingsCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService'];

    function profilesettingsCtrl($scope, membershipService, notificationService, $rootScope, $location, modelService) {
        $scope.user = {};
        $scope.getProfileForUpdate = getProfileForUpdate;
        $scope.updateProfile = updateProfile;
        $scope.avatarpicUploaded = avatarpicUploaded;
        $scope.profilepicUploaded = profilepicUploaded;
        $scope.avatarFlow = {};
        $scope.currentUsername = '';
        
        function profilepicUploaded($file, $message, $flow) {
            notificationService.displayInfo("Profile picture: " + $file.name + " Uploaded!");
            $scope.user.ProfilePicPath = $file.name;
        }

        function avatarpicUploaded( $file, $message, $flow )
        {
            notificationService.displayInfo("Avatar picture: " + $file.name + " Uploaded!");
            $scope.user.AvatarPicPath = $file.name;
        }


        function updateProfile()
        {
            membershipService.updateProfile($scope.user, updateProfileCompleted, updateProfileFailed);
        }

        function updateProfileCompleted(response)
        {
            if (response.data.success)
            {
                notificationService.displaySuccess("User profile updated!");

                if ($scope.currentUsername !== $scope.user.Username)
                {
                    //Username changed force user to login using new credentials...
                    $scope.logout();
                }
                else
                {
                    membershipService.getUserProfile($scope.userId, getProfileForUpdateCompleted, getProfileForUpdateFailed);
                }
            }
            else
            {
                notificationService.displayError("User profile update failed!");
            }
        }

        function updateProfileFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function getProfileForUpdate()
        {
            notificationService.displayPleaseWaitInfo();
            membershipService.getUserProfile($scope.userId, getProfileForUpdateCompleted, getProfileForUpdateFailed);
        }

        function getProfileForUpdateCompleted(response)
        {
            $scope.user = response.data;
            //Save current username in scope variable...
            $scope.currentUsername = $scope.user.Username;
            $scope.refreshUserInfo($scope.user.UserID, $scope.user.Fullname, $scope.user.AvatarPicPath, $scope.user.ProfilePicPath, $scope.user.ProfileQuote, $scope.user.Nickname);
        }

        function getProfileForUpdateFailed(response)
        {
            notificationService.displayError(response.data);
        }

        $scope.checkIsUserLoggedIn();
        $scope.getProfileForUpdate();
    }

})(angular.module('common.core'));