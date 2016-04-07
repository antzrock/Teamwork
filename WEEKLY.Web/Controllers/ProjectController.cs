using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;
using WEEKLY.Web.Infrastructure.Core;
using WEEKLY.Web.Models;

namespace WEEKLY.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin, User")]
    [RoutePrefix("api/projects")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMembershipService _membershipService;
        private readonly IEntityBaseRepository<ScrumRole> _scrumRoleRepository;

        public ProjectController(IProjectService projectService, IMembershipService membershipService, IEntityBaseRepository<ScrumRole> scrumRoleRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork) :
            base(_errorsRepository, _unitOfWork)
        {
            _projectService = projectService;
            _membershipService = membershipService;
            _scrumRoleRepository = scrumRoleRepository;
        }

        [Authorize]
        [Route("categories/default")]
        [HttpGet]
        public HttpResponseMessage GetProjectCategoryDefault(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProjectCategory defaultCategory = _projectService.GetDefaultProjectCategory();

                if (defaultCategory == null)
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Default project category not found!");

                ProjectCategoryViewModel pcVm = Mapper.Map<Weekly.Entities.ProjectCategory, ProjectCategoryViewModel>(defaultCategory);
                
                response = request.CreateResponse(HttpStatusCode.OK, pcVm);

                return response;
            });
        }

        [Authorize]
        [Route("categories")]
        [HttpGet]
        public HttpResponseMessage GetProjectCategories(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProjectCategory> allCategories = _projectService.GetProjectCategories();
                List<ProjectCategoryViewModel> pcVmList = new List<ProjectCategoryViewModel>();

                foreach(ProjectCategory pc in allCategories)
                {
                    ProjectCategoryViewModel pcVm = Mapper.Map<Weekly.Entities.ProjectCategory, ProjectCategoryViewModel>(pc);

                    if (pcVm != null)
                        pcVmList.Add(pcVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, pcVmList);
                
                return response;
            });
        }

        [Authorize]
        [Route("schedule/activity/status/default")]
        [HttpGet]
        public HttpResponseMessage GetScheduleActivityStatusDefault(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProjectScheduleActivityStatus defaultActivityStatus = _projectService.GetDefaultScheduleActivityStatus();

                if (defaultActivityStatus == null)
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Default schedule activity status not found!");

                ProjectScheduleActivityStatusViewModel psVm = Mapper.Map<Weekly.Entities.ProjectScheduleActivityStatus, ProjectScheduleActivityStatusViewModel>(defaultActivityStatus);

                response = request.CreateResponse(HttpStatusCode.OK, psVm);

                return response;
            });
        }

        [Authorize]
        [Route("schedule/status/default")]
        [HttpGet]
        public HttpResponseMessage GetScheduleStatusDefault(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProjectScheduleStatus defaultScheduleStatus = _projectService.GetDefaultScheduleStatus();

                if (defaultScheduleStatus == null)
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Default schedule status not found!");

                ProjectScheduleStatusViewModel psVm = Mapper.Map<Weekly.Entities.ProjectScheduleStatus, ProjectScheduleStatusViewModel>(defaultScheduleStatus);

                response = request.CreateResponse(HttpStatusCode.OK, psVm);

                return response;
            });
        }

        [Authorize]
        [Route("member/status/default")]
        [HttpGet]
        public HttpResponseMessage GetMemberStatusDefault(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MemberStatus defaultMemberStatus = _projectService.GetDefaultMemberStatus();
                
                if (defaultMemberStatus == null)
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Default member status not found!");

                MemberStatusViewModel msVm = Mapper.Map<Weekly.Entities.MemberStatus, MemberStatusViewModel>(defaultMemberStatus);
                
                response = request.CreateResponse(HttpStatusCode.OK, msVm);

                return response;
            });
        }

        [Authorize]
        [Route("activity/states")]
        [HttpGet]
        public HttpResponseMessage GetProjectActivityStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProjectScheduleActivityStatus> allActivityStates = _projectService.GetProjectScheduleActivityStates();
                List<ProjectScheduleActivityStatusViewModel> psasVmList = new List<ProjectScheduleActivityStatusViewModel>();

                foreach (ProjectScheduleActivityStatus psas in allActivityStates)
                {
                    ProjectScheduleActivityStatusViewModel psasVm = Mapper.Map<Weekly.Entities.ProjectScheduleActivityStatus, ProjectScheduleActivityStatusViewModel>(psas);

                    if (psasVm != null)
                        psasVmList.Add(psasVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, psasVmList);

                return response;
            });
        }

        [Authorize]
        [Route("schedule/states")]
        [HttpGet]
        public HttpResponseMessage GetProjectScheduleStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProjectScheduleStatus> allSchedStates = _projectService.GetProjectScheduleStates();
                List<ProjectScheduleStatusViewModel> pssVmList = new List<ProjectScheduleStatusViewModel>();

                foreach (ProjectScheduleStatus pss in allSchedStates)
                {
                    ProjectScheduleStatusViewModel pssVm = Mapper.Map<Weekly.Entities.ProjectScheduleStatus, ProjectScheduleStatusViewModel>(pss);

                    if (pssVm != null)
                        pssVmList.Add(pssVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, pssVmList);

                return response;
            });
        }

        [Authorize]
        [Route("member/states")]
        [HttpGet]
        public HttpResponseMessage GetProjectMemberStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<MemberStatus> allMemberStates = _projectService.GetProjectMemberStates();
                List<MemberStatusViewModel> msVmList = new List<MemberStatusViewModel>();

                foreach(MemberStatus ms in allMemberStates)
                {
                    MemberStatusViewModel msVm = Mapper.Map<Weekly.Entities.MemberStatus, MemberStatusViewModel>(ms);

                    if (msVm != null)
                        msVmList.Add(msVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, msVmList);

                return response;
            });
        }

        [Authorize]
        [Route("states")]
        [HttpGet]
        public HttpResponseMessage GetProjectStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProjectStatus> allStates = _projectService.GetProjectStates();
                List<ProjectStatusViewModel> pssVmList = new List<ProjectStatusViewModel>();

                foreach(ProjectStatus ps in allStates)
                {
                    ProjectStatusViewModel psVm = Mapper.Map<Weekly.Entities.ProjectStatus, ProjectStatusViewModel>(ps);

                    if (psVm != null)
                        pssVmList.Add(psVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, pssVmList);

                return response;
            });
        }

        [Authorize]
        [Route("status/default")]
        [HttpGet]
        public HttpResponseMessage GetDefaultProjectStatus(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProjectStatus defaultProjectStatus = _projectService.GetDefaultProjectStatus();
                
                if (defaultProjectStatus != null)
                {
                    ProjectStatusViewModel psVm = Mapper.Map<Weekly.Entities.ProjectStatus, ProjectStatusViewModel>(defaultProjectStatus);
                    response = request.CreateResponse(HttpStatusCode.OK, psVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Default project status not found in database!");
                }                
                

                return response;
            });
        }

        [Authorize]
        [Route("all")]
        [HttpGet]
        public HttpResponseMessage GetAllProjectsForUser(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Project> allProjects = _projectService.GetAllProjects(userId);
                List<ProjectViewModel> apVm = new List<ProjectViewModel>();

                foreach (Project project in allProjects)
                {
                    ProjectViewModel pVm = new ProjectViewModel();
                    pVm.ProjectID = project.ID;
                    pVm.Name = project.Name;
                    pVm.LogoPath = project.LogoPath;
                    pVm.ExecutiveSummary = project.ExecutiveSummary;
                    pVm.Code = project.Code;
                    pVm.CurrentActivityStatus = project.CurrentActivityStatus;

                    if (pVm != null)
                    {
                        apVm.Add(pVm);
                    }
                }

                response = request.CreateResponse(HttpStatusCode.OK, apVm);
                return response;
            });
        }

        [Authorize]
        [Route("{projectId}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int projectId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Project existingProject = _projectService.GetProjectById(projectId);
                ProjectViewModel pVm = new ProjectViewModel();

                if (existingProject != null)
                {
                    pVm.ProjectID = existingProject.ID;
                    pVm.Name = existingProject.Name;
                    pVm.Code = existingProject.Code;
                    pVm.ExecutiveSummary = existingProject.ExecutiveSummary;
                    pVm.isGroupProject = existingProject.isGroupProject;
                    pVm.Group = new GroupViewModel();
                    pVm.Group.GroupID = existingProject.Group.ID;
                    pVm.Group.Name = existingProject.Group.Name;
                    pVm.Team = new TeamViewModel();
                    pVm.LogoPath = existingProject.LogoPath;
                    pVm.Status = new ProjectStatusViewModel();
                    pVm.Status.ProjectStatusID = existingProject.Status.ID;
                    pVm.Status.Name = existingProject.Status.Name;
                    pVm.Category = new ProjectCategoryViewModel();
                    pVm.Category.ProjectCategoryID = existingProject.Category.ID;
                    pVm.Category.Name = existingProject.Category.Name;

                    if (pVm.isGroupProject)
                    {
                        pVm.Team.TeamID = 0;
                        pVm.Team.Name = String.Empty;
                    }
                    else
                    {
                        pVm.Team.TeamID = existingProject.Team.ID;
                        pVm.Team.Name = existingProject.Team.Name;
                    }

                    pVm.TeamMembers = new List<ScrumTeamMemberViewModel>();

                    foreach(ScrumTeamMember stm in existingProject.TeamMembers)
                    {
                        ScrumTeamMemberViewModel stmVm = new ScrumTeamMemberViewModel();
                        stmVm.ScrumTeamMemberID = stm.ID;
                        stmVm.ProjectID = existingProject.ID;

                        User existingUser = _membershipService.GetUser(stm.UserID);

                        if (existingUser != null)
                        {
                            stmVm.Fullname = existingUser.Fullname;
                            stmVm.UserID = existingUser.ID;
                            //Get avatar pic url...
                            stmVm.AvatarPicUrl = existingUser.AvatarPicPath;
                            stmVm.JobTitle = existingUser.Title;
                        }

                        stmVm.Roles = new int[stm.Roles.Count];
                        int roleCount = 0;
                        
                        foreach(ScrumRole sr in stm.Roles)
                        {
                            stmVm.Roles[roleCount] = sr.ID;

                            if (roleCount == 0)
                                stmVm.MainRole = sr.Name;

                            ++roleCount;
                        }

                        //Member status...
                        stmVm.Status = stm.MemberStatus.Name;
                        
                        pVm.TeamMembers.Add(stmVm);
                    }

                    pVm.Activities = new List<ProjectScheduleViewModel>();

                    foreach(ProjectSchedule ps in existingProject.ActivitiesSchedule)
                    {
                        ProjectScheduleViewModel psVm = new ProjectScheduleViewModel();
                        psVm.ProjectID = existingProject.ID;
                        psVm.ProjectScheduleID = ps.ID;
                        psVm.PlannedStartDate = ps.PlannedStartDate;
                        psVm.PlannedEndDate = ps.PlannedEndDate;
                        psVm.ActualStartDate = ps.ActualStartDate;
                        psVm.ActualEndDate = ps.ActualEndDate;
                        psVm.Activity = ps.Activity;

                        psVm.Status = ps.Status.Name;
                        psVm.ActivityStatus = ps.ActivityStatus.Name;
                        
                        pVm.Activities.Add(psVm);
                    }

                    response = request.CreateResponse(HttpStatusCode.OK, pVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Project with id: " + projectId + " not found!");
                }

                return response;
            });
        }

        [Authorize]
        [Route("group")]
        [HttpGet]
        public HttpResponseMessage GetGroupProjects(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Project> teamProjects = _projectService.GetGroupProjects(userId);
                List<ProjectViewModel> tpVm = new List<ProjectViewModel>();

                foreach (Project project in teamProjects)
                {
                    ProjectViewModel pVm = new ProjectViewModel();
                    pVm.ProjectID = project.ID;
                    pVm.Name = project.Name;
                    pVm.LogoPath = project.LogoPath;
                    pVm.ExecutiveSummary = project.ExecutiveSummary;
                    pVm.Code = project.Code;
                    pVm.CurrentActivityStatus = project.CurrentActivityStatus;
                    pVm.CurrentActivityName = project.CurrentActivityName;

                    if (pVm != null)
                    {
                        tpVm.Add(pVm);
                    }
                }

                response = request.CreateResponse(HttpStatusCode.OK, tpVm);

                return response;
            });
        }

        [Authorize]
        [Route("team")]
        [HttpGet]
        public HttpResponseMessage GetTeamProjects(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Project> teamProjects = _projectService.GetTeamProjects(userId);
                List<ProjectViewModel> tpVm = new List<ProjectViewModel>();

                foreach (Project project in teamProjects)
                {
                    ProjectViewModel pVm = new ProjectViewModel();
                    pVm.ProjectID = project.ID;
                    pVm.Name = project.Name;
                    pVm.LogoPath = project.LogoPath;
                    pVm.ExecutiveSummary = project.ExecutiveSummary;
                    pVm.Code = project.Code;
                    pVm.CurrentActivityStatus = project.CurrentActivityStatus;
                    pVm.CurrentActivityName = project.CurrentActivityName;

                    if (pVm != null)
                    {
                        tpVm.Add(pVm);
                    }
                }

                response = request.CreateResponse(HttpStatusCode.OK, tpVm);

                return response;
            });
        }

        [Authorize]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, ProjectViewModel project)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "Project is invalid!");
                }
                else
                {
                    //Check team members at least one...
                    if (project.TeamMembers.Count <= 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Project must have at least 1 member");
                    }

                    //Get team members...
                    List<ScrumTeamMember> teamMembers = new List<ScrumTeamMember>();

                    foreach(ScrumTeamMemberViewModel stVm in project.TeamMembers)
                    {
                        ScrumTeamMember newMember = new ScrumTeamMember();
                        newMember.Roles = new List<ScrumRole>();

                        // USER
                        User existingUser = _membershipService.GetUserByFullname(stVm.Fullname);

                        if (existingUser == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "User with fullname: " + stVm.Fullname + " not found!");
                        }

                        newMember.ID = stVm.ScrumTeamMemberID;
                        newMember.UserID = existingUser.ID;
                        newMember.User = existingUser;

                        // ROLES
                        foreach(int roleId in stVm.Roles)
                        {
                            //Check if role exists...
                            ScrumRole existingRole = _scrumRoleRepository.FindBy(sr => sr.ID == roleId).FirstOrDefault();

                            if (existingRole != null)
                            {
                                newMember.Roles.Add(existingRole);
                            }
                        }

                        // STATUS
                        MemberStatus existingMemberStatus = _projectService.GetMemberStatus(stVm.Status);
                        
                        if (existingMemberStatus == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "Scrum Team member status : " + stVm.Status + " not found!");
                        }
                        else
                        {
                            newMember.MemberStatusID = existingMemberStatus.ID;
                            newMember.MemberStatus = existingMemberStatus;
                        }
                        
                        teamMembers.Add(newMember);
                    }

                    //Get activities...
                    List<ProjectSchedule> projActivities = new List<ProjectSchedule>();

                    foreach(ProjectScheduleViewModel psVm in project.Activities)
                    {
                        if (!String.IsNullOrWhiteSpace(psVm.Activity))
                        {
                            ProjectSchedule projSched = new ProjectSchedule();
                            projSched.ID = psVm.ProjectScheduleID;
                            projSched.PlannedStartDate = psVm.PlannedStartDate;
                            projSched.PlannedEndDate = psVm.PlannedEndDate;
                            projSched.ActualStartDate = psVm.ActualStartDate;
                            projSched.ActualEndDate = psVm.ActualEndDate;
                            projSched.Activity = psVm.Activity;

                            // ACTIVITY STATUS
                            ProjectScheduleActivityStatus existingProjectScheduleActivityStatus = _projectService.GetProjectScheduleActivityState(psVm.ActivityStatus);

                            if (existingProjectScheduleActivityStatus == null)
                            {
                                return request.CreateResponse(HttpStatusCode.NotFound, "Project schedule activity status : " + psVm.ActivityStatus + " not found!");
                            }
                            else
                            {
                                projSched.ProjectScheduleActivityStatusID = existingProjectScheduleActivityStatus.ID;
                                projSched.ActivityStatus = existingProjectScheduleActivityStatus;
                            }

                            // SCHEDULE STATUS
                            ProjectScheduleStatus existingProjectScheduleStatus = _projectService.GetProjectScheduleState(psVm.Status);

                            if (existingProjectScheduleStatus == null)
                            {
                                return request.CreateResponse(HttpStatusCode.NotFound, "Project schedule status : " + psVm.Status + " not found!");
                            }
                            else
                            {
                                projSched.ProjectScheduleStatusID = existingProjectScheduleStatus.ID;
                                projSched.Status = existingProjectScheduleStatus;
                            }
                                                        
                            projActivities.Add(projSched);
                        }
                        else
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, "Project schedule activity is required!");
                        }
                    }

                    int TeamId = 0;

                    if (!project.isGroupProject)
                    {
                        TeamId = project.Team.TeamID;
                    }

                    Project _project = new Project();
                    
                    if (project.ProjectID <= 0)
                    {
                        _project = _projectService.CreateProject(project.Name, project.Code, project.ExecutiveSummary, project.LogoPath, teamMembers, projActivities, project.MainProjectID, TeamId, project.Group.GroupID, project.Status.ProjectStatusID, project.Category.ProjectCategoryID, project.CreatedBy, project.isGroupProject);
                    }
                    else
                    {
                        _project = _projectService.EditProject(project.ProjectID, project.Name, project.Code, project.ExecutiveSummary, project.LogoPath, teamMembers, projActivities, project.MainProjectID, TeamId, project.Group.GroupID, project.Status.ProjectStatusID, project.Category.ProjectCategoryID, project.UpdatedBy, project.isGroupProject);
                    }
                    

                    if (_project != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }

                return response;
            });
        }
    }
}