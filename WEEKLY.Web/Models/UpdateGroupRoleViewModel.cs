using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class UpdateGroupRoleViewModel
    {
        public int UserID { get; set; }

        public List<GroupRoleViewModel> GroupRoles { get; set; }
    }
}