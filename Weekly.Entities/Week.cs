using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class Week : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public int WeekNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ReportDate { get; set; }

        public int Year { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        
        public virtual ICollection<WeekReportHeader> WeeklyReports { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
