using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class GroupRoleConfiguration : EntityBaseConfiguration<GroupRole>
    {
        public GroupRoleConfiguration()
        {
            Property(gr => gr.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isSystemGenerated).IsRequired();
            HasMany(gr => gr.Members).WithMany(gm => gm.Roles);
            HasMany(gr => gr.DefaultPermissions).WithMany(p => p.GroupRoles).Map(gr => { gr.MapLeftKey("PermissionRefID"); gr.MapRightKey("GroupRoleRefID"); gr.ToTable("GroupRolePermission"); });
        }
    }
}
