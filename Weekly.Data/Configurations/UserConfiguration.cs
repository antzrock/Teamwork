using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(100);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.Title).IsRequired().HasMaxLength(200);
            Property(u => u.IsLocked).IsRequired();
            Property(u => u.DateCreated);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(u => u.GroupMembership).WithRequired(gm => gm.User).HasForeignKey(gm => gm.UserID).WillCascadeOnDelete(false);
            HasMany(u => u.TeamMembership).WithRequired(tm => tm.User).HasForeignKey(tm => tm.UserID).WillCascadeOnDelete(false);
            Property(u => u.Nickname).IsRequired().HasMaxLength(50);
            HasMany(u => u.ProjectScrumTeamMembership).WithRequired(ptm => ptm.User).HasForeignKey(u => u.UserID).WillCascadeOnDelete(false);
            HasRequired(u => u.UserStatus).WithMany(us => us.Users).HasForeignKey(u => u.UserStatusID).WillCascadeOnDelete(false);
            HasMany(u => u.WeeklyReports).WithRequired(wr => wr.User).HasForeignKey(wr => wr.UserID).WillCascadeOnDelete(false);
            HasMany(u => u.ApplicationPermissions).WithMany(p => p.Users).Map(u => { u.MapLeftKey("PermissionRefId"); u.MapRightKey("UserRefId"); u.ToTable("UserPermission"); });
            HasMany(u => u.UserRoles).WithRequired(ur => ur.User).HasForeignKey(ur => ur.UserId).WillCascadeOnDelete(false);
        }
    }
}
