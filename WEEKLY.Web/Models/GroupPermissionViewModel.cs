using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class GroupPermissionViewModel
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public ICollection<PermissionViewModel> Permissions { get; set; }
    }
}