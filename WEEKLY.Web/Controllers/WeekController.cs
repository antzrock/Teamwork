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
    [RoutePrefix("api/week")]
    public class WeekController : ApiControllerBase
    {
        private readonly IWeekNumberService _weekNumberService;
        private readonly IEntityBaseRepository<Week> _weeksRepository;

        public WeekController(IWeekNumberService weekNumberService, IEntityBaseRepository<Week> weeksRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _weekNumberService = weekNumberService;
            _weeksRepository = weeksRepository;
        }

        [Authorize]
        [Route("weeknos/{yearNo}")]
        [HttpGet]
        public HttpResponseMessage GetWeekNosForYear(HttpRequestMessage request, int yearNo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<WeekNoViewModel> allWeekNos = new List<WeekNoViewModel>();
                List<Week> allWeeks = _weeksRepository.FindBy(w => w.Year == yearNo).ToList();

                foreach (Week wk in allWeeks)
                {
                    WeekNoViewModel wVm = new WeekNoViewModel();
                    wVm.WeekNo = wk.WeekNo;
                    wVm.TargetWeekNo = wk.WeekNo;

                    allWeekNos.Add(wVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allWeekNos);

                return response;
            });
        }

        [Authorize]
        [Route("years")]
        [HttpGet]
        public HttpResponseMessage GetYearsList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<int> allYears = new List<int>();
                var weekGroups = _weeksRepository.GetAll().GroupBy(w => w.Year);

                foreach (IGrouping<int, Week> weekGroup in weekGroups)
                {
                    allYears.Add(weekGroup.Key);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allYears);

                return response;
            });
        }

        [Authorize]
        [Route("{yearNo}/{weekNo}")]
        [HttpGet]
        public HttpResponseMessage GetWeekFromDate(HttpRequestMessage request, int yearNo, int weekNo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Week selectedWeek = _weeksRepository.FindBy(w => w.WeekNo == weekNo && w.Year == yearNo).FirstOrDefault();
                WeekViewModel weekVm = new WeekViewModel();

                if (selectedWeek != null)
                {
                    weekVm.ID = selectedWeek.ID;
                    weekVm.EndDate = selectedWeek.EndDate;
                    weekVm.ReportDate = selectedWeek.EndDate;
                    weekVm.StartDate = selectedWeek.StartDate;
                    weekVm.WeekNo = selectedWeek.WeekNo;
                    weekVm.Year = selectedWeek.Year;

                    response = request.CreateResponse(HttpStatusCode.OK, weekVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Current Week not found!");
                }

                return response;
            });

        }

        [AllowAnonymous]
        [Route("current")]
        [HttpGet]
        public HttpResponseMessage GetCurrentWeek(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DateTime currentDate = System.DateTime.Now;
                int currentWeekNo = _weekNumberService.GetIso8601WeekOfYear(currentDate);
                int currentYear = currentDate.Year;
                Week currentWeek = _weeksRepository.FindBy(w => (w.StartDate <= currentDate && w.EndDate >= currentDate) && w.Year == currentYear).FirstOrDefault();
                WeekViewModel weekVm = new WeekViewModel();

                if (currentWeek != null)
                {
                    weekVm.ID = currentWeek.ID;
                    weekVm.EndDate = currentWeek.EndDate;
                    weekVm.ReportDate = currentWeek.EndDate;
                    weekVm.StartDate = currentWeek.StartDate;
                    weekVm.WeekNo = currentWeek.WeekNo;
                    weekVm.Year = currentWeek.Year;

                    response = request.CreateResponse(HttpStatusCode.OK, weekVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Current Week not found!");
                }

                return response;
            });

        }

        [AllowAnonymous]
        [Route("populate")]
        [HttpPost]
        public HttpResponseMessage PopulateWeeksForYear(HttpRequestMessage request, PopulateWeekViewModel vm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Week> weekList = new List<Week>();
                weekList = _weekNumberService.GetWeekNumbersOfYear(vm.YearNo.ToString(), vm.ReportDayOffset);

                foreach (Week tmpWeek in weekList)
                {
                    //Check if week exists...
                    Week dbWeek = _weeksRepository.FindBy(w => w.WeekNo == tmpWeek.WeekNo && w.Year == tmpWeek.Year).FirstOrDefault();

                    if (dbWeek == null)
                    {
                        //Add Week
                        _weeksRepository.Add(tmpWeek);
                    }
                    else
                    {
                        //Edit Week
                        dbWeek.StartDate = tmpWeek.StartDate;
                        dbWeek.EndDate = tmpWeek.EndDate;
                        dbWeek.ReportDate = tmpWeek.ReportDate;
                        dbWeek.UpdatedBy = 1;
                        dbWeek.UpdatedDate = System.DateTime.Now;
                        dbWeek.Year = vm.YearNo;
                        _weeksRepository.Edit(dbWeek);
                    }

                    _unitOfWork.Commit();
                }

                return response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
            });
        }
    }
}