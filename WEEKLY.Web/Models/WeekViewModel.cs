using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class WeekViewModel
    {
        public int ID { get; set; }

        public int WeekNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ReportDate { get; set; }

        public int Year { get; set; }
    }
}