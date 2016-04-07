using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class PermissionConfiguration : EntityBaseConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isAppLevel).IsRequired();
            Property(p => p.isGroupLevel).IsRequired();
            Property(p => p.isTeamLevel).IsRequired();
            Property(p => p.isProjectLevel).IsRequired();
            HasMany(p => p.Users).WithMany(u => u.ApplicationPermissions);
            HasMany(p => p.GroupMembers).WithMany(gm => gm.GroupPermissions);
            HasMany(p => p.TeamMembers).WithMany(tm => tm.Permissions);
            HasMany(p => p.ScrumTeamMembers).WithMany(stm => stm.Permissions);
            HasMany(p => p.Roles).WithMany(r => r.DefaultPermissions);
            HasMany(p => p.GroupRoles).WithMany(gr => gr.DefaultPermissions);
            HasMany(p => p.TeamRoles).WithMany(tr => tr.DefaultPermissions);
            HasMany(p => p.ScrumRoles).WithMany(sr => sr.DefaultPermissions);
        }
    }
}
