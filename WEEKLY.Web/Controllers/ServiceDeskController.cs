using SysAid.Data.Infrastructure;
using SysAid.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;
using WEEKLY.Web.Infrastructure.Core;
using WEEKLY.Web.Models;

namespace WEEKLY.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin, User")]
    [RoutePrefix("api/servicedesk")]
    public class ServiceDeskController : ApiControllerBase
    {
        private readonly IServiceDeskService _serviceDeskService;
        private readonly IMembershipService _membershipService;
        private readonly IWeekNumberService _weeknumberService;
        private readonly IEntityBaseRepository<Week> _weeksRepository;
        private readonly IDateTimeService _dateTimeService;

        private const int TASK_FINISHED_STATUS = 3;
        private const int ADD_DAYS_YESTERDAY = -1;
        private const int ADD_DAYS_A_WEEK_AGO = -7;
        private const int ADD_DAYS_A_MONTH_AGO = -30;
        private const int WEEK_QUOTA = 40;
        private const int DAY_QUOTA = 8;
        private const int MONTH_QUOTA = 640;

        public ServiceDeskController(IServiceDeskService serviceDeskService, 
                                     IMembershipService membershipService, 
                                     IWeekNumberService weeknumberService, 
                                     IEntityBaseRepository<Week> weeksRepository,
                                     IDateTimeService dateTimeService,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _serviceDeskService = serviceDeskService;
            _membershipService = membershipService;
            _weeknumberService = weeknumberService;
            _weeksRepository = weeksRepository;
            _dateTimeService = dateTimeService;
        }

        [Authorize]
        [HttpGet]
        [Route("projects/tasks")]
        public HttpResponseMessage GetProjectTasksForUser(HttpRequestMessage request, int userId, DateTime startDate, DateTime endDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var existingUser = _membershipService.GetUser(userId);

                if (existingUser == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with ID: " + userId + " not found!");
                }

                string userName = "PASARCORP\\" + existingUser.Email.Split('@')[0];

                List<ProjectTask> userTasks = _serviceDeskService.GetProjectsTasksForUser(userName, _dateTimeService.getStartDate(startDate), _dateTimeService.getEndDate(endDate));

                if (userTasks != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userTasks);
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("problems")]
        public HttpResponseMessage GetProblemsForUser(HttpRequestMessage request, int userId, DateTime startDate, DateTime endDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var existingUser = _membershipService.GetUser(userId);

                if (existingUser == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with ID: " + userId + " not found!");
                }

                string userName = "PASARCORP\\" + existingUser.Email.Split('@')[0];
                
                List<ServiceRequest> userRequests = _serviceDeskService.GetProblemsForUser(userName, _dateTimeService.getStartDate(startDate), _dateTimeService.getEndDate(endDate));

                if (userRequests != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userRequests);
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("incidents")]
        public HttpResponseMessage GetIncidentsForUser(HttpRequestMessage request, int userId, DateTime startDate, DateTime endDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var existingUser = _membershipService.GetUser(userId);

                if (existingUser == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with ID: " + userId + " not found!");
                }

                string userName = "PASARCORP\\" + existingUser.Email.Split('@')[0];
                
                List<ServiceRequest> userRequests = _serviceDeskService.GetIncidentsForUser(userName, _dateTimeService.getStartDate(startDate), _dateTimeService.getEndDate(endDate));

                if (userRequests != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userRequests);
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("requests")]
        public HttpResponseMessage GetRequestsForUser(HttpRequestMessage request, int userId, DateTime startDate, DateTime endDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var existingUser = _membershipService.GetUser(userId);

                if (existingUser == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with ID: " + userId + " not found!");
                }

                string userName = "PASARCORP\\" + existingUser.Email.Split('@')[0];
                
                List<ServiceRequest> userRequests = _serviceDeskService.GetRequestsForUser(userName, _dateTimeService.getStartDate(startDate), _dateTimeService.getEndDate(endDate));

                if (userRequests != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userRequests);
                }

                return response;
            });
        }
        

        [Authorize]
        [HttpGet]
        [Route("stats/today")]
        public HttpResponseMessage GetStatsForToday(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var existingUser = _membershipService.GetUser(userId);

                if (existingUser == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with ID: " + userId + " not found!");
                }

                string userName = "PASARCORP\\" + existingUser.Email.Split('@')[0];
                DateTime dateToday = System.DateTime.Now;
                DateTime dateLastMonth = dateToday.AddDays(ADD_DAYS_A_MONTH_AGO);
                ServiceDeskSummaryViewModel sdVm = new ServiceDeskSummaryViewModel();

                // Calculate Project TASKS...
                sdVm.TaskCount = _serviceDeskService.getTasksCount(dateToday, dateToday, userName, TASK_FINISHED_STATUS); ;
                int YesterdayTaskCount = _serviceDeskService.getTasksCount(dateToday.AddDays(ADD_DAYS_YESTERDAY), dateToday.AddDays(ADD_DAYS_YESTERDAY), userName, TASK_FINISHED_STATUS);
                sdVm.TaskPercentChange = sdVm.CalculatePercentageChange(sdVm.TaskCount, YesterdayTaskCount);

                // Calculate REQUESTS...
                sdVm.RequestCount = _serviceDeskService.getClosedRequestsCount(dateToday, dateToday, userName);
                int YesterdayRequestCount = _serviceDeskService.getClosedRequestsCount(dateToday.AddDays(ADD_DAYS_YESTERDAY), dateToday.AddDays(ADD_DAYS_YESTERDAY), userName);
                sdVm.RequestPercentChange = sdVm.CalculatePercentageChange(sdVm.RequestCount, YesterdayRequestCount);

                // Calculate INCIDENTS...
                sdVm.IncidentCount = _serviceDeskService.getClosedIncidentsCount(dateToday, dateToday, userName);
                int YesterdayIncidentCount = _serviceDeskService.getClosedIncidentsCount(dateToday.AddDays(ADD_DAYS_YESTERDAY), dateToday.AddDays(ADD_DAYS_YESTERDAY), userName);
                sdVm.IncidentPercentChange = sdVm.CalculatePercentageChange(sdVm.IncidentCount, YesterdayIncidentCount);

                // Calculate PROBLEMS...
                sdVm.ProblemCount = _serviceDeskService.getClosedProblemsCount(dateToday, dateToday, userName);
                int YesterdayProblemCount = _serviceDeskService.getClosedProblemsCount(dateToday.AddDays(ADD_DAYS_YESTERDAY), dateToday.AddDays(ADD_DAYS_YESTERDAY), userName);
                sdVm.ProblemPercentChange = sdVm.CalculatePercentageChange(sdVm.ProblemCount, YesterdayProblemCount);

                // CALCULATE TODAY TOTALS...
                sdVm.TodayTotalCount = sdVm.CalculateTotalsCount(sdVm.TaskCount, sdVm.RequestCount, sdVm.IncidentCount, sdVm.ProblemCount);
                //int YesterdayTotalCount = sdVm.CalculateTotalsCount(YesterdayTaskCount, YesterdayRequestCount, YesterdayIncidentCount, YesterdayProblemCount);
                sdVm.TodayPercentageChange = sdVm.CalculatePercentageChange(sdVm.TodayTotalCount, DAY_QUOTA);

                // Calculate Week Statistics...
                int thisWeekNo = 0;
                thisWeekNo = _weeknumberService.GetIso8601WeekOfYear(dateToday);
                Week thisWeek = _weeksRepository.FindBy(w => w.WeekNo == thisWeekNo && w.Year == dateToday.Year).FirstOrDefault();

                if (thisWeek == null)
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Week with Week No: " + thisWeekNo + " not found!");
                }

                ////int lastWeekNo = _weeknumberService.GetIso8601WeekOfYear(dateToday.AddDays(ADD_DAYS_A_WEEK_AGO)); ;
                ////Week lastWeek = _weeksRepository.FindBy(w => w.WeekNo == lastWeekNo).FirstOrDefault();

                ////if (lastWeek == null)
                ////{
                ////    response = request.CreateResponse(HttpStatusCode.NotFound, "Week with Week No: " + lastWeekNo + " not found!");
                ////}

                // CALCULATE TASKS...
                int ThisWeekTaskCount = _serviceDeskService.getTasksCount(thisWeek.StartDate, thisWeek.EndDate, userName, TASK_FINISHED_STATUS);
                //int LastWeekTaskCount = _serviceDeskService.getTasksCount(lastWeek.StartDate, lastWeek.EndDate, userName, TASK_FINISHED_STATUS);

                // CALCULATE REQUESTS...
                int ThisWeekRequestCount = _serviceDeskService.getClosedRequestsCount(thisWeek.StartDate, thisWeek.EndDate, userName);
                //int LastWeekRequestCount = _serviceDeskService.getClosedRequestsCount(lastWeek.StartDate, lastWeek.EndDate, userName);

                // CALCULATE INCIDENTS...
                int ThisWeekIncidentsCount = _serviceDeskService.getClosedIncidentsCount(thisWeek.StartDate, thisWeek.EndDate, userName);
                //int LastWeekIncidentsCount = _serviceDeskService.getClosedIncidentsCount(lastWeek.StartDate, lastWeek.EndDate, userName);

                // CALCULATE PROBLEMS...
                int ThisWeekProblemsCount = _serviceDeskService.getClosedProblemsCount(thisWeek.StartDate, thisWeek.EndDate, userName);
                //int LastWeekProblemsCount = _serviceDeskService.getClosedProblemsCount(lastWeek.StartDate, lastWeek.EndDate, userName);

                sdVm.WeekTotalCount = sdVm.CalculateTotalsCount(ThisWeekTaskCount, ThisWeekRequestCount, ThisWeekIncidentsCount, ThisWeekProblemsCount);
                //int LastWeekTotalCount = sdVm.CalculateTotalsCount(LastWeekTaskCount, LastWeekRequestCount, LastWeekIncidentsCount, LastWeekProblemsCount);

                sdVm.WeekPercentageChange = sdVm.CalculatePercentageChange(sdVm.WeekTotalCount, WEEK_QUOTA);

                // Calculate month statisitics...
                DateTime FirstDayOfMonth = new DateTime(dateToday.Year, dateToday.Month, 1);
                DateTime LastDayofMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);
                //DateTime FirstDayOfLastMonth = FirstDayOfMonth.AddMonths(-1);
                //DateTime LastDayofLastMonth = FirstDayOfMonth.AddDays(-1);

                int ThisMonthTaskCount = _serviceDeskService.getTasksCount(FirstDayOfMonth, LastDayofMonth, userName, TASK_FINISHED_STATUS);
                int ThisMonthRequestCount = _serviceDeskService.getClosedRequestsCount(FirstDayOfMonth, LastDayofMonth, userName);
                int ThisMonthIncidentCount = _serviceDeskService.getClosedIncidentsCount(FirstDayOfMonth, LastDayofMonth, userName);
                int ThisMonthProblemCount = _serviceDeskService.getClosedProblemsCount(FirstDayOfMonth, LastDayofMonth, userName);

                //int LastMonthTaskCount = _serviceDeskService.getTasksCount(FirstDayOfLastMonth, LastDayofLastMonth, userName, TASK_FINISHED_STATUS);
                //int LastMonthRequestCount = _serviceDeskService.getClosedRequestsCount(FirstDayOfLastMonth, LastDayofLastMonth, userName);
                //int LastMonthIncidentCount = _serviceDeskService.getClosedIncidentsCount(FirstDayOfLastMonth, LastDayofLastMonth, userName);
                //int LastMonthProblemCount = _serviceDeskService.getClosedProblemsCount(FirstDayOfLastMonth, LastDayofLastMonth, userName);

                sdVm.MonthTotalCount = ThisMonthTaskCount + ThisMonthRequestCount + ThisMonthIncidentCount + ThisMonthProblemCount;
                //int LastMonthTotalCount = LastMonthTaskCount + LastMonthRequestCount + LastMonthIncidentCount + LastMonthProblemCount;
                sdVm.MonthPercentageChange = sdVm.CalculatePercentageChange(sdVm.MonthTotalCount, MONTH_QUOTA);
                
                response = request.CreateResponse(HttpStatusCode.OK, sdVm);

                return response;
            });
        }
    }
}