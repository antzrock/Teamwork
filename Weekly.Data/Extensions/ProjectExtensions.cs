using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Repositiories;
using Weekly.Entities;

namespace Weekly.Data.Extensions
{
    public static class ProjectExtensions
    {
        public static Project GetComplete(this IEntityBaseRepository<Project> projectRepository, int projectId)
        {
            return projectRepository.AllIncluding(p => p.Group, p => p.Team, p => p.Status, p => p.TeamMembers, p => p.Category, p => p.ActivitiesSchedule.Select(psas => psas.Status), p => p.ActivitiesSchedule.Select(psas => psas.ActivityStatus), p => p.ProductBacklog).Where(p => p.ID == projectId).FirstOrDefault();
        }
        
        public static List<Project> GetTeamProjects(this IEntityBaseRepository<Project> projectRepository)
        {
            return  projectRepository.AllIncluding(p => p.Group, p => p.Team, p => p.Status, p => p.TeamMembers, p => p.Category, p => p.ActivitiesSchedule.Select(psas => psas.Status), p => p.ActivitiesSchedule.Select(psas => psas.ActivityStatus), p => p.ProductBacklog).Where(p => p.isGroupProject == false && p.Status.Name == "Active").ToList();
        }

        public static List<Project> GetGroupProjects(this IEntityBaseRepository<Project> projectRepository)
        {
            return projectRepository.AllIncluding(p => p.Group, p => p.Team, p => p.Status, p => p.TeamMembers, p => p.Category, p => p.ActivitiesSchedule.Select(psas => psas.Status), p => p.ActivitiesSchedule.Select(psas => psas.ActivityStatus), p => p.ProductBacklog).Where(p => p.isGroupProject == true && p.Status.Name == "Active").ToList();
        }
        
    }
}
