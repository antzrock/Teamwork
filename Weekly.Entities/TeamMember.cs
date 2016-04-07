using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class TeamMember : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public int TeamID { get; set; }

        public Team Team { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }

        public bool IsUserDefault { get; set; }

        public virtual ICollection<TeamRole> Roles { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public int MemberStatusID { get; set; }

        public MemberStatus MemberStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
