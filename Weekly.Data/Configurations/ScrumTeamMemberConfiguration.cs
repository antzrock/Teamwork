using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    class ScrumTeamMemberConfiguration : EntityBaseConfiguration<ScrumTeamMember>
    { 
        public ScrumTeamMemberConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(stm => stm.Roles).WithMany(p => p.ScrumTeamMembers).Map(stm => { stm.MapLeftKey("RoleRefID"); stm.MapRightKey("ScrumTeamMemberRefID"); stm.ToTable("ScrumTeamMemberRole"); });
            HasRequired(stm => stm.Project).WithMany(p => p.TeamMembers).HasForeignKey(stm => stm.ProjectID).WillCascadeOnDelete(false);
            HasRequired(stm => stm.MemberStatus).WithMany(ms => ms.ScrumTeamMembers).HasForeignKey(stm => stm.MemberStatusID).WillCascadeOnDelete(false);
            HasRequired(stm => stm.User).WithMany(u => u.ProjectScrumTeamMembership).HasForeignKey(stm => stm.UserID).WillCascadeOnDelete(false);
            HasMany(stm => stm.Permissions).WithMany(p => p.ScrumTeamMembers).Map(stm => { stm.MapLeftKey("PermissionRefID"); stm.MapRightKey("ScrumTeamMemberRefID"); stm.ToTable("ScrumTeamMemberPermission"); });
            HasMany(stm => stm.Tasks).WithRequired(t => t.AssignedTo).HasForeignKey(t => t.AssignedToID).WillCascadeOnDelete(false);
        }
    }
}
