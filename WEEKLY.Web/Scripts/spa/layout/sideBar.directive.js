(function (app) {
    'use strict';

    app.directive('sideBar', sideBar);

    function sideBar() {
        return {
            restrict: 'E',
            replace: true,
            scope: true,
            templateUrl: '/scripts/spa/layout/sideBar.html'
        }
    }

})(angular.module('common.ui'));