(function (app) {
    'use strict';

    app.controller('serviceRequestModalCtrl', serviceRequestModalCtrl);

    serviceRequestModalCtrl.$inject = ['$scope', '$modalInstance'];

    function serviceRequestModalCtrl($scope, $uibModalInstance, items)
    {
        $scope.ok = function () {
            $modalInstance.close($scope.selected);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})(angular.module('common.core'));