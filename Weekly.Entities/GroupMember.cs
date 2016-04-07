using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class GroupMember : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        public Group Group { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }

        public bool IsUserDefault { get; set; }

        public virtual ICollection<GroupRole> Roles { get; set; }

        public virtual ICollection<Permission> GroupPermissions { get; set; }

        public int MemberStatusID { get; set; }

        public MemberStatus MemberStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
