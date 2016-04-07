using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectScheduleConfiguration : EntityBaseConfiguration<ProjectSchedule>
    {
        public ProjectScheduleConfiguration()
        {
            Property(ps => ps.Activity).IsRequired().HasMaxLength(100);
            Property(ps => ps.PlannedStartDate).IsRequired();
            Property(ps => ps.PlannedEndDate).IsRequired();
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(ps => ps.Project).WithMany(p => p.ActivitiesSchedule).HasForeignKey(ps => ps.ProjectID).WillCascadeOnDelete(false);
            HasRequired(ps => ps.Status).WithMany(pss => pss.ProjectSchedules).HasForeignKey(ps => ps.ProjectScheduleStatusID).WillCascadeOnDelete(false);
            HasRequired(ps => ps.ActivityStatus).WithMany(sa => sa.ProjectSchedules).HasForeignKey(ps => ps.ProjectScheduleActivityStatusID).WillCascadeOnDelete(false);
        }
    }
}
