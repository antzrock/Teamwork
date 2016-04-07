using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class ScrumRole : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ScrumTeamMember> ScrumTeamMembers { get; set; }

        public virtual ICollection<Permission> DefaultPermissions { get; set; }

        public bool isSystemGenerated { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }

    }
}
