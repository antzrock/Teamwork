using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class UserStatusConfiguration : EntityBaseConfiguration<UserStatus>
    {
        public UserStatusConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(us => us.Users).WithRequired(u => u.UserStatus).HasForeignKey(u => u.UserStatusID).WillCascadeOnDelete(false);
        }
    }
}
