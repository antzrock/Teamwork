using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class Project : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ExecutiveSummary { get; set; }

        public string LogoPath { get; set; }

        public virtual ICollection<ScrumTeamMember> TeamMembers { get; set; }

        public virtual ICollection<ProjectSchedule> ActivitiesSchedule { get; set; }
        
        public virtual ICollection<WeekReportDetail> WeekReportDetails { get; set; }

        public virtual ICollection<KeyAchievement> KeyAchievements { get; set; }

        public virtual ICollection<PlannedActivity> PlannedActivities { get; set; }

        public virtual ICollection<Risk> Risks { get; set; }

        public virtual ICollection<Impediment> Impediments { get; set; }

        // GROUP REFERENCE
        public int GroupID { get; set; }

        public Group Group { get; set; }

        // TEAM REFERENCE
        public int? TeamID { get; set; }

        public Team Team { get; set; }

        public bool isGroupProject { get; set; }

        // PROJECT CATEGORY
        public int ProjectCategoryID { get; set; }
        public ProjectCategory Category { get; set; }

        // PROJECT STATUS
        public int ProjectStatusID { get; set; }

        public ProjectStatus Status { get; set; }

        // SUB PROJECT REFERENCE
        public int? MainProjectID { get; set; }

        public Project MainProject { get; set; }

        public virtual ICollection<Project> SubProjects { get; set; }

        // PRODUCT BACKLOG
        public int? ProductBacklogID { get; set; }

        public ProductBacklog ProductBacklog { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentActivityStatus { get; set; }

        public string CurrentActivityName { get; set; }

        public ProjectSchedule GetCurrentActivityForDate(DateTime referenceDate)
        {
            ProjectSchedule currentSchedule = null;

            foreach (ProjectSchedule ps in ActivitiesSchedule.OrderBy(asd => asd.ActualStartDate))
            {
                //if (ps.ActualStartDate.HasValue && ps.ActualEndDate.HasValue)
                //{
                //    DateTime actualStart = ps.ActualStartDate.Value;
                //    DateTime actualEnd = ps.ActualEndDate.Value;

                //    if (referenceDate >= actualStart && referenceDate <= actualEnd)
                //    {
                //        currentSchedule = new ProjectSchedule();
                //        currentSchedule = ps;
                //        break;
                //    }
                //}

                if (ps.ActivityStatus.Name == "On-Going")
                {
                    currentSchedule = new ProjectSchedule();
                    currentSchedule = ps;
                    break;
                }
            }

            return currentSchedule;
        }

        public string GetActivityStatusForDate(DateTime referenceDate)
        {
            string currentStatus = "No Activity";

            foreach(ProjectSchedule ps in ActivitiesSchedule.OrderBy(asd => asd.ActualStartDate))
            {
                //if(ps.ActualStartDate.HasValue && ps.ActualEndDate.HasValue)
                //{
                //    DateTime actualStart = ps.ActualStartDate.Value;
                //    DateTime actualEnd = ps.ActualEndDate.Value;

                //    if ((referenceDate >= actualStart && referenceDate <= actualEnd) && ps.ActivityStatus.Name != null)
                //    {
                //        currentStatus = ps.ActivityStatus.Name;
                //        break;
                //    }
                //}

                if (ps.ActivityStatus.Name == "On-Going")
                {
                    currentStatus = ps.ActivityStatus.Name;
                    break;
                }
            }

            return currentStatus;
        }
    }
}
