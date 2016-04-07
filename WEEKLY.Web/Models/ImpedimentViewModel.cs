using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ImpedimentViewModel
    {
        public int ImpedimentID { get; set; }

        public int WeekNo { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }
    }
}