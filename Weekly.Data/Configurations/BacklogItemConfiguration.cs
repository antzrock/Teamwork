using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class BacklogItemConfiguration : EntityBaseConfiguration<BacklogItem>
    {
        public BacklogItemConfiguration()
        {
            Property(t => t.Item).IsRequired().HasMaxLength(1000);
            Property(t => t.Feature).IsOptional().HasMaxLength(150);
            Property(t => t.StoryPoints).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.CreatedDate).IsRequired();
            HasRequired(bi => bi.Status).WithMany(bis => bis.BacklogItems).HasForeignKey(bi => bi.BacklogItemStatusID).WillCascadeOnDelete(false);
            HasRequired(bi => bi.Priority).WithMany(bip => bip.BacklogItems).HasForeignKey(bi => bi.BacklogItemPriorityID).WillCascadeOnDelete(false);
            HasOptional(bi => bi.ParentItem).WithMany(bi => bi.SubItems).HasForeignKey(bi => bi.ParentID).WillCascadeOnDelete(false);
            HasMany(bi => bi.Tasks).WithOptional(t => t.BacklogItem).HasForeignKey(t => t.BacklogItemID).WillCascadeOnDelete(false);
        }
    }
}
