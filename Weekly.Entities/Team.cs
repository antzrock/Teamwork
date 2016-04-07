using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class Team : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int GroupID { get; set; }

        public Group Group { get; set; }

        // STATUS REFERENCE
        public int TeamStatusID { get; set; }

        public TeamStatus Status { get; set; }

        public virtual ICollection<TeamMember> Members { get; set; }
               
        public virtual ICollection<Project> Projects { get; set; }
        
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
