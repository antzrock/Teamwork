using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class UpdateRolesViewModel
    {
        public int UserID { get; set; }

        public List<RoleViewModel> ApplicationRoles { get; set; }
    }
}