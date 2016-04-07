using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    class WeekConfiguration : EntityBaseConfiguration<Week>
    {
        public WeekConfiguration()
        {
            Property(w => w.WeekNo).IsRequired();
            Property(w => w.StartDate).IsRequired();
            Property(w => w.EndDate).IsRequired();
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(w => w.Year).IsRequired();
            HasMany(p => p.WeeklyReports).WithRequired(wr => wr.Week).HasForeignKey(wr => wr.WeekID).WillCascadeOnDelete(false);
        }
    }
}
