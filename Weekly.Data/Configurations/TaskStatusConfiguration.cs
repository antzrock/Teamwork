using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TaskStatusConfiguration : EntityBaseConfiguration<TaskStatus>
    {
        public TaskStatusConfiguration()
        {
            Property(ts => ts.Name).IsRequired().HasMaxLength(150);
            Property(t => t.Description).IsRequired().HasMaxLength(1000);

            HasMany(t => t.Tasks).WithRequired(t => t.Status).HasForeignKey(t => t.TaskStatusID).WillCascadeOnDelete(false);
            Property(ts => ts.CreatedBy).IsRequired();
            Property(ts => ts.CreatedDate).IsRequired();
        }
    }
}
