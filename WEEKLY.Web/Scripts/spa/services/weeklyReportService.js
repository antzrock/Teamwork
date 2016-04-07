(function (app) {
    'use strict';

    app.factory('weeklyReportService', weeklyReportService);

    weeklyReportService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function weeklyReportService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {
        var service = {
            getWeeklyReport: getWeeklyReport,
            saveReportDetails: saveReportDetails,
            ViewPdfWeeklyReport: ViewPdfWeeklyReport
        }

        function ViewPdfWeeklyReport(headerId, completed, failed)
        {
            apiService.getBlob("api/report/weekly/pdf/" + headerId, { responseType: 'arraybuffer' }, completed, failed);
            //apiService.downloadFile("api/report/weekly/pdf/" + headerId);
        }

        function saveReportDetails(reportHeader, completed, failed)
        {
            apiService.post("api/report/weekly/register", reportHeader, completed, failed);
        }

        function getWeeklyReport(yearNo, weekNo, userId, projectId, completed, failed)
        {
            apiService.get("api/report/weekly/" + yearNo + "/" + weekNo + "/" + userId + "/" + projectId, null, completed, failed);
        }

        return service;
    }

})(angular.module('common.core'));