using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class WeekReportDetailViewModel
    {
        public int WeekReportDetailID { get; set; }

        public int ProjectID { get; set; }

        public int WeekReportHeaderID { get; set; }

        public bool ShowInReport { get; set; }

        // KEY ACHIEVEMENTS
        public ICollection<KeyAchievementViewModel> KeyAchievements { get; set; }

        // PLANNED ACTIVITIES
        public ICollection<PlannedActivityViewModel> PlannedActivities { get; set; }

        // RISKS
        public ICollection<RiskViewModel> Risks { get; set; }

        // IMPEDIMENTS
        public ICollection<ImpedimentViewModel> Impediments { get; set; }

    }
}