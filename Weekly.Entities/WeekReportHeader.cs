using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class WeekReportHeader : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        // WEEK REFERENCE
        public int WeekID { get; set; }

        public Week Week { get; set; }
        
        // USER REFERENCE
        public int UserID { get; set; }
                
        public User User { get; set; }
        
        public bool isGroupReport { get; set; }

        // DETAIL REFERENCE
        public virtual ICollection<WeekReportDetail> Details { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
