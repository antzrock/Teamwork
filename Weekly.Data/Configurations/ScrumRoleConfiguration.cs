using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ScrumRoleConfiguration : EntityBaseConfiguration<ScrumRole>
    {
        public ScrumRoleConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isSystemGenerated).IsRequired();
            HasMany(sr => sr.ScrumTeamMembers).WithMany(stm => stm.Roles);
            HasMany(sr => sr.DefaultPermissions).WithMany(p => p.ScrumRoles).Map(sr => { sr.MapLeftKey("PermissionRefID");  sr.MapRightKey("ScrumRoleRefID"); sr.ToTable("ScrumRolePermission"); });
        }
    }
}
