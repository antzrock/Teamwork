using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class GroupStatusConfiguration : EntityBaseConfiguration<GroupStatus>
    {
        public GroupStatusConfiguration()
        {
            Property(ps => ps.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(gs => gs.Groups).WithRequired(g => g.Status).HasForeignKey(g => g.GroupStatusID).WillCascadeOnDelete(false);
        }
    }
}
