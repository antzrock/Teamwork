using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TeamMemberConfiguration : EntityBaseConfiguration<TeamMember>
    {
        public TeamMemberConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.IsUserDefault).IsRequired();
            HasRequired(tm => tm.Team).WithMany(t => t.Members).HasForeignKey(tm => tm.TeamID).WillCascadeOnDelete(false);
            HasRequired(tm => tm.User).WithMany(u => u.TeamMembership).HasForeignKey(tm => tm.UserID).WillCascadeOnDelete(false);
            HasMany(tm => tm.Roles).WithMany(tr => tr.Members).Map(tm => { tm.MapLeftKey("RoleRefID"); tm.MapRightKey("TeamMemberRefID"); tm.ToTable("TeamMemberRole"); });
            HasRequired(tm => tm.MemberStatus).WithMany(ms => ms.TeamMembers).HasForeignKey(tm => tm.MemberStatusID).WillCascadeOnDelete(false);
            HasMany(tm => tm.Permissions).WithMany(p => p.TeamMembers).Map(tm => { tm.MapLeftKey("PermissionRefID"); tm.MapRightKey("TeamMemberRefID"); tm.ToTable("TeamMemberPermission"); });
        }
    }
}
