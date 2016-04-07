using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IWeeklyReportService
    {
        WeekReportHeader GetWeeklyReportHeader(int yearNo, int weekNo, int userId);
        WeekReportDetail GetWeeklyReportDetail(int headerId, int projectId);

        WeekReportHeader CreateInitialWeeklyReportHeader(int yearNo, int weekNo, int userId);

        WeekReportDetail CreateInitialWeeklyReportDetail(int headerId, int userId, int projectId);

        WeekReportHeader GetWeeklyReportHeaderById(int reportHeaderId);

        WeekReportDetail GetWeeklyReportDetailById(int reportDetailId);

        void UpdateWeeklyReportDetail(int weekReportDetailId, int weekReportHeaderId, int userId, int projectId, bool showInReport, List<KeyAchievement> keyAchievements, List<Risk> risks, List<Impediment> impediments, List<PlannedActivity> plannedActivities);

        string GetFilenameForWeeklyReport(int reportHeaderId);   
    }
}
