﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class UpdateProjectRoleViewModel
    {
        public int UserID { get; set; }

        public List<ProjectRoleViewModel> ProjectRoles { get; set; }
    }
}