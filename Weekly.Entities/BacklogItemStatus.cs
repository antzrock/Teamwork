using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class BacklogItemStatus : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        // BACKLOG ITEM REFERENCE
        public virtual ICollection<BacklogItem> BacklogItems { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
