using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ProjectViewModel
    {
        public int ProjectID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ExecutiveSummary { get; set; }

        public string LogoPath { get; set; }

        public int MainProjectID { get; set; }

        public GroupViewModel Group { get; set; }

        public TeamViewModel Team { get; set; }

        public ProjectStatusViewModel Status { get; set; }

        public ProjectCategoryViewModel Category { get; set; }

        public List<ScrumTeamMemberViewModel> TeamMembers { get; set; }

        public List<ProjectScheduleViewModel> Activities { get; set; }
        
        public bool isGroupProject { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public string CurrentActivityStatus { get; set; }

        public string CurrentActivityName { get; set; }

    }
}