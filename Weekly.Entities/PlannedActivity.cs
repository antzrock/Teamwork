using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class PlannedActivity : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public int TargetWeekNo { get; set; }

        public string Description { get; set; }

        // PROJECT REFERENCE
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        // WEEK REPORT DETAIL REFERENCE
        public int WeekReportDetailID { get; set; }
        public WeekReportDetail WeekReportDetail { get; set; }

        // AUDIT FIELDS
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
