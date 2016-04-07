using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class UserRole : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        // USER REFERENCE
        public int UserId { get; set; }

        public User User { get; set; }

        // ROLE REFERENCE
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
