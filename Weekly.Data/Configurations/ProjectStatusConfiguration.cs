using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectStatusConfiguration : EntityBaseConfiguration<ProjectStatus>
    {
        public ProjectStatusConfiguration()
        {
            Property(ps => ps.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(ps => ps.Projects).WithRequired(p => p.Status).HasForeignKey(p => p.ProjectStatusID).WillCascadeOnDelete(false);
        }
    }
}
