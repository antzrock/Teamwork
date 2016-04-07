using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Extensions;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class ProjectService : IProjectService
    {
        #region Variables
        private readonly IEntityBaseRepository<Project> _projectRepository;
        private readonly IEntityBaseRepository<ScrumRole> _scrumRoleRepository;
        private readonly IEntityBaseRepository<ScrumTeamMember> _scrumTeamMemberRepository;
        private readonly IEntityBaseRepository<Team> _teamRepository;
        private readonly IEntityBaseRepository<Group> _groupRepository;
        private readonly IEntityBaseRepository<ProjectStatus> _projectStatusRepository;
        private readonly IEntityBaseRepository<MemberStatus> _memberStatusRepository;
        private readonly IEntityBaseRepository<ProjectSchedule> _projectScheduleRepository;
        private readonly IEntityBaseRepository<ProjectScheduleStatus> _projectScheduleStatusRepository;
        private readonly IEntityBaseRepository<ProjectScheduleActivityStatus> _projectScheduleActivityStatusRespository;
        private readonly IEntityBaseRepository<ProjectCategory> _projectCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public ProjectService(IEntityBaseRepository<Project> projectRepository,  
            IEntityBaseRepository<ScrumRole> scrumRoleRepository, 
            IEntityBaseRepository<ScrumTeamMember> scrumTeamMemberRepository, 
            IEntityBaseRepository<Team> teamRepository,
            IEntityBaseRepository<Group> groupRepository,
            IEntityBaseRepository<ProjectStatus> projectStatusRepository,
            IEntityBaseRepository<MemberStatus> memberStatusRepository,
            IEntityBaseRepository<ProjectSchedule> projectScheduleRepository,
            IEntityBaseRepository<ProjectScheduleStatus> projectScheduleStatusRepository,
            IEntityBaseRepository<ProjectScheduleActivityStatus> projectScheduleActivityStatusRespository,
            IEntityBaseRepository<ProjectCategory> projectCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _scrumRoleRepository = scrumRoleRepository;
            _scrumTeamMemberRepository = scrumTeamMemberRepository;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _projectStatusRepository = projectStatusRepository;
            _memberStatusRepository = memberStatusRepository;
            _projectScheduleRepository = projectScheduleRepository;
            _projectScheduleStatusRepository = projectScheduleStatusRepository;
            _projectScheduleActivityStatusRespository = projectScheduleActivityStatusRespository;
            _projectCategoryRepository = projectCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public List<ProjectCategory> GetProjectCategories()
        {
            List<ProjectCategory> categories = _projectCategoryRepository.GetAll().ToList();

            return categories;
        }

        public ProjectCategory GetDefaultProjectCategory()
        {
            ProjectCategory defaultProjectCategory = _projectCategoryRepository.FindBy(pc => pc.Name == "Application Implementation").FirstOrDefault();

            if (defaultProjectCategory == null)
                throw new Exception("Default Project Category not found in database");

            return defaultProjectCategory;
        }

        public ProjectScheduleActivityStatus GetDefaultScheduleActivityStatus()
        {
            ProjectScheduleActivityStatus defaultStatus = _projectScheduleActivityStatusRespository.FindBy(psas => psas.Name == "Pending").FirstOrDefault();

            if (defaultStatus == null)
                throw new Exception("Default Schedule activity status not found");

            return defaultStatus;
        }

        public ProjectScheduleStatus GetDefaultScheduleStatus()
        {
            ProjectScheduleStatus defaultStatus = _projectScheduleStatusRepository.FindBy(pss => pss.Name == "Active").FirstOrDefault();

            if (defaultStatus == null)
                throw new Exception("Default Schedule status not found");

            return defaultStatus;
        }

        public MemberStatus GetDefaultMemberStatus()
        {
            MemberStatus defaultMemberStatus = _memberStatusRepository.FindBy(ms => ms.Name == "Active").FirstOrDefault();

            if (defaultMemberStatus == null)
                throw new Exception("Default member status not found");

            return defaultMemberStatus;
        }

        public ProjectScheduleActivityStatus GetProjectScheduleActivityState(string status)
        {
            ProjectScheduleActivityStatus existingStatus = _projectScheduleActivityStatusRespository.FindBy(psas => psas.Name == status).FirstOrDefault();

            return existingStatus;
        }

        public ProjectScheduleStatus GetProjectScheduleState(string status)
        {
            ProjectScheduleStatus existingStatus = _projectScheduleStatusRepository.FindBy(pss => pss.Name == status).FirstOrDefault();

            return existingStatus;
        }

        public MemberStatus GetMemberStatus(string status)
        {
            MemberStatus existingMemberStatus = _memberStatusRepository.FindBy(ms => ms.Name == status).FirstOrDefault();

            return existingMemberStatus;
        }

        public List<ProjectScheduleActivityStatus> GetProjectScheduleActivityStates()
        {
            return _projectScheduleActivityStatusRespository.GetAll().ToList();
        }

        public List<ProjectScheduleStatus> GetProjectScheduleStates()
        {
            return _projectScheduleStatusRepository.GetAll().ToList();
        }

        public List<MemberStatus> GetProjectMemberStates()
        {
            return _memberStatusRepository.GetAll().ToList();
        }

        public List<ProjectStatus> GetProjectStates()
        {
            return _projectStatusRepository.GetAll().ToList();
        }

        public ProjectStatus GetDefaultProjectStatus()
        {
            return _projectStatusRepository.FindBy(ps => ps.Name == "Active").FirstOrDefault();
        }

        public Project GetProjectById(int id)
        {
            var existingProject = _projectRepository.GetComplete(id);

            if (existingProject == null)
            {
                throw new Exception("Project not found!");
            }

            return existingProject;
        }

        public List<Project> GetTeamProjects(int userId)
        {
            List<Project> projectsList = new List<Project>();
            projectsList = _projectRepository.GetTeamProjects();

            List<Project> myProjectsList = new List<Project>();

            foreach (Project project in projectsList)
            {
                foreach(ScrumTeamMember member in project.TeamMembers)
                {
                    if (member.UserID == userId && member.MemberStatus.Name == "Active")
                    {
                        //project.CurrentActivityStatus = project.GetActivityStatusForDate(DateTime.Now);
                        ProjectSchedule currentSchedule = project.GetCurrentActivityForDate(DateTime.Now);

                        if (currentSchedule != null)
                        {
                            project.CurrentActivityName = currentSchedule.Activity;
                            project.CurrentActivityStatus = currentSchedule.ActivityStatus.Name;
                        }

                        myProjectsList.Add(project);
                    }
                }
            }
            
            return myProjectsList;
        }

        public List<Project> GetAllProjects(int userId)
        {
            List<Project> allProjectsList = new List<Project>();

            //Team Projects
            List<Project> allTeamProjects = new List<Project>();
            allTeamProjects = GetTeamProjects(userId);

            //Group Projects
            List<Project> allGroupProjects = new List<Project>();
            allGroupProjects = GetGroupProjects(userId);

            //Merge list...
            if (allTeamProjects != null && allTeamProjects.Count > 0)
            {
                foreach (Project teamProj in allTeamProjects)
                {
                    allProjectsList.Add(teamProj);
                }
            }

            if (allGroupProjects != null && allGroupProjects.Count > 0)
            {
                foreach (Project grpProj in allGroupProjects)
                {
                    allProjectsList.Add(grpProj);
                }
            }

            return allProjectsList;
        }

        public List<Project> GetGroupProjects(int userId)
        {
            List<Project> projectsList = new List<Project>();
            projectsList = _projectRepository.GetGroupProjects();

            List<Project> myProjectsList = new List<Project>();

            foreach (Project project in projectsList)
            {
                foreach (ScrumTeamMember member in project.TeamMembers)
                {
                    if (member.UserID == userId && member.MemberStatus.Name == "Active")
                    {
                        //project.CurrentActivityStatus = project.GetActivityStatusForDate(DateTime.Now);
                        ProjectSchedule currentSchedule = project.GetCurrentActivityForDate(DateTime.Now);

                        if (currentSchedule != null)
                        {
                            project.CurrentActivityName = currentSchedule.Activity;
                            project.CurrentActivityStatus = currentSchedule.Status.Name;
                        }
                        myProjectsList.Add(project);
                    }
                }
            }

            return myProjectsList;
        }

        public Project EditProject(int projectId, string projectName, string projectCode, string executiveSummary, string logoPath, List<ScrumTeamMember> teamMembers, List<ProjectSchedule> activities, int mainProjectId, int teamId, int groupId, int stateId, int categoryId, int userId, bool isGroupProject)
        {
            var existingProject = GetProjectById(projectId);

            if (existingProject == null)
            {
                throw new Exception("Project does not exist in database!");
            }

            // Check if main project exists...
            var mainProject = _projectRepository.FindBy(p => p.ID == mainProjectId).FirstOrDefault();

            if (mainProjectId > 0 && mainProject == null)
            {
                throw new Exception("Main project does not exist in database!");
            }

            // Check if group exists...
            var existingGroup = _groupRepository.FindBy(g => g.ID == groupId).FirstOrDefault();

            if (existingGroup == null)
            {
                throw new Exception("Group does not exist in the database!");
            }

            // Check if team exists...
            var existingTeam = _teamRepository.FindBy(t => t.ID == teamId).FirstOrDefault();

            if (teamId > 0 && existingTeam == null)
            {
                throw new Exception("Team does not exist in the database");
            }

            // Check if state exists...
            var existingState = _projectStatusRepository.FindBy(ps => ps.ID == stateId).FirstOrDefault();

            if (existingState == null)
                throw new Exception("Project state does not exist in database");

            // Check if category exists...
            var existingCategory = _projectCategoryRepository.FindBy(pc => pc.ID == categoryId).FirstOrDefault();

            if (existingCategory == null)
                throw new Exception("Project category does not exist in database");

            existingProject.Name = projectName;
            existingProject.Code = projectCode;
            existingProject.ExecutiveSummary = executiveSummary;
            existingProject.LogoPath = logoPath;
            existingProject.GroupID = existingGroup.ID;
            existingProject.Group = existingGroup;
            existingProject.ProjectCategoryID = existingCategory.ID;
            existingProject.Category = existingCategory;

            if (existingTeam == null)
            {
                existingProject.TeamID = null;
                existingProject.Team = null;
            }
            else
            {
                existingProject.TeamID = existingTeam.ID;
                existingProject.Team = existingTeam;
            }

            if (mainProject == null)
            {
                existingProject.MainProjectID = null;
                existingProject.MainProject = null;
            }
            else
            {
                existingProject.MainProject = mainProject;
                existingProject.MainProjectID = mainProject.ID;
            }

            existingProject.ProjectStatusID = existingState.ID;
            existingProject.Status = existingState;

            existingProject.UpdatedBy = userId;
            existingProject.UpdatedDate = DateTime.Now;

            // EDIT TEAM MEMBERS
            // Delete missing members...
            List<ScrumTeamMember> deletedMembers = existingProject.TeamMembers.Where(tm => !teamMembers.Any(tm2 => tm2.ID == tm.ID)).ToList();

            foreach (ScrumTeamMember deleteThis in deletedMembers)
            {
                _scrumTeamMemberRepository.Delete(deleteThis);
                //existingProject.TeamMembers.Remove(deleteThis);
            }

            // Edit existing members...
            foreach (ScrumTeamMember editThis in existingProject.TeamMembers)
            {
                ScrumTeamMember editFrom = teamMembers.FirstOrDefault(tm => tm.ID == editThis.ID);

                if (editFrom != null)
                {
                    // Edit roles...
                    // Delete all roles...
                    List<ScrumRole> deletedRoles = editThis.Roles.ToList();

                    foreach (ScrumRole deleteThisRole in deletedRoles)
                    {
                        editThis.Roles.Remove(deleteThisRole);
                    }

                    // Add new roles...
                    List<ScrumRole> newRoles = editFrom.Roles.ToList();

                    foreach (ScrumRole addThisRole in newRoles)
                    {
                        editThis.Roles.Add(addThisRole);
                    }

                    editThis.UserID = editFrom.UserID;
                    editThis.User = editFrom.User;
                    editThis.UpdatedBy = userId;
                    editThis.UpdatedDate = DateTime.Now;
                    editThis.MemberStatusID = editFrom.MemberStatusID;
                    editThis.MemberStatus = editFrom.MemberStatus;

                    _scrumTeamMemberRepository.Edit(editThis);
                }
            }

            // Add new members...
            List<ScrumTeamMember> newMembers = teamMembers.Where(tm => tm.ID == 0).ToList();

            foreach (ScrumTeamMember addThisMember in newMembers)
            {
                addThisMember.ProjectID = existingProject.ID;
                addThisMember.Project = existingProject;
                addThisMember.CreatedBy = userId;
                addThisMember.CreatedDate = DateTime.Now;

                existingProject.TeamMembers.Add(addThisMember);
            }

            // EDIT PROJECT SCHEDULE
            // Delete missing project schedule...
            List<ProjectSchedule> missingSchedule = existingProject.ActivitiesSchedule.Where(ps => !activities.Any(ps2 => ps2.ID == ps.ID)).ToList();

            foreach (ProjectSchedule deleteThisSchedule in missingSchedule)
            {
                //existingProject.ActivitiesSchedule.Remove(deleteThisSchedule);
                _projectScheduleRepository.Delete(deleteThisSchedule);
            }

            // Edit existing schedule...
            foreach (ProjectSchedule editThisSchedule in existingProject.ActivitiesSchedule)
            {
                ProjectSchedule editScheduleFrom = activities.FirstOrDefault(ps => ps.ID == editThisSchedule.ID);

                if (editScheduleFrom != null)
                {
                    editThisSchedule.PlannedStartDate = editScheduleFrom.PlannedStartDate;
                    editThisSchedule.PlannedEndDate = editScheduleFrom.PlannedEndDate;
                    editThisSchedule.ActualStartDate = editScheduleFrom.ActualStartDate;
                    editThisSchedule.ActualEndDate = editScheduleFrom.ActualEndDate;
                    editThisSchedule.Activity = editScheduleFrom.Activity;
                    editThisSchedule.UpdatedBy = userId;
                    editThisSchedule.UpdatedDate = DateTime.Now;
                    editThisSchedule.ProjectScheduleStatusID = editScheduleFrom.ProjectScheduleStatusID;
                    editThisSchedule.Status = editScheduleFrom.Status;
                    editThisSchedule.ProjectScheduleActivityStatusID = editScheduleFrom.ProjectScheduleActivityStatusID;
                    editThisSchedule.ActivityStatus = editScheduleFrom.ActivityStatus;

                    _projectScheduleRepository.Edit(editThisSchedule);
                }
            }

           //Add new schedule...
           List<ProjectSchedule> newSchedule = activities.Where(ps => ps.ID == 0).ToList();

            foreach (ProjectSchedule addThisSchedule in newSchedule)
            {
                addThisSchedule.ProjectID = existingProject.ID;
                addThisSchedule.Project = existingProject;
                addThisSchedule.CreatedBy = userId;
                addThisSchedule.CreatedDate = DateTime.Now;

                existingProject.ActivitiesSchedule.Add(addThisSchedule);
            }

            _projectRepository.Edit(existingProject);
            _unitOfWork.Commit();

            return existingProject;
        }

        public Project CreateProject(string projectName, string projectCode, string executiveSummary, string logoPath, List<ScrumTeamMember> teamMembers, List<ProjectSchedule> activities, int mainProjectId, int teamId, int groupId, int stateId, int categoryId, int userId, bool isGroupProject)
        {
            var existingProject = _projectRepository.FindBy(p => p.Name == projectName).FirstOrDefault();

            if (existingProject != null)
            {
                throw new Exception("Project name is already in use!");
            }

            // Check if main project exists...
            var mainProject = _projectRepository.FindBy(p => p.ID == mainProjectId).FirstOrDefault();
            
            if (mainProjectId > 0 && mainProject == null)
            {
                throw new Exception("Main project does not exist in database!");
            }

            // Check if group exists...
            var existingGroup = _groupRepository.FindBy(g => g.ID == groupId).FirstOrDefault();

            if (existingGroup == null)
            {
                throw new Exception("Group does not exist in the database!");
            }

            // Check if team exists...
            Team existingTeam = null;

            if (!isGroupProject)
            {
                existingTeam = _teamRepository.FindBy(t => t.ID == teamId).FirstOrDefault();

                if (teamId > 0 && existingTeam == null)
                {
                    throw new Exception("Team does not exist in the database");
                }
            }

            // Check if state exists...
            var existingState = _projectStatusRepository.FindBy(ps => ps.ID == stateId).FirstOrDefault();

            if (existingState == null)
                throw new Exception("Project state does not exist");

            // Check if category exists...
            var existingCategory = _projectCategoryRepository.FindBy(pc => pc.ID == categoryId).FirstOrDefault();

            if (existingCategory == null)
                throw new Exception("Project category does not exist");

            var project = new Project();
            project.Name = projectName;
            project.Code = projectCode;
            project.ExecutiveSummary = executiveSummary;
            project.LogoPath = logoPath;
            project.GroupID = existingGroup.ID;
            project.Group = existingGroup;
            project.ProjectCategoryID = existingCategory.ID;
            project.Category = existingCategory;

            if (existingTeam == null)
            {
                project.TeamID = null;
                project.Team = null;
            }
            else
            {
                project.TeamID = existingTeam.ID;
                project.Team = existingTeam;
            }

            if (mainProject == null)
            {
                project.MainProjectID = null;
                project.MainProject = null;
            }
            else
            {
                project.MainProject = mainProject;
                project.MainProjectID = mainProject.ID;
            }

            project.ProjectStatusID = existingState.ID;
            project.Status = existingState;

            project.CreatedBy = userId;
            project.CreatedDate = DateTime.Now;

            if (isGroupProject)
                project.isGroupProject = true;
            else
                project.isGroupProject = false;

            _projectRepository.Add(project);
            _unitOfWork.Commit();

            if (project != null)
            {

                project.TeamMembers = new List<ScrumTeamMember>();

                foreach (ScrumTeamMember teamMember in teamMembers)
                {
                    teamMember.ProjectID = project.ID;
                    teamMember.Project = project;
                    teamMember.CreatedBy = userId;
                    teamMember.CreatedDate = DateTime.Now;
                    project.TeamMembers.Add(teamMember);
                }

                project.ActivitiesSchedule = new List<ProjectSchedule>();

                foreach (ProjectSchedule sched in activities)
                {
                    sched.ProjectID = project.ID;
                    sched.Project = project;
                    sched.CreatedBy = userId;
                    sched.CreatedDate = DateTime.Now;
                    project.ActivitiesSchedule.Add(sched);
                }

                _projectRepository.Edit(project);
                _unitOfWork.Commit();
            }
                       

            return project;
        }
    }
}
