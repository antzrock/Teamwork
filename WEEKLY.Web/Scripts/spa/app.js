(function () {
    'use strict';

    angular.module('weekly', ['common.core', 'common.ui'])
        .config(config)
        .run(run)
        .run(function (editableOptions) {
            editableOptions.theme = 'bs2';
        })
        .constant('baseUrl', {
            //web: 'http://ledt4xfchz1:52108/',
            web: 'http://localhost:52108/',
        });

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/dashboard", {
                templateUrl: "scripts/spa/dashboard/dashboard.html",
                controller: "dashboardCtrl"
            })
            .when("/forgotpassword", {
                templateUrl: "scripts/spa/account/forgot-password.html",
                controller: "forgotpasswordCtrl"
            })
            .when("/profilesettings", {
                templateUrl: "scripts/spa/account/profile-settings.html",
                controller: "profilesettingsCtrl"
            })
            .when("/changepassword", {
                templateUrl: "scripts/spa/account/change-password.html",
                controller: "changePasswordCtrl"
            })
            .when("/registerproject/:id/:mainProjectId/:isGroupProject", {
                templateUrl: "scripts/spa/project/register-project.html",
                controller: "registerProjectCtrl"
            })
            .when("/project/:id", {
                templateUrl: "scripts/spa/project/project-home.html",
                controller: "homeProjectCtrl"
            })
            .when("/weeklyreport", {
                templateUrl: "scripts/spa/weeklyreport/weekly-report.html",
                controller: "weeklyReportCtrl"
            })
            .when("/roles", {
                templateUrl: "scripts/spa/roles/role.html",
                controller: "roleCtrl"
            })
            .when("/group/:id", {
                templateUrl: "scripts/spa/group/group.html",
                controller: "groupCtrl"
            })
            .when("/group/register/:id", {
                templateUrl: "scripts/spa/group/group-register.html",
                controller: "groupRegisterCtrl"
            })
            .when("/team/:id", {
                templateUrl: "scripts/spa/team/team.html",
                controller: "teamHomeCtrl"
            })
            .when("/team/register/:id", {
                templateUrl: "scripts/spa/team/teamRegister.html",
                controller: "teamRegisterCtrl"
            })
            .when("/project/backlog/:id", {
                templateUrl: "scripts/spa/project/product-backlog.html",
                controller: "productBacklogCtrl"
            })
            .when("/groups", {
                templateUrl: "scripts/spa/group/group-maintenance.html",
                controller: "groupMaintenanceCtrl"
            })
            .when("/teams", {
                templateUrl: "scripts/spa/team/team-maintenance.html",
                controller: "teamMaintenanceCtrl"
            })
            .when("/users", {
                templateUrl: "scripts/spa/account/user-maintenance.html",
                controller: "userMaintenanceCtrl"
            })
            .otherwise({ redirectTo: "/" });


        if (window.history && window.history.pushState) {
            $locationProvider.html5Mode(true);
        }
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        /*$(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });*/
    }

})();