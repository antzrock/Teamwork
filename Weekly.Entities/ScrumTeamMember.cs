using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class ScrumTeamMember : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        //USER
        public int UserID { get; set; }

        public virtual User User { get; set; }

        //PROJECT
        public int ProjectID { get; set;}

        public virtual Project Project { get; set; }

        //ROLE
        public virtual ICollection<ScrumRole> Roles { get; set; }

        // STATUS
        public int MemberStatusID { get; set; }

        public virtual MemberStatus MemberStatus { get; set; }

        // PERMISSIONS
        public virtual ICollection<Permission> Permissions { get; set; }

        // TASKS
        public virtual ICollection<Task> Tasks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
