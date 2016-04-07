using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TeamConfigurations : EntityBaseConfiguration<Team>
    {
        public TeamConfigurations()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(100);
            Property(t => t.Code).IsRequired().HasMaxLength(50);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(t => t.Group).WithMany(g => g.Teams).HasForeignKey(t => t.GroupID).WillCascadeOnDelete(false);
            HasMany(t => t.Members);
            HasMany(t => t.Projects).WithOptional(p => p.Team).HasForeignKey(t => t.TeamID).WillCascadeOnDelete(false);
            HasRequired(t => t.Status).WithMany(ts => ts.Teams).HasForeignKey(ts => ts.TeamStatusID).WillCascadeOnDelete(false);
        }
    }
}
