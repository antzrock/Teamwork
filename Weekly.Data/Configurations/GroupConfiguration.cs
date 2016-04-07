using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class GroupConfiguration : EntityBaseConfiguration<Group>
    {
        public GroupConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(100);
            Property(g => g.Code).IsRequired().HasMaxLength(50);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(g => g.Members);
            HasMany(g => g.Teams);
            HasMany(g => g.Projects).WithRequired(p => p.Group).HasForeignKey(p => p.GroupID).WillCascadeOnDelete(false);
            HasRequired(g => g.Status).WithMany(gs => gs.Groups).HasForeignKey(gs => gs.GroupStatusID).WillCascadeOnDelete(false);
        }
    }
}
