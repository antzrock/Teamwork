using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(ur => ur.Name).IsRequired().HasMaxLength(50);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(ur => ur.isSystemGenerated).IsRequired();
            HasMany(r => r.DefaultPermissions).WithMany(p => p.Roles).Map( p => { p.MapLeftKey("PermissionRefID"); p.MapRightKey("RoleRefID"); p.ToTable("RolePermission"); });
            HasMany(r => r.UserRoles).WithRequired(ur => ur.Role).HasForeignKey(ur => ur.RoleId).WillCascadeOnDelete(false);
        }
    }
}
