(function (app) {
    'use strict';

    app.directive('blankTarget', blankTarget);

    function blankTarget() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var href = element.href;
                if (true) {  // replace with your condition
                    element.attr("target", "_blank");
                }
            }
        }
    }

})(angular.module('common.ui'));