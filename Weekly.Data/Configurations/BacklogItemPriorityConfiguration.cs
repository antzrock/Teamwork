using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class BacklogItemPriorityConfiguration : EntityBaseConfiguration<BacklogItemPriority>
    {
        public BacklogItemPriorityConfiguration()
        {
            Property(bip => bip.Name).IsRequired().HasMaxLength(100);
            Property(bip => bip.Description).IsRequired().HasMaxLength(1000);
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.CreatedDate).IsRequired();
            HasMany(bip => bip.BacklogItems).WithRequired(bi => bi.Priority).HasForeignKey(bi => bi.BacklogItemPriorityID).WillCascadeOnDelete(false);
        }
    }
}
