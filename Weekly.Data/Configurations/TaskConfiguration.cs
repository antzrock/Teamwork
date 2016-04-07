using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TaskConfiguration : EntityBaseConfiguration<Weekly.Entities.Task>
    {
        public TaskConfiguration()
        {
            Property(t => t.Name).IsRequired().HasMaxLength(150);
            Property(t => t.Description).IsRequired().HasMaxLength(1000);
            HasOptional(t => t.MainTask).WithMany(mt => mt.SubTasks).HasForeignKey(t => t.MainTaskID).WillCascadeOnDelete(false);
            HasRequired(t => t.AssignedTo).WithMany(stm => stm.Tasks).HasForeignKey(t => t.AssignedToID).WillCascadeOnDelete(false);
            HasRequired(t => t.Status).WithMany(ts => ts.Tasks).HasForeignKey(t => t.TaskStatusID).WillCascadeOnDelete(false);

            HasOptional(t => t.BacklogItem).WithMany(bi => bi.Tasks).HasForeignKey(t => t.BacklogItemID).WillCascadeOnDelete(false);

            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.CreatedDate).IsRequired();

        }
    }
}
