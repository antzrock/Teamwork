using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class PlannedActivityConfiguration : EntityBaseConfiguration<PlannedActivity>
    {
        public PlannedActivityConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.TargetWeekNo).IsRequired();
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            HasRequired(pa => pa.WeekReportDetail).WithMany(wrd => wrd.PlannedActivities).HasForeignKey(pa => pa.WeekReportDetailID).WillCascadeOnDelete(false);
            HasRequired(pa => pa.Project).WithMany(p => p.PlannedActivities).HasForeignKey(pa => pa.ProjectID).WillCascadeOnDelete(false);
        }
    }
}
