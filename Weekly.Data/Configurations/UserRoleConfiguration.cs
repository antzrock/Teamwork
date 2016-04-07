using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class UserRoleConfiguration : EntityBaseConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(ur => ur.UserId).IsRequired();
            Property(ur => ur.RoleId).IsRequired();
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).WillCascadeOnDelete(false);
            HasRequired(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).WillCascadeOnDelete(false);
        }
    }
}
