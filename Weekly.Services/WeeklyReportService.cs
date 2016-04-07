using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class WeeklyReportService : IWeeklyReportService
    {
        private readonly IEntityBaseRepository<WeekReportHeader> _weekReportHeaderRepository;
        private readonly IEntityBaseRepository<WeekReportDetail> _weekReportDetailRepository;
        private readonly IMembershipService _membershipService;
        private readonly IEntityBaseRepository<Week> _weekRepository;
        private readonly IProjectService _projectService;
        private readonly IEntityBaseRepository<KeyAchievement> _keyAchievementRepository;
        private readonly IEntityBaseRepository<Impediment> _impedimentRepository;
        private readonly IEntityBaseRepository<Risk> _riskRepository;
        private readonly IEntityBaseRepository<PlannedActivity> _plannedActivityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityBaseRepository<Project> _projectRepository;
        
        public WeeklyReportService(
            IEntityBaseRepository<WeekReportHeader> weekReportHeaderRepository,
            IEntityBaseRepository<WeekReportDetail> weekReportDetailRepository,
            IMembershipService membershipService,
            IEntityBaseRepository<Week> weekRepository,
            IProjectService projectService,
            IEntityBaseRepository<KeyAchievement> keyAchievementRepository,
            IEntityBaseRepository<Impediment> impedimentRepository,
            IEntityBaseRepository<Risk> riskRepository,
            IEntityBaseRepository<PlannedActivity> plannedActivityRepository,
            IEntityBaseRepository<Project> projectRepository,
            IUnitOfWork unitOfWork)
        {
            _weekReportHeaderRepository = weekReportHeaderRepository;
            _weekReportDetailRepository = weekReportDetailRepository;
            _membershipService = membershipService;
            _weekRepository = weekRepository;
            _projectService = projectService;
            _keyAchievementRepository = keyAchievementRepository;
            _impedimentRepository = impedimentRepository;
            _riskRepository = riskRepository;
            _plannedActivityRepository = plannedActivityRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;

        }

        public WeekReportDetail CreateInitialWeeklyReportDetail(int headerId, int userId, int projectId)
        {
            //Get user
            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new ApplicationException("User doesn't exist.");

            WeekReportHeader weekReportHeader = _weekReportHeaderRepository.AllIncluding(wrh => wrh.Week, wrh => wrh.User, wrh => wrh.Details).Where(rh => rh.ID == headerId).FirstOrDefault();

            if (weekReportHeader == null)
                throw new ApplicationException("Week Report header doesn't exist.");

            Project existingProject = _projectService.GetProjectById(projectId);

            if (existingProject == null)
                throw new ApplicationException("Project doesn't exist.");

            WeekReportDetail tempReportDetail = new WeekReportDetail();
            tempReportDetail.WeekReportHeaderID = weekReportHeader.ID;
            tempReportDetail.WeekReportHeader = weekReportHeader;
            tempReportDetail.ProjectID = existingProject.ID;
            tempReportDetail.Project = existingProject;
            tempReportDetail.CreatedBy = existingUser.ID;
            tempReportDetail.CreatedDate = DateTime.Now;
            tempReportDetail.ShowInReport = true;

            _weekReportDetailRepository.Add(tempReportDetail);
            _unitOfWork.Commit();

            return tempReportDetail;
        }

        public WeekReportHeader CreateInitialWeeklyReportHeader(int yearNo, int weekNo, int userId)
        {
            WeekReportHeader initialObj = new WeekReportHeader();

            //Get user
            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new ApplicationException("User doesn't exist.");

            //Get Week number
            Week existingWeek = _weekRepository.FindBy(w => w.Year == yearNo && w.WeekNo == weekNo).FirstOrDefault();

            if (existingWeek == null)
                throw new ApplicationException("Week number doesn't exist.");

            initialObj.WeekID = existingWeek.ID;
            initialObj.Week = existingWeek;
            initialObj.UserID = existingUser.ID;
            initialObj.User = existingUser;
            initialObj.CreatedBy = existingUser.ID;
            initialObj.CreatedDate = DateTime.Now;

            _weekReportHeaderRepository.Add(initialObj);
            _unitOfWork.Commit();

            return initialObj;
        }

        public WeekReportHeader GetWeeklyReportHeaderById(int reportHeaderId)
        {
            return _weekReportHeaderRepository.AllIncluding(wrh => wrh.Week, wrh => wrh.User, wrh => wrh.Details).Where(rh => rh.ID == reportHeaderId).FirstOrDefault();
        }

        public WeekReportHeader GetWeeklyReportHeader(int yearNo, int weekNo, int userId)
        {
            return _weekReportHeaderRepository.AllIncluding(wrh => wrh.Week, wrh => wrh.User, wrh => wrh.Details).Where(rh => rh.Week.Year == yearNo && rh.Week.WeekNo == weekNo && rh.User.ID == userId).FirstOrDefault();
        }

        public WeekReportDetail GetWeeklyReportDetail(int headerId, int projectId)
        {
            return _weekReportDetailRepository.AllIncluding(rd => rd.Project, rd => rd.KeyAchievements, rd => rd.Impediments, rd => rd.Risks, rd => rd.PlannedActivities).Where(rd => rd.WeekReportHeaderID == headerId && rd.Project.ID == projectId).FirstOrDefault();
        }

        public WeekReportDetail GetWeeklyReportDetailById(int reportDetailId)
        {
            return _weekReportDetailRepository.AllIncluding(rd => rd.Project, rd => rd.KeyAchievements, rd => rd.Impediments, rd => rd.Risks, rd => rd.PlannedActivities).Where(rd => rd.ID == reportDetailId).FirstOrDefault();
        }

        public string GetFilenameForWeeklyReport(int reportHeaderId)
        {
            string filename = "WeeklyReport";

            WeekReportHeader wrh = GetWeeklyReportHeaderById(reportHeaderId);

            if (wrh == null)
                throw new ApplicationException("Week report header doesn't exist.");

            Group defaultGroup = _membershipService.GetDefaultGroupForUser(wrh.UserID);
            Team defaultTeam = _membershipService.GetDefaultTeamForUser(wrh.UserID);
            DateTime dateToday = DateTime.Now;
            
            filename = defaultGroup.Code + " " + defaultTeam.Code + " Weekly Report - " + dateToday.ToString("MMddyyyy") + " - " + wrh.User.Nickname;

            return filename;
        }

        public void UpdateWeeklyReportDetail(int weekReportDetailId, int weekReportHeaderId, int userId, int projectId, bool showInReport, List<KeyAchievement> keyAchievements, List<Risk> risks, List<Impediment> impediments, List<PlannedActivity> plannedActivities)
        {
            //Check if report header exists...
            WeekReportHeader wrh = GetWeeklyReportHeaderById(weekReportHeaderId);

            if (wrh == null)
                throw new ApplicationException("Week report header doesn't exist.");

            //Check if report detail exists...
            WeekReportDetail wrd = GetWeeklyReportDetailById(weekReportDetailId);

            //Check if user exists...
            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new ApplicationException("User doesn't exist.");

            //Check if project exists...
            Project existingProject = _projectRepository.FindBy(p => p.ID == projectId).FirstOrDefault();

            if (existingProject == null)
                throw new ApplicationException("Project doesn't exist.");

            if (wrd == null)
                throw new ApplicationException("Week report detail doesn't exist.");

            //Update show in report field...
            wrd.ShowInReport = showInReport;

            //UPDATE KEY ACHIEVEMENTS START
            //Delete missing key achievements...
            List<KeyAchievement> deletedKeyAchievements = wrd.KeyAchievements.Where(ka => !keyAchievements.Any(ka2 => ka2.ID == ka.ID)).ToList();

            foreach (KeyAchievement dKa in deletedKeyAchievements)
            {
                _keyAchievementRepository.Delete(dKa);
            }

            // Edit existing key achievments...
            foreach (KeyAchievement editThis in wrd.KeyAchievements)
            {
                KeyAchievement editFrom = keyAchievements.FirstOrDefault(ka => ka.ID == editThis.ID);

                if (editFrom != null)
                {
                    editThis.WeekNo = editFrom.WeekNo;
                    editThis.Description = editFrom.Description;
                    editThis.PercentComplete = editFrom.PercentComplete;
                    editThis.ProjectID = existingProject.ID;
                    editThis.Project = existingProject;
                    editThis.UpdatedBy = existingUser.ID;
                    editThis.UpdatedDate = DateTime.Now;

                    _keyAchievementRepository.Edit(editThis);
                }
            }

            // Add new key achievements...
            List<KeyAchievement> newKaList = keyAchievements.Where(ka => ka.ID == 0).ToList();

            foreach (KeyAchievement addMe in newKaList)
            {
                addMe.Project = existingProject;
                addMe.ProjectID = existingProject.ID;
                addMe.CreatedBy = existingUser.ID;
                addMe.CreatedDate = DateTime.Now;

                wrd.KeyAchievements.Add(addMe);
            }

            //UPDATE KEY ACHIEVEMENTS END

            //UPDATE IMPEDIMENTS START
            //Delete missing impediments...
            List<Impediment> deletedImpediments = wrd.Impediments.Where(imp => !impediments.Any(imp2 => imp2.ID == imp.ID)).ToList();

            foreach (Impediment deleteThis in deletedImpediments)
            {
                _impedimentRepository.Delete(deleteThis);
            }

            //Edit existing impediments
            foreach (Impediment editThis in wrd.Impediments)
            {
                Impediment editFrom = impediments.FirstOrDefault(imp => imp.ID == editThis.ID);

                if (editFrom != null)
                {
                    editThis.WeekNo = editFrom.WeekNo;
                    editThis.Description = editFrom.Description;
                    editThis.Effect = editFrom.Effect;
                    editThis.UpdatedBy = existingUser.ID;
                    editThis.UpdatedDate = DateTime.Now;

                    _impedimentRepository.Edit(editThis);
                }
            }

            //Add new impediments...
            List<Impediment> newImpediments = impediments.Where(imp => imp.ID == 0).ToList();

            foreach (Impediment addThis in newImpediments)
            {
                addThis.Project = existingProject;
                addThis.ProjectID = existingProject.ID;
                addThis.CreatedBy = existingUser.ID;
                addThis.CreatedDate = DateTime.Now;

                wrd.Impediments.Add(addThis);
            }

            //UPDATE IMPEDIMENTS END

            //UPDATE RISKS START
            //Delete missing risks...
            List<Risk> deletedRisks = wrd.Risks.Where(rsk => !risks.Any(rsk2 => rsk2.ID == rsk.ID)).ToList();

            foreach (Risk deleteThis in deletedRisks)
            {
                _riskRepository.Delete(deleteThis);
            }

            //Edit existing risks...
            foreach (Risk editThis in wrd.Risks)
            {
                Risk editFrom = risks.FirstOrDefault(rsk => rsk.ID == editThis.ID);

                if (editFrom != null)
                {
                    editThis.WeekNo = editFrom.WeekNo;
                    editThis.Description = editFrom.Description;
                    editThis.Effect = editFrom.Effect;
                    editThis.PercentLikelihood = editFrom.PercentLikelihood;
                    editThis.UpdatedBy = existingUser.ID;
                    editThis.UpdatedDate = DateTime.Now;

                    _riskRepository.Edit(editThis);
                }
            }

            //Add new risks...
            List<Risk> newRisks = risks.Where(rsk => rsk.ID == 0).ToList();

            foreach (Risk addThis in newRisks)
            {
                addThis.Project = existingProject;
                addThis.ProjectID = existingProject.ID;
                addThis.CreatedBy = existingUser.ID;
                addThis.CreatedDate = DateTime.Now;

                wrd.Risks.Add(addThis);
            }

            //UPDATE RISKS END

            //UPDATE PLANNED ACTIVITIES START
            //Delete missing planned activities...
            List<PlannedActivity> deletedPlannedActivities = wrd.PlannedActivities.Where(pa => !plannedActivities.Any(pa2 => pa2.ID == pa.ID)).ToList();

            foreach (PlannedActivity deleteThis in deletedPlannedActivities)
            {
                _plannedActivityRepository.Delete(deleteThis);
            }

            //Edit existing planned activities...
            foreach (PlannedActivity editThis in wrd.PlannedActivities)
            {
                PlannedActivity editFrom = plannedActivities.FirstOrDefault(pa => pa.ID == editThis.ID);

                if (editFrom != null)
                {
                    editThis.TargetWeekNo = editFrom.TargetWeekNo;
                    editThis.Description = editFrom.Description;
                    editThis.UpdatedBy = existingUser.ID;
                    editThis.UpdatedDate = DateTime.Now;

                    _plannedActivityRepository.Edit(editThis);
                }
            }

            //Add new planned activities...
            List<PlannedActivity> newPlannedActivities = plannedActivities.Where(pa => pa.ID == 0).ToList();

            foreach(PlannedActivity addThis in newPlannedActivities)
            {
                addThis.Project = existingProject;
                addThis.ProjectID = existingProject.ID;
                addThis.CreatedBy = existingUser.ID;
                addThis.CreatedDate = DateTime.Now;

                wrd.PlannedActivities.Add(addThis);
            }

            //UPDATE PLANNED ACTIVITIES END

            _unitOfWork.Commit();
        }
    }
}
