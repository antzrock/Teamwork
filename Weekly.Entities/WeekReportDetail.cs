using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class WeekReportDetail : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        // PROJECT REFERENCE
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        // WEEK REPORT HEADER REFERENCE
        public int WeekReportHeaderID { get; set; }
        public WeekReportHeader WeekReportHeader { get; set; }

        public bool ShowInReport { get; set; }

        // KEY ACHEIVEMENTS
        public virtual ICollection<KeyAchievement> KeyAchievements { get; set; }

        // PLANNED ACTIVITIES
        public virtual ICollection<PlannedActivity> PlannedActivities { get; set; }

        // RISKS
        public virtual ICollection<Risk> Risks { get; set; }

        // IMPEDIMENTS
        public virtual ICollection<Impediment> Impediments { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
