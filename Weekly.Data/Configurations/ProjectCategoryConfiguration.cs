using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectCategoryConfiguration : EntityBaseConfiguration<ProjectCategory>
    {
        public ProjectCategoryConfiguration()
        {
            Property(pc => pc.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(pc => pc.Projects).WithRequired(p => p.Category).HasForeignKey(p => p.ProjectCategoryID).WillCascadeOnDelete(false);
        }
    }
}
