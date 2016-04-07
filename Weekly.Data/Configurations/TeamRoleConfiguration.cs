using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TeamRoleConfiguration : EntityBaseConfiguration<TeamRole>
    {
        public TeamRoleConfiguration()
        {
            Property(tr => tr.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isSystemGenerated).IsRequired();
            HasMany(tr => tr.Members).WithMany(tm => tm.Roles);
            HasMany(tr => tr.DefaultPermissions).WithMany(p => p.TeamRoles).Map(tr => { tr.MapLeftKey("PermissionRefID"); tr.MapRightKey("TeamRoleRefID"); tr.ToTable("TeamRolePermission"); });
        }
    }
}
