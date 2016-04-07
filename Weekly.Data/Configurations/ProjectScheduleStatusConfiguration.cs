using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectScheduleStatusConfiguration : EntityBaseConfiguration<ProjectScheduleStatus>
    {
        public ProjectScheduleStatusConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(pss => pss.ProjectSchedules).WithRequired(ps => ps.Status).HasForeignKey(pss => pss.ProjectScheduleStatusID).WillCascadeOnDelete(false);
        }
    }
}
