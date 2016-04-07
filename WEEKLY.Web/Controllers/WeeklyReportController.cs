using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    [RoutePrefix("api/report/weekly")]
    public class WeeklyReportController : ApiControllerBase
    {
        private readonly IWeeklyReportService _weeklyReportService;
        private readonly IEntityBaseRepository<Week> _weekRepository;
        private readonly IDataSetService _dataSetService;

        public WeeklyReportController(
            IWeeklyReportService weeklyReportService,
            IEntityBaseRepository<Week> weekRepository,
            IDataSetService dataSetService,
            IEntityBaseRepository<Error> _errorsRepository, 
            IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _weeklyReportService = weeklyReportService;
            _weekRepository = weekRepository;
            _dataSetService = dataSetService;
        }

        [Authorize]
        [Route("pdf/{id}")]
        [HttpGet]
        public HttpResponseMessage CreateWeeklyReportPdf(HttpRequestMessage request, int id)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK); ;

                string filename = _weeklyReportService.GetFilenameForWeeklyReport(id);

                var rd = new ReportDocument();
                
                string strPath = HttpContext.Current.Server.MapPath("~/") + "WeeklyReports//WeeklyReportDetails.rpt";
                rd.Load(strPath);
                rd.Refresh();
                rd.SetParameterValue(0, id);
                rd.SetDatabaseLogon("teamwork", "teamwork01");
                
                var tip = ExportFormatType.PortableDocFormat;
                var pdf = rd.ExportToStream(tip);
                response.Headers.Clear();
                response.Content = new StreamContent(pdf);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.Add("x-filename", filename + ".pdf");
                //string contentDisposition = string.Concat("attachment; filename=", "WeeklyReport", ".pdf");
                //response.Content.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(contentDisposition);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filename + ".pdf"
                };

                //response.StatusCode = HttpStatusCode.OK;

                return response;
            });
        }

        [Authorize]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage RegisterWeeklyReportDetails(HttpRequestMessage request, WeekReportHeaderViewModel reportHeader)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //Check week exists...
                Week reportWeek = _weekRepository.FindBy(w => w.ID == reportHeader.WeekID).FirstOrDefault();

                if (reportWeek == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Week does not exist.");

                //Check header exists...
                WeekReportHeader wrh = _weeklyReportService.GetWeeklyReportHeaderById(reportHeader.WeekReportHeaderID);

                if (wrh == null)
                {
                    //Header does not exist....create it...
                    wrh = _weeklyReportService.CreateInitialWeeklyReportHeader(reportWeek.Year, reportWeek.WeekNo, reportHeader.UserID);

                    if (wrh == null)
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Weekly Report cannot be initialized!");

                    //Get header for update....
                    wrh = _weeklyReportService.GetWeeklyReportHeaderById(wrh.ID);
                }

                if (reportHeader.CurrentDetail == null)
                    return request.CreateResponse(HttpStatusCode.BadRequest, "Weekly Report detail is undefined!");

                // Check if detail exists...
                WeekReportDetail wrd = _weeklyReportService.GetWeeklyReportDetailById(reportHeader.CurrentDetail.WeekReportDetailID);

                if (wrd == null)
                {
                    // detail does not exist...create it...
                    wrd = _weeklyReportService.CreateInitialWeeklyReportDetail(wrh.ID, wrh.UserID, reportHeader.CurrentDetail.ProjectID);

                    if (wrd == null)
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Weekly Report detail cannot be initialized!");

                    wrd = _weeklyReportService.GetWeeklyReportDetailById(wrd.ID);
                }

                List<KeyAchievement> kaList = new List<KeyAchievement>();

                // Key Achievements...
                foreach (KeyAchievementViewModel kaVm in reportHeader.CurrentDetail.KeyAchievements)
                {
                    KeyAchievement ka = new KeyAchievement();
                    ka.ID = kaVm.KeyAchievementID;
                    ka.WeekNo = kaVm.WeekNo;
                    ka.Description = kaVm.Description;
                    ka.PercentComplete = kaVm.PercentComplete;
                    kaList.Add(ka);
                }

                List<Risk> riskList = new List<Risk>();

                // Risks....
                foreach (RiskViewModel rVm in reportHeader.CurrentDetail.Risks)
                {
                    Risk r = new Risk();
                    r.ID = rVm.RiskID;
                    r.WeekNo = rVm.WeekNo;
                    r.Description = rVm.Description;
                    r.Effect = rVm.Effect;
                    r.PercentLikelihood = rVm.PercentLikelihood;

                    riskList.Add(r);
                }

                List<Impediment> impedimentsList = new List<Impediment>();

                // Impediments...
                foreach(ImpedimentViewModel impVm in reportHeader.CurrentDetail.Impediments)
                {
                    Impediment imp = new Impediment();
                    imp.ID = impVm.ImpedimentID;
                    imp.WeekNo = impVm.WeekNo;
                    imp.Description = impVm.Description;
                    imp.Effect = impVm.Effect;

                    impedimentsList.Add(imp);
                }

                List<PlannedActivity> plannedActivityList = new List<PlannedActivity>();

                // Planned Activities...
                foreach(PlannedActivityViewModel paVm in reportHeader.CurrentDetail.PlannedActivities)
                {
                    PlannedActivity pa = new PlannedActivity();
                    pa.ID = paVm.PlannedActivityID;
                    pa.TargetWeekNo = paVm.TargetWeekNo;
                    pa.Description = paVm.Description;

                    plannedActivityList.Add(pa);
                }

                //Edit weekly report detail...
                _weeklyReportService.UpdateWeeklyReportDetail(wrd.ID, wrh.ID, reportHeader.UserID, wrd.ProjectID, reportHeader.CurrentDetail.ShowInReport, kaList, riskList, impedimentsList, plannedActivityList);

                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });

                return response;
            });
        }

        [Authorize]
        [Route("{yearNo}/{weekNo}/{userId}/{projectId}")]
        [HttpGet]
        public HttpResponseMessage GetWeeklyReport(HttpRequestMessage request, int yearNo, int weekNo, int userId, int projectId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                WeekReportHeader weeklyReport = _weeklyReportService.GetWeeklyReportHeader(yearNo, weekNo, userId);
                WeekReportDetail weeklyReportDetail = new WeekReportDetail();

                if (weeklyReport == null)
                {
                    weeklyReport = _weeklyReportService.CreateInitialWeeklyReportHeader(yearNo, weekNo, userId);

                    if (weeklyReport == null)
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Weekly Report cannot be initialized!");

                }
                
                if (weeklyReport == null)
                    return request.CreateResponse(HttpStatusCode.NotFound, "Weekly Report Header not found!");

                // Get current weekly report detail using project parameter...
                weeklyReportDetail = _weeklyReportService.GetWeeklyReportDetail(weeklyReport.ID, projectId);

                if (weeklyReportDetail == null)
                {
                    weeklyReportDetail = _weeklyReportService.CreateInitialWeeklyReportDetail(weeklyReport.ID, userId, projectId);

                    if (weeklyReportDetail == null)
                        return request.CreateResponse(HttpStatusCode.NotFound, "Weekly Report Detail not found!");
                }
                
                //Create and populate view model....
                WeekReportHeaderViewModel wrhVm = new WeekReportHeaderViewModel();

                wrhVm.WeekReportHeaderID = weeklyReport.ID;
                wrhVm.WeekID = weeklyReport.Week.ID;
                wrhVm.UserID = weeklyReport.User.ID;

                WeekReportDetailViewModel wrdVm = new WeekReportDetailViewModel();
                wrdVm.WeekReportHeaderID = weeklyReport.ID;
                wrdVm.WeekReportDetailID = weeklyReportDetail.ID;
                wrdVm.ShowInReport = weeklyReportDetail.ShowInReport;
                wrdVm.ProjectID = weeklyReportDetail.Project.ID;
                wrdVm.ShowInReport = weeklyReportDetail.ShowInReport;
                wrdVm.KeyAchievements = new List<KeyAchievementViewModel>();
                wrdVm.Impediments = new List<ImpedimentViewModel>();
                wrdVm.Risks = new List<RiskViewModel>();
                wrdVm.PlannedActivities = new List<PlannedActivityViewModel>();

                // Key Achievements
                if (weeklyReportDetail.KeyAchievements != null)
                {
                    foreach (KeyAchievement ka in weeklyReportDetail.KeyAchievements)
                    {
                        KeyAchievementViewModel kaVm = new KeyAchievementViewModel();
                        kaVm.KeyAchievementID = ka.ID;
                        kaVm.WeekNo = ka.WeekNo;
                        kaVm.PercentComplete = ka.PercentComplete;
                        kaVm.Description = ka.Description;

                        wrdVm.KeyAchievements.Add(kaVm);
                    }
                }

                // Impediments
                if (weeklyReportDetail.Impediments != null)
                {
                    foreach (Impediment imp in weeklyReportDetail.Impediments)
                    {
                        ImpedimentViewModel impVm = new ImpedimentViewModel();
                        impVm.ImpedimentID = imp.ID;
                        impVm.WeekNo = imp.WeekNo;
                        impVm.Description = imp.Description;
                        impVm.Effect = imp.Effect;

                        wrdVm.Impediments.Add(impVm);
                    }
                }

                // Risks
                if (weeklyReportDetail.Risks != null)
                {
                    foreach (Risk rsk in weeklyReportDetail.Risks)
                    {
                        RiskViewModel rskVm = new RiskViewModel();
                        rskVm.RiskID = rsk.ID;
                        rskVm.WeekNo = rsk.WeekNo;
                        rskVm.PercentLikelihood = rsk.PercentLikelihood;
                        rskVm.Effect = rsk.Effect;
                        rskVm.Description = rsk.Description;

                        wrdVm.Risks.Add(rskVm);
                    }
                }

                // Planned Activities...
                if (weeklyReportDetail.PlannedActivities != null)
                {
                    foreach (PlannedActivity pa in weeklyReportDetail.PlannedActivities)
                    {
                        PlannedActivityViewModel paVm = new PlannedActivityViewModel();
                        paVm.PlannedActivityID = pa.ID;
                        paVm.TargetWeekNo = pa.TargetWeekNo;
                        paVm.Description = pa.Description;

                        wrdVm.PlannedActivities.Add(paVm);
                    }
                }

                wrhVm.CurrentDetail = wrdVm;

                response = request.CreateResponse(HttpStatusCode.OK, wrhVm);

                return response;
            });
        }
    }
}