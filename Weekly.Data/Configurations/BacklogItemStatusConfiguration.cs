using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class BacklogItemStatusConfiguration : EntityBaseConfiguration<BacklogItemStatus>
    {
        public BacklogItemStatusConfiguration()
        {
            Property(bi => bi.Name).IsRequired().HasMaxLength(150);
            Property(bi => bi.Description).IsOptional().HasMaxLength(1000);
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.CreatedDate).IsRequired();
            HasMany(bis => bis.BacklogItems).WithRequired(bi => bi.Status).HasForeignKey(bi => bi.BacklogItemStatusID).WillCascadeOnDelete(false);
        }
    }
}
