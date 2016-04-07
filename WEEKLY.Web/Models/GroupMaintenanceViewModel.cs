using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class GroupMaintenanceViewModel
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public GroupStatusViewModel Status { get; set; }


    }
}