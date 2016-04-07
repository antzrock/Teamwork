(function (app) {
    'use strict';

    app.controller('weeklyReportCtrl', weeklyReportCtrl);

    weeklyReportCtrl.$inject = ['$scope', '$routeParams', 'membershipService', 'notificationService', '$rootScope', '$location', 'modelService', 'baseUrl', '$filter', '$timeout', 'projectService', '$modal', 'weeklyReportService']

    function weeklyReportCtrl($scope, $routeParams, membershipService, notificationService, $rootScope, $location, modelService, baseUrl, $filter, $timeout, projectService, $modal, weeklyReportService) {

        $scope.selectedWeekData = {};
        $scope.initialPageLoad = 1;
        $scope.getSelectedWeekData = getSelectedWeekData;
        $scope.selectedWeekFilter = {};
        $scope.yearList = [];
        $scope.getYearList = getYearList;
        $scope.selectedYear = 0;
        $scope.yearFilterSelected = yearFilterSelected;
        $scope.weekNoList = [];
        $scope.projectsList = [];
        $scope.selectedProject = {};
        $scope.getWeekNoList = getWeekNoList;
        $scope.weekFilterSelected = weekFilterSelected;
        $scope.serviceRequestStatusClass = serviceRequestStatusClass;
        $scope.projectTaskCategoryClass = projectTaskCategoryClass;
        $scope.projectTaskStatusClass = projectTaskStatusClass;
        $scope.showMoreProjectTaskDetails = showMoreProjectTaskDetails;
        $scope.getAllProjects = getAllProjects;
        $scope.projectFilterSelected = projectFilterSelected;
        $scope.getWeeklyReport = getWeeklyReport;
        $scope.currentReport = {};
        $scope.showKeyAchievementWeekNo = showKeyAchievementWeekNo;
        $scope.removeKeyAchievementWeekNo = removeKeyAchievementWeekNo;
        $scope.addKeyAchievement = addKeyAchievement;
        $scope.showRiskWeekNo = showRiskWeekNo;
        $scope.removeRisk = removeRisk;
        $scope.addRisk = addRisk;
        $scope.showImpedimentWeekNo = showImpedimentWeekNo;
        $scope.removeImpediment = removeImpediment;
        $scope.addImpediment = addImpediment;
        $scope.showPlannedActivitiesWeekNo = showPlannedActivitiesWeekNo;
        $scope.removePlannedActivity = removePlannedActivity;
        $scope.addPlannedActivity = addPlannedActivity;
        $scope.saveReportDetails = saveReportDetails;
        $scope.keyAchievementsHasError = false;
        $scope.risksHasError = false;
        $scope.impedimentsHasError = false;
        $scope.plannedActivitiesHasError = false;
        $scope.viewPdfWeeklyReport = viewPdfWeeklyReport;
        $scope.hideServiceDeskFilter = true;
        $scope.toggleServiceDeskFilter = toggleServiceDeskFilter;
        $scope.hideServiceDeskDetails = true;
        $scope.toggleHideServiceDeskDetails = toggleHideServiceDeskDetails;

        $scope.selectedReportMenuOption = 'View Report';
        $scope.setSelectedReportMenuOption = setSelectedReportMenuOption;

        function setSelectedReportMenuOption(opt)
        {
            $scope.selectedReportMenuOption = opt;

            if (opt === 'View Report')
            {
                $scope.viewPdfWeeklyReport();
            }
        }


        function toggleHideServiceDeskDetails()
        {
            $scope.hideServiceDeskDetails = $scope.hideServiceDeskDetails == false ? true : false;
        }
        
        function toggleServiceDeskFilter()
        {
            $scope.hideServiceDeskFilter = $scope.hideServiceDeskFilter === false ? true : false;
        }

        function viewPdfWeeklyReport()
        {
            notificationService.displayInfo("Please wait while your report is being rendered.")
            weeklyReportService.ViewPdfWeeklyReport($scope.currentReport.WeekReportHeaderID, ViewPdfWeeklyReportCompleted, requestFailed);
        }

        function ViewPdfWeeklyReportCompleted(response)
        {
            try
            {
                var file = new Blob([response.data], { type: 'application/pdf' });
                var today = new Date();
                var filename = $scope.defaultGroup.Code + " " + $scope.defaultTeam.Code + " Weekly Report - " + moment(today).format("MMDDYYYY") + " - " + $scope.nickname + ".pdf";
                //var filename = "sheet.pdf";

                var doc = window.document;
                var msie = document.documentMode;

                if (msie)
                {
                    if (navigator.msSaveBlob)
                        navigator.msSaveBlob(file, filename);
                    else {
                        // Try using other saveBlob implementations, if available
                        var saveBlob = navigator.webkitSaveBlob || navigator.mozSaveBlob || navigator.saveBlob;
                        if (saveBlob === undefined) throw "Not supported";
                        saveBlob(blob, filename);
                    }
                }
                else
                {
                    var fileURL = URL.createObjectURL(file);
                    window.open(fileURL);
                }
            }
            catch(ex)
            {
                notificationService.displayError("Error in downloading or rendering PDF file in browser!");
            }
        }

        function saveReportDetails()
        {
            //Check for key achievement errors...
            $scope.keyAchievementsHasError = false;
            var doKeyAchievementCheck = true;

            angular.forEach($scope.currentReport.CurrentDetail.KeyAchievements, function (ka) {
                
                if (doKeyAchievementCheck)
                {
                    if (!ka.Description || !angular.isNumber(ka.PercentComplete))
                    {
                        $scope.keyAchievementsHasError = true;
                        doKeyAchievementCheck = false;
                    }
                }

            });
            
            if ($scope.keyAchievementsHasError)
            {
                notificationService.displayError("Key achievements has errors. Please check!");
            }

            //Check for risk errors...
            $scope.risksHasError = false;
            var doRiskCheck = true;

            angular.forEach($scope.currentReport.CurrentDetail.Risks, function (rsk) {
                if (doRiskCheck)
                {
                    if (!rsk.Description || !rsk.Effect || !angular.isNumber(rsk.PercentLikelihood))
                    {
                        $scope.risksHasError = true;
                        doRiskCheck = false;
                    }
                }
            });

            if ($scope.risksHasError)
            {
                notificationService.displayError("Risks has errors. Please check!");
            }

            //Check for impediments errors...
            $scope.impedimentsHasError = false;
            var doImpedimentsCheck = true;

            angular.forEach($scope.currentReport.CurrentDetail.Impediments, function (imp) {
                if (doImpedimentsCheck) {
                    if (!imp.Description || !imp.Effect) {
                        $scope.impedimentsHasError = true;
                        doImpedimentsCheck = false;
                    }
                }
            });

            if ($scope.impedimentsHasError)
            {
                notificationService.displayError("Impediments has errors. Please check!");
            }

            //Check for planned activities error...
            $scope.plannedActivitiesHasError = false;
            var doPlannedActivitiesCheck = true;

            angular.forEach($scope.currentReport.CurrentDetail.PlannedActivities, function (pa) {
                if (doPlannedActivitiesCheck) {
                    if (!pa.Description) {
                        $scope.plannedActivitiesHasError = true;
                        doPlannedActivitiesCheck = false;
                    }
                }
            });

            if ($scope.plannedActivitiesHasError)
            {
                notificationService.displayError("Planned activities has errors. Please check!");
            }

            if (!$scope.keyAchievementsHasError && !$scope.risksHasError && !$scope.impedimentsHasError && !$scope.plannedActivitiesHasError)
            {
                weeklyReportService.saveReportDetails($scope.currentReport, saveReportDetailsCompleted, requestFailed);
            }
                        
        }

        function saveReportDetailsCompleted(response)
        {
            if (response.data.success) {
                notificationService.displaySuccess("Weekly Report detail updated!");

                //Refresh weekly report detail...
                $scope.getWeeklyReport();
            }
            else {
                notificationService.displayError(response.data);
            }
        }

        // Planned Activities START
        function showPlannedActivitiesWeekNo(pa)
        {
            if (pa.PlannedActivityID && $scope.weekNoList.length) {
                var selected = $filter('filter')($scope.weekNoList, { TargetWeekNo: pa.TargetWeekNo });
                return selected.length ? selected[0].TargetWeekNo : 'Week no is required';
            } else {
                return pa.TargetWeekNo || 'Week no is required';
            }
        }

        function removePlannedActivity(index)
        {
            $scope.currentReport.CurrentDetail.PlannedActivities.splice(index, 1);
        }

        function addPlannedActivity()
        {
            $scope.inserted = {
                id: $scope.currentReport.CurrentDetail.PlannedActivities.length + 1,
                PlannedActivityID: 0,
                TargetWeekNo: $scope.selectedWeekFilter.WeekNo,
                Description: ''
            };

            $scope.currentReport.CurrentDetail.PlannedActivities.push($scope.inserted);
        }

        // Planned Activities END

        // Impediments START
        function showImpedimentWeekNo(imp)
        {
            if (imp.ImpedimentID && $scope.weekNoList.length) {
                var selected = $filter('filter')($scope.weekNoList, { WeekNo: imp.WeekNo });
                return selected.length ? selected[0].WeekNo : 'Week no is required';
            } else {
                return imp.WeekNo || 'Week no is required';
            }
        }

        function removeImpediment(index)
        {
            $scope.currentReport.CurrentDetail.Impediments.splice(index, 1);
        }

        function addImpediment()
        {
            $scope.inserted = {
                id: $scope.currentReport.CurrentDetail.Impediments.length + 1,
                ImpedimentID: 0,
                WeekNo: $scope.selectedWeekFilter.WeekNo,
                Description: '',
                Effect: ''
            };

            $scope.currentReport.CurrentDetail.Impediments.push($scope.inserted);
        }

        // Impediments END

        //Key Achievements START

        function addKeyAchievement()
        {
            $scope.inserted = {
                id: $scope.currentReport.CurrentDetail.KeyAchievements.length + 1,
                KeyAchievementID: 0,
                WeekNo: $scope.selectedWeekFilter.WeekNo,
                Description: '',
                PercentComplete: 0.00
            };

            $scope.currentReport.CurrentDetail.KeyAchievements.push($scope.inserted);
        }

        function removeKeyAchievementWeekNo(index)
        {
            $scope.currentReport.CurrentDetail.KeyAchievements.splice(index, 1);
        }

        function showKeyAchievementWeekNo(ka)
        {
            if (ka.KeyAchievementID && $scope.weekNoList.length) {
                var selected = $filter('filter')($scope.weekNoList, { WeekNo: ka.WeekNo });
                return selected.length ? selected[0].WeekNo : 'Week no is required True';
            } else {
                return ka.WeekNo || 'Week no is required False';
            }
        }

        //Key Achievements END

        // Risk START
        function addRisk()
        {
            $scope.inserted = {
                id: $scope.currentReport.CurrentDetail.Risks.length + 1,
                RiskID: 0,
                WeekNo: $scope.selectedWeekFilter.WeekNo,
                Description: '',
                Effect: '',
                PercentLikelihood: 0.00
            };

            $scope.currentReport.CurrentDetail.Risks.push($scope.inserted);
        }


        function removeRisk(index)
        {
            $scope.currentReport.CurrentDetail.Risks.splice(index, 1);
        }

        function showRiskWeekNo(rsk)
        {
            if (rsk.RiskID && $scope.weekNoList.length) {
                var selected = $filter('filter')($scope.weekNoList, { WeekNo: rsk.WeekNo });
                return selected.length ? selected[0].WeekNo : 'Week no is required';
            } else {
                return rsk.WeekNo || 'Week no is required';
            }
        }

        // Risk END

        function getWeeklyReport()
        {
            var yearSelected = $scope.weekData.Year;
            var weekSelected = $scope.weekData.WeekNo;

            if ($scope.selectedWeekFilter.Year)
            {
                yearSelected = $scope.selectedWeekFilter.Year;
            }

            if ($scope.selectedWeekFilter.WeekNo)
            {
                weekSelected = $scope.selectedWeekFilter.WeekNo;
            }

            weeklyReportService.getWeeklyReport(yearSelected, weekSelected, $scope.userId, $scope.selectedProject.ProjectID, getWeeklyReportCompleted, requestFailed);
        }

        function getWeeklyReportCompleted(response)
        {
            $scope.currentReport = response.data;
        }

        function projectFilterSelected(proj)
        {
            $scope.selectedProject = proj;
            $scope.getWeeklyReport();
        }

        function getAllProjects()
        {
            projectService.getAllProjects($scope.userId, getAllProjectsCompleted, requestFailed);
        }

        function getAllProjectsCompleted(response)
        {
            $scope.projectsList = response.data;

            if ($scope.projectsList.length > 0)
            {
                $scope.selectedProject = $scope.projectsList[0];
                $scope.getWeeklyReport();
            }
        }

        function projectTaskStatusClass(stsStr)
        {
            var stsTxtClass;

            switch (stsStr) {
                case "Not started":
                    stsTxtClass = "color10";
                    break;
                case "In progress":
                    stsTxtClass = "color7";
                    break;
                case "Finished":
                    stsTxtClass = "color5";
                    break;
                default:
                    stsTxtClass = "color10";
            }

            return stsTxtClass;
        }

        function projectTaskCategoryClass(catStr)
        {
            var catTxtClass;

            switch (catStr) {
                case "Specification":
                    catTxtClass = "color14";
                    break;
                case "Test":
                    catTxtClass = "color10";
                    break;
                case "Design":
                    catTxtClass = "color9";
                    break;
                case "Implementation":
                    catTxtClass = "color5";
                    break;
                case "Support":
                    catTxtClass = "color11";
                    break;
                case "Installation":
                    catTxtClass = "color15";
                    break;
                case "Upgrade":
                    catTxtClass = "color7";
                    break;
                case "Backup":
                    catTxtClass = "color13";
                    break;
                default:
                    statusTxtClass = "color14";
            }

            return catTxtClass;
        }

        function serviceRequestStatusClass(statusStr)
        {
            var statusTxtClass;

            switch(statusStr) {
                case "New":
                    statusTxtClass = "color7";
                    break;
                case "Open":
                    statusTxtClass = "color10";
                    break;
                case "Closed":
                    statusTxtClass = "color2";
                    break;
                case "Verified Closed":
                    statusTxtClass = "color1";
                    break;
                case "Pending":
                    statusTxtClass = "color7";
                    break;
                case "Postponed":
                    statusTxtClass = "color3";
                    break;
                case "Deleted":
                    statusTxtClass = "color3";
                    break;
                case "Change Approved":
                    statusTxtClass = "color8";
                    break;
                case "Change Rejected":
                    statusTxtClass = "color2";
                    break;
                case "Pending Problem resolution":
                    statusTxtClass = "color8";
                    break;
                case "Request opened and being analyzed":
                    statusTxtClass = "color14";
                    break;
                case "Request Rejected":
                    statusTxtClass = "color15";
                    break;
                case "Awaiting User Response":
                    statusTxtClass = "color7";
                    break;
                case "Support is Working":
                    statusTxtClass = "color9";
                    break;
                case "User Responded":
                    statusTxtClass = "color7";
                    break;
                case "Duplicate item":
                    statusTxtClass = "color11";
                    break;
                case "Request Completed":
                    statusTxtClass = "color6";
                    break;
                default:
                    statusTxtClass = "color5";
            }

            return statusTxtClass;
        }

        // DATE PICKER COMMON START
        $scope.formats = ['MMMM dd, yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.sysAidDateFormat = $scope.formats[0];
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        // DATE PICKER COMMON END
        
        // SYSAID TICKETS START

        // START DATE  START
        $scope.sysAidStartDateOpen = sysAidStartDateOpen;
        $scope.sysAidStarDateOpened = false;

        function sysAidStartDateOpen($event)
        {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.sysAidStarDateOpened = true;
        }

        $scope.sysAidDateFilter = {}
        $scope.sysAidStartDate = Date.now();
        // START DATE END

        // END DATE START
        $scope.sysAidEndDateOpen = sysAidEndDateOpen;
        $scope.sysAidEndDateOpened = false;

        function sysAidEndDateOpen($event)
        {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.sysAidEndDateOpened = true;
        }

        $scope.sysAidEndDate = Date.now();
        // END DATE END
        
        $scope.getSysAidTickets = getSysAidTickets;
        $scope.sysAidRequests = [];
        $scope.sysAidIncidents = [];
        $scope.sysAidProblems = [];
        $scope.sysAidProjectTasks = [];
        $scope.showMoreServiceRequestDetails = showMoreServiceRequestDetails;
        
        function getSysAidTickets()
        {
            $scope.sysAidDateFilter.sysAidStartDate = moment($scope.sysAidStartDate).format("YYYY-MM-DD");
            $scope.sysAidDateFilter.sysAidEndDate = moment($scope.sysAidEndDate).format("YYYY-MM-DD");

            modelService.getSysAidRequests($scope.userId, $scope.sysAidDateFilter, getSysAidRequestsCompleted, requestFailed);
            modelService.getSysAidIncidents($scope.userId, $scope.sysAidDateFilter, getSysAidIncidentsCompleted, requestFailed)
            modelService.getSysAidProblems($scope.userId, $scope.sysAidDateFilter, getSysAidProblemsCompleted, requestFailed)
            modelService.getSysAidProjectsTasks($scope.userId, $scope.sysAidDateFilter, getSysAidProjectsTasksCompleted, requestFailed)
        }

        function getSysAidProjectsTasksCompleted(response)
        {
            $scope.sysAidProjectTasks = response.data;
        }

        function getSysAidProblemsCompleted(response)
        {
            $scope.sysAidProblems = response.data;
        }

        function getSysAidIncidentsCompleted(response)
        {
            $scope.sysAidIncidents = response.data;
        }

        function getSysAidRequestsCompleted(response)
        {
            $scope.sysAidRequests = response.data;
        }

        // SYSAID TICKETS END

        function weekFilterSelected(item)
        {
            $scope.selectedWeekFilter.WeekNo = item;
            $scope.getSelectedWeekData();
        }

        function yearFilterSelected(item)
        {
            $scope.selectedWeekFilter.Year = item;
            $scope.getWeekNoList();
            $scope.getSelectedWeekData();
        }
        
        function getWeekNoList()
        {
            modelService.getWeekNoList($scope.selectedWeekFilter.Year, getWeeksListCompleted, requestFailed);
        }

        function getWeeksListCompleted(response)
        {
            $scope.weekNoList = response.data;
        }

        function getYearList()
        {
            modelService.getYearList(getYearListCompleted, requestFailed);
        }

        function getYearListCompleted(response)
        {
            $scope.yearList = response.data;
        }

        function getSelectedWeekData()
        {
            if ($scope.initialPageLoad == 1)
            {
                $scope.initialPageLoad = 0;
                //Get current week data...
                modelService.loadWeekData(getSelectedWeekDataCompleted, requestFailed);
            }
            else
            {
                //Get selected week data...
                modelService.getSpecificWeekData($scope.selectedWeekFilter, getSpecificWeekDataCompleted, requestFailed);
            }
        }

        function getSpecificWeekDataCompleted(response)
        {
            $scope.selectedWeekData = response.data;

            // Set filter...
            $scope.selectedWeekFilter.Year = response.data.Year;
            $scope.selectedWeekFilter.WeekNo = response.data.WeekNo;

            // Set SysAid dates filter...
            $scope.sysAidStartDate = response.data.StartDate;
            $scope.sysAidEndDate = response.data.EndDate;
            $scope.getSysAidTickets();
            $scope.getWeeklyReport();
        }

        function getSelectedWeekDataCompleted(response)
        {
            $scope.selectedWeekData = response.data;

            // Set filter...
            $scope.selectedWeekFilter.Year = response.data.Year;
            $scope.selectedWeekFilter.WeekNo = response.data.WeekNo;

            $scope.getWeekNoList();

            // Set SysAid dates filter...
            $scope.sysAidStartDate = response.data.StartDate;
            $scope.sysAidEndDate = response.data.EndDate;
            $scope.getSysAidTickets();
           
        }

        function requestFailed(response)
        {
            notificationService.displayError(response.data);
        }

        // PROJECT TASK MODAL START
        function showMoreProjectTaskDetails(task)
        {
            var modalInstance = $modal.open({
                templateUrl: 'projectTaskModal.html',
                controller: projectTaskModalCtrl,
                scope: $scope,
                backdrop: "static",
                resolve: {
                    task: function ()
                    {
                        return task;
                    }
                }
            });

            return false;
        }

        // PROJECT TASK MODAL END

        // SERVICE REQUEST MODAL START
        function showMoreServiceRequestDetails(request, type) {

            var modalInstance = $modal.open({
                templateUrl: 'serviceRequestModal.html',
                controller: serviceRequestModalCtrl,
                scope: $scope,
                backdrop: "static",
                resolve: {
                    type: function ()
                    {
                        return type;
                    },
                    request: function ()
                    {
                        return request;
                    }
                }
            });

            return false;
        }

        // SERVICE REQUEST MODAL END

        $scope.setIsWeeklyReportActive(true);
        $scope.checkIsUserLoggedIn();
        $scope.getYearList();
        $scope.getSelectedWeekData();
        $scope.getAllProjects();
    }

    var serviceRequestModalCtrl = function ($scope, $modalInstance, request, type) {
        $scope.request = request;
        $scope.type = type;

        $scope.ok = function () {
            $modalInstance.dismiss('cancel');
        };
    }

    var projectTaskModalCtrl = function ($scope, $modalInstance, task)
    {
        $scope.task = task;

        $scope.ok = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})(angular.module('common.core'));

