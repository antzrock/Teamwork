using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class Permission : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool isAppLevel { get; set; }

        public bool isGroupLevel { get; set; }

        public bool isTeamLevel { get; set; }

        public bool isProjectLevel { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }

        public virtual ICollection<GroupRole> GroupRoles { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }

        public virtual ICollection<TeamRole> TeamRoles { get; set; }

        public virtual ICollection<ScrumTeamMember> ScrumTeamMembers { get; set; }

        public virtual ICollection<ScrumRole> ScrumRoles { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
