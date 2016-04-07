using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class BacklogItem : IEntityBase, IAuditable, IScrumStatus
    {
        public int ID { get; set; }

        public string Item { get; set; }

        public string Feature { get; set; }

        public int StoryPoints { get; set; }

        // TASK REFERENCE
        public virtual ICollection<Task> Tasks { get; set; }

        // EPIC REFERENCE
        public int? ParentID { get; set; }

        public BacklogItem ParentItem { get; set; }

        public virtual ICollection<BacklogItem> SubItems { get; set; }
        
        // STATUS REFERENCE
        public int BacklogItemStatusID { get; set; }
        public BacklogItemStatus Status { get; set; }

        // PRIORITY REFERENCE
        public int BacklogItemPriorityID { get; set; }
        
        public BacklogItemPriority Priority { get; set; }

        // SCRUM STATE
        public DateTime? ToDoDate { get; set; }

        public DateTime? InProgressDate { get; set; }

        public DateTime? DoneDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
