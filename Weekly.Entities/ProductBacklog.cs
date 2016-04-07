using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class ProductBacklog : IEntityBase, IAuditable
    {
        public int ID { get; set; }
        
        // PROJECT REFERENCE
        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public virtual ICollection<BacklogItem> Items { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
