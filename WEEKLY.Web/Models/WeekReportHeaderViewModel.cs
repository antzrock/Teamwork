using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class WeekReportHeaderViewModel
    {
        public int WeekReportHeaderID { get; set; }

        public int WeekID { get; set; }

        public int UserID { get; set; }

        public int GroupID { get; set; }

        public int TeamID { get; set; }

        // DETAIL REFERENCE
        public WeekReportDetailViewModel CurrentDetail { get; set; }
    }
}