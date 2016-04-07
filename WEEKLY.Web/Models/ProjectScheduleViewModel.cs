using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ProjectScheduleViewModel
    {
        public int ProjectScheduleID { get; set; }

        public string Activity { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime PlannedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        //PROJECT
        public int ProjectID { get; set; }

        //STATUS
        public string Status { get; set; }

        //ACTIVITY STATUS
        
        public string ActivityStatus { get; set; }
    }
}