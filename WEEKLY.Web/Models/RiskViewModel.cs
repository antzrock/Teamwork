using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class RiskViewModel
    {
        public int RiskID { get; set; }

        public int WeekNo { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public decimal PercentLikelihood { get; set; }
    }
}