using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IProjectService
    {
        Project CreateProject(string projectName, string projectCode, string executiveSummary, string logoPath, List<ScrumTeamMember> teamMembers, List<ProjectSchedule> activities, int mainProjectId, int teamId, int groupId, int stateId, int categoryId, int userId, bool isGroupProject);

        Project EditProject(int projectId, string projectName, string projectCode, string executiveSummary, string logoPath, List<ScrumTeamMember> teamMembers, List<ProjectSchedule> activities, int mainProjectId, int teamId, int groupId, int stateId, int categoryId, int userId, bool isGroupProject);

        Project GetProjectById(int id);

        List<Project> GetTeamProjects(int userId);

        List<Project> GetGroupProjects(int userId);

        List<Project> GetAllProjects(int userId);

        ProjectStatus GetDefaultProjectStatus();

        List<ProjectStatus> GetProjectStates();

        List<MemberStatus> GetProjectMemberStates();

        List<ProjectScheduleStatus> GetProjectScheduleStates();

        List<ProjectScheduleActivityStatus> GetProjectScheduleActivityStates();

        MemberStatus GetMemberStatus(string status);

        ProjectScheduleStatus GetProjectScheduleState(string status);

        ProjectScheduleActivityStatus GetProjectScheduleActivityState(string status);

        MemberStatus GetDefaultMemberStatus();

        ProjectScheduleStatus GetDefaultScheduleStatus();

        ProjectScheduleActivityStatus GetDefaultScheduleActivityStatus();

        List<ProjectCategory> GetProjectCategories();

        ProjectCategory GetDefaultProjectCategory();
    }
}
