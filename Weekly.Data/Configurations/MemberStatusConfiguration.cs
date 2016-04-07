using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class MemberStatusConfiguration : EntityBaseConfiguration<MemberStatus>
    {
        public MemberStatusConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(ms => ms.ScrumTeamMembers).WithRequired(stm => stm.MemberStatus).HasForeignKey(stm => stm.MemberStatusID).WillCascadeOnDelete(false);
            HasMany(ms => ms.TeamMembers).WithRequired(tm => tm.MemberStatus).HasForeignKey(tm => tm.MemberStatusID).WillCascadeOnDelete(false);
            HasMany(ms => ms.GroupMembers).WithRequired(gm => gm.MemberStatus).HasForeignKey(gm => gm.MemberStatusID).WillCascadeOnDelete(false);
        }
    }
}
