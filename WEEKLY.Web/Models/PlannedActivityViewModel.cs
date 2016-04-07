using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class PlannedActivityViewModel
    {
        public int PlannedActivityID { get; set; }

        public int TargetWeekNo { get; set; }

        public string Description { get; set; }
    }
}