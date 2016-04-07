using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weekly.Entities
{
    public class Task : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public decimal PercentComplete { get; set; }

        // BACKLOG ITEM REFERENCE
        public int? BacklogItemID { get; set; }

        public BacklogItem BacklogItem { get; set; }

        // TASK STATUS REFERENCE
        public int TaskStatusID { get; set; }

        public TaskStatus Status { get; set; }
        
        // ASSIGNED TO REFERENCE
        public int AssignedToID { get; set; }

        public ScrumTeamMember AssignedTo { get; set; }

        // SUB-TASK REFERENCE
        public int? MainTaskID { get; set; }

        public Task MainTask { get; set; }

        public virtual ICollection<Task> SubTasks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
