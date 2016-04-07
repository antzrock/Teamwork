using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WEEKLY.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/Vendors/angular.js",
                "~/Scripts/Vendors/angular-route.js",
                "~/Scripts/Vendors/angular-cookies.js",
                "~/Scripts/Vendors/angular-validator.js",
                "~/Scripts/Vendors/angular-base64.js",
                "~/Scripts/Vendors/angular-animate.js",
                "~/Scripts/Vendors/angular-sanitize.js",
                "~/Scripts/Vendors/angucomplete-alt.min.js",
                "~/Scripts/Vendors/ui-bootstrap-tpls-0.13.1.js",
                "~/Scripts/Vendors/toaster.js",
                "~/Scripts/Vendors/select.js",
                "~/Scripts/Vendors/ngletteravatar.js",
                "~/Scripts/Vendors/ng-flow-standalone.js",
                "~/Scripts/Vendors/smart-table.js",
                "~/Scripts/Vendors/xeditable.js",
                "~/Scripts/Vendors/checklist-model.js",
                "~/Scripts/Vendors/moment.js",
                "~/Scripts/Vendors/angular-bootstrap-checkbox.js",
                "~/Scripts/Vendors/angular-local-storage.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/services/modelService.js",
                "~/Scripts/spa/services/projectService.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/paging.directive.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/dashboard/dashboardCtrl.js",
                "~/Scripts/spa/account/forgotpasswordCtrl.js",
                "~/Scripts/spa/account/profilesettingsCtrl.js",
                "~/Scripts/spa/account/changepasswordCtrl.js",
                "~/Scripts/spa/project/registerProjectCtrl.js",
                "~/Scripts/spa/project/homeProjectCtrl.js",
                "~/Scripts/spa/weeklyreport/weeklyReportCtrl.js",
                "~/Scripts/spa/group/groupCtrl.js",
                "~/Scripts/spa/common/blankTarget.directive.js",
                "~/Scripts/spa/services/weeklyReportService.js",
                "~/Scripts/spa/roles/roleCtrl.js",
                "~/Scripts/spa/group/groupRegisterCtrl.js",
                "~/Scripts/spa/team/teamHomeCtrl.js",
                "~/Scripts/spa/team/teamRegisterCtrl.js",
                "~/Scripts/spa/project/productBacklogCtrl.js",
                "~/Scripts/spa/group/groupMaintenanceCtrl.js",
                "~/Scripts/spa/team/teamMaintenanceCtrl.js",
                "~/Scripts/spa/account/userMaintenanceCtrl.js"
                ));

            /*bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/services/fileUploadService.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/customPager.directive.js",
                "~/Scripts/spa/directives/rating.directive.js",
                "~/Scripts/spa/directives/availableMovie.directive.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",
                "~/Scripts/spa/customers/customersCtrl.js",
                "~/Scripts/spa/customers/customersRegCtrl.js",
                "~/Scripts/spa/customers/customerEditCtrl.js",
                "~/Scripts/spa/movies/moviesCtrl.js",
                "~/Scripts/spa/movies/movieAddCtrl.js",
                "~/Scripts/spa/movies/movieDetailsCtrl.js",
                "~/Scripts/spa/movies/movieEditCtrl.js",
                "~/Scripts/spa/controllers/rentalCtrl.js",
                "~/Scripts/spa/rental/rentMovieCtrl.js",
                "~/Scripts/spa/rental/rentStatsCtrl.js"
                ));*/

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/awesome-bootstrap-checkbox.css",
                "~/content/css/bootstrap.css",
                "~/content/css/font-awesome.min.css",
                "~/content/css/root.css",
                "~/content/css/responsive.css",
                "~/content/css/shortcuts.css",
                "~/content/css/style.css",
                "~/content/css/toaster.css",
                "~/content/css/select.css",
                "~/content/css/xeditable.css",
                "~/content/css/summernote-bs3.css",
                "~/content/css/datatables.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}