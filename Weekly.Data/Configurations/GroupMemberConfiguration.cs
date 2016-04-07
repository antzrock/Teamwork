using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class GroupMemberConfiguration : EntityBaseConfiguration<GroupMember>
    {
        public GroupMemberConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(gm => gm.IsUserDefault).IsRequired();
            HasRequired(g => g.Group).WithMany(g => g.Members).HasForeignKey(g => g.GroupID).WillCascadeOnDelete(false);
            HasRequired(gm => gm.User).WithMany(u => u.GroupMembership).HasForeignKey(gm => gm.UserID).WillCascadeOnDelete(false);
            HasMany(gm => gm.Roles).WithMany(gr => gr.Members).Map(gm => { gm.MapLeftKey("RoleRefID"); gm.MapRightKey("GroupMemberRefID"); gm.ToTable("GroupMemberRole"); });
            HasRequired(gm => gm.MemberStatus).WithMany(ms => ms.GroupMembers).HasForeignKey(gm => gm.MemberStatusID).WillCascadeOnDelete(false);
            HasMany(gm => gm.GroupPermissions).WithMany(gp => gp.GroupMembers).Map(gm => { gm.MapLeftKey("PermissionRefID"); gm.MapRightKey("GroupMemberRefID"); gm.ToTable("GroupMemberPermission");});
        }
    }
}
