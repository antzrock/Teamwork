using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class BacklogItemViewModel
    {
        public int BacklogItemID { get; set; }

        public string Item { get; set; }

        public string Feature { get; set; }

        public int StoryPoints { get; set; }

        public List<TaskViewModel> Tasks { get; set; }

        // STATUS REFERENCE
        public BacklogItemStatusViewModel Status { get; set; }

        // PRIORITY REFERENCE
        public BacklogItemPriorityViewModel Priority { get; set; }

        // SCRUM STATE
        public DateTime? ToDoDate { get; set; }

        public DateTime? InProgressDate { get; set; }

        public DateTime? DoneDate { get; set; }

    }
}