using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class KeyAchievementViewModel
    {
        public int KeyAchievementID { get; set; }

        public int WeekNo { get; set; }

        public string Description { get; set; }

        public decimal PercentComplete { get; set; }
    }
}