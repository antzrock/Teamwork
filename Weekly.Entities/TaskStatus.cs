using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weekly.Entities
{
    public class TaskStatus : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // TASK REFERENCE
        public virtual ICollection<Task> Tasks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
