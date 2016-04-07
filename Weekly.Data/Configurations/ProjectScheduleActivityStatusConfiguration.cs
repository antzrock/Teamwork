using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectScheduleActivityStatusConfiguration : EntityBaseConfiguration<ProjectScheduleActivityStatus>
    {
        public ProjectScheduleActivityStatusConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(psas => psas.ProjectSchedules).WithRequired(ps => ps.ActivityStatus).HasForeignKey(ps => ps.ProjectScheduleActivityStatusID).WillCascadeOnDelete(false);
        }
    }
}
