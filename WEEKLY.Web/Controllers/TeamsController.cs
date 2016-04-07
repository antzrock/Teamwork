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
    [RoutePrefix("api/teams")]
    public class TeamsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Team> _teamsRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<TeamRole> _teamRoleRepository;
        private readonly IEntityBaseRepository<MemberStatus> _memberStatusRepository;
        private readonly IMembershipService _membershipService;
        private readonly ITeamService _teamService;
        private readonly IProjectService _projectService;

        public TeamsController(IEntityBaseRepository<Team> teamsRepository,
                               IEntityBaseRepository<User> userRepository,
                               IEntityBaseRepository<TeamRole> teamRoleRepository,
                               IEntityBaseRepository<MemberStatus> memberStatusRepository,
                               IMembershipService membershipService,
                               ITeamService teamService,
                               IProjectService projectService,
                               IEntityBaseRepository<Error> _errorsRepository,
                               IUnitOfWork _unitOfWork) :
            base(_errorsRepository, _unitOfWork)
        {
            _teamsRepository = teamsRepository;
            _userRepository = userRepository;
            _teamRoleRepository = teamRoleRepository;
            _memberStatusRepository = memberStatusRepository;
            _membershipService = membershipService;
            _teamService = teamService;
            _projectService = projectService;
        }

        [Authorize]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage RegisterTeam(HttpRequestMessage request, TeamCompleteViewModel teamVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "Team is invalid!");
                }
                else
                {
                    //Check team members at least one...
                    if (teamVm.Members.Count <= 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Group must have at least 1 member");
                    }

                    //Get team members...
                    List<TeamMember> teamMembers = new List<TeamMember>();

                    foreach(TeamMemberViewModel tmVm in teamVm.Members)
                    {
                        TeamMember tm = new TeamMember();
                        tm.Roles = new List<TeamRole>();

                        //USER
                        User existingUser = _membershipService.GetUserByFullname(tmVm.Fullname);

                        if (existingUser == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "User with fullname: " + tmVm.Fullname + " not found!");
                        }

                        tm.ID = tmVm.TeamMemberID;
                        tm.UserID = existingUser.ID;
                        tm.User = existingUser;

                        // ROLES
                        foreach (int roleId in tmVm.Roles)
                        {
                            //Check if role exists...
                            TeamRole existingRole = _teamRoleRepository.FindBy(tr => tr.ID == roleId).FirstOrDefault();

                            if (existingRole != null)
                            {
                                tm.Roles.Add(existingRole);
                            }
                        }

                        // STATUS
                        MemberStatus existingMemberStatus = _projectService.GetMemberStatus(tmVm.Status);

                        if (existingMemberStatus == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "Team member status : " + tmVm.Status + " not found!");
                        }
                        else
                        {
                            tm.MemberStatusID = existingMemberStatus.ID;
                            tm.MemberStatus = existingMemberStatus;
                        }

                        // CHECK IF USER IS SET AS DEFAULT IN OTHER TEAMS....
                        bool defaultSetInOtherTeams = _teamService.IsTeamMemberDefaultInOtherGroups(teamVm.TeamID, existingUser.ID);

                        if (defaultSetInOtherTeams && tmVm.IsUserDefault)
                            return request.CreateResponse(HttpStatusCode.BadRequest, existingUser.Fullname + " already has a default team. Only one team can be set as default for a user");

                        // IS DEFAULT GROUP OF USER
                        tm.IsUserDefault = tmVm.IsUserDefault;

                        teamMembers.Add(tm);
                    }

                    Team _team = new Team();

                    if(teamVm.TeamID <= 0)
                    {
                        // Add new team....
                        _team = _teamService.CreateTeam(teamVm.Name, teamVm.Code, teamVm.Description, teamVm.Group.GroupID, teamVm.Status.TeamStatusID, teamVm.UserID, teamMembers);
                    }
                    else
                    {
                        // Update existing team...
                        _team = _teamService.UpdateTeam(teamVm.TeamID, teamVm.Name, teamVm.Code, teamVm.Description, teamVm.Group.GroupID, teamVm.Status.TeamStatusID, teamVm.UserID, teamMembers);
                    }

                    if (_team != null)
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

        [Authorize]
        [Route("maintenance")]
        [HttpGet]
        public HttpResponseMessage GetAllTeamsForMaintenance(HttpRequestMessage request, int? page, int? pageSize, string fieldFilter)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var currPage = page.GetValueOrDefault(0);
                var currPageSize = pageSize.GetValueOrDefault(10);
                
                //var paged = _teamsRepository.AllIncluding(t => t.Status, t => t.Group).OrderBy(t => t.Name).Skip(currPage * currPageSize).Take(currPageSize);

                var totalCount = _teamsRepository.GetAll().Count();

                List<Team> allTeams = new List<Team>();

                if (String.IsNullOrWhiteSpace(fieldFilter))
                {
                    allTeams = _teamsRepository.AllIncluding(t => t.Status, t => t.Group).OrderBy(t => t.Name).Skip(currPage * currPageSize).Take(currPageSize).ToList();
                }
                else
                {
                    allTeams = _teamsRepository.AllIncluding(t => t.Status, t => t.Group).Where(t => t.Name.Contains(fieldFilter) || t.Code.Contains(fieldFilter) || t.Description.Contains(fieldFilter) || t.Group.Name.Contains(fieldFilter) || t.Status.Name.Contains(fieldFilter)).OrderBy(t => t.Name).Skip(currPage * currPageSize).Take(currPageSize).ToList();
                    totalCount = allTeams.Count();
                }
                
                List<TeamMaintenanceViewModel> tmVmList = new List<TeamMaintenanceViewModel>();

                foreach (Team tm in allTeams)
                {
                    TeamMaintenanceViewModel tmVm = new TeamMaintenanceViewModel();

                    tmVm.TeamID = tm.ID;
                    tmVm.Name = tm.Name;
                    tmVm.Code = tm.Code;
                    tmVm.Description = tm.Description;

                    GroupViewModel gVm = new GroupViewModel();
                    gVm.GroupID = tm.Group.ID;
                    gVm.Name = tm.Group.Name;
                    tmVm.Group = gVm;

                    TeamStatusViewModel tsVm = new TeamStatusViewModel();
                    tsVm.TeamStatusID = tm.Status.ID;
                    tsVm.Name = tm.Status.Name;

                    tmVm.Status = tsVm;

                    tmVmList.Add(tmVm);
                }

                PagedCollection<TeamMaintenanceViewModel> pagedTeam = new PagedCollection<TeamMaintenanceViewModel>();
                pagedTeam.Page = currPage;
                pagedTeam.TotalCount = totalCount;
                pagedTeam.TotalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize);
                pagedTeam.Items = (IEnumerable<TeamMaintenanceViewModel>)tmVmList;

                response = request.CreateResponse<PagedCollection<TeamMaintenanceViewModel>>(HttpStatusCode.OK, pagedTeam);
                
                return response;
            });
        }

        [Authorize]
        [Route("members/roles")]
        [HttpGet]
        public HttpResponseMessage GetAllMemberRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TeamRole> allRoles = _teamService.GetAllTeamRoles();
                List<TeamRoleViewModel> allTrVm = new List<TeamRoleViewModel>();

                foreach(TeamRole tr in allRoles)
                {
                    TeamRoleViewModel trVm = Mapper.Map<TeamRole, TeamRoleViewModel>(tr);

                    if (trVm != null)
                        allTrVm.Add(trVm);
                }

                response = request.CreateResponse<IEnumerable<TeamRoleViewModel>>(HttpStatusCode.OK, allTrVm);

                return response;
            });
        }

        [Authorize]
        [Route("states")]
        [HttpGet]
        public HttpResponseMessage GetAllStatus(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TeamStatus> allStates = _teamService.GetAllTeamStates();
                List<TeamStatusViewModel> allTsVm = new List<TeamStatusViewModel>();

                foreach(TeamStatus ts in allStates)
                {
                    TeamStatusViewModel tsVm = Mapper.Map<TeamStatus, TeamStatusViewModel>(ts);

                    if (tsVm != null)
                        allTsVm.Add(tsVm);
                }
                
                response = request.CreateResponse<IEnumerable<TeamStatusViewModel>>(HttpStatusCode.OK, allTsVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTeamForUpdate(HttpRequestMessage request, int teamId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Team existingTeam = _teamService.GetTeamById(teamId);

                if (existingTeam == null)
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Team not found in database!");

                TeamCompleteViewModel tVm = new TeamCompleteViewModel();

                tVm.TeamID = existingTeam.ID;
                tVm.Name = existingTeam.Name;
                tVm.Code = existingTeam.Code;
                tVm.Description = existingTeam.Description;
                tVm.Group = Mapper.Map<Group, GroupViewModel>(existingTeam.Group);
                tVm.Status = Mapper.Map<TeamStatus, TeamStatusViewModel>(existingTeam.Status);
                tVm.Members = new List<TeamMemberViewModel>();

                foreach(TeamMember tm in existingTeam.Members)
                {
                    TeamMemberViewModel tmVm = new TeamMemberViewModel();

                    //USER
                    User existingUser = _userRepository.FindBy(u => u.ID == tm.UserID).FirstOrDefault();

                    if (existingUser == null)
                        return request.CreateErrorResponse(HttpStatusCode.NotFound, "Team Member not found in database!");

                    tmVm.Fullname = existingUser.Fullname;

                    //STATUS
                    MemberStatus existingMemberStatus = _memberStatusRepository.FindBy(ms => ms.ID == tm.MemberStatusID).FirstOrDefault();

                    if (existingMemberStatus == null)
                        return request.CreateErrorResponse(HttpStatusCode.NotFound, "Group member status not found in database!");

                    tmVm.Status = existingMemberStatus.Name;

                    //ROLES
                    tmVm.Roles = new int[tm.Roles.Count];
                    int roleCount = 0;

                    foreach(TeamRole tr in tm.Roles)
                    {
                        tmVm.Roles[roleCount] = tr.ID;

                        ++roleCount;
                    }

                    // IS USER DEFAULT
                    tmVm.IsUserDefault = tm.IsUserDefault;

                    tVm.Members.Add(tmVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, tVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTeamForView(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Team existingTeam = _teamService.GetTeamById(id);

                if (existingTeam == null)
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Team not found in database!");

                TeamCompleteViewModel tVm = new TeamCompleteViewModel();

                tVm.TeamID = existingTeam.ID;
                tVm.Name = existingTeam.Name;
                tVm.Code = existingTeam.Code;
                tVm.Description = existingTeam.Description;
                tVm.Status = Mapper.Map<TeamStatus, TeamStatusViewModel>(existingTeam.Status);

                // MEMBERS
                tVm.Members = new List<TeamMemberViewModel>();

                foreach(TeamMember tm in existingTeam.Members)
                {
                    TeamMemberViewModel tmVm = new TeamMemberViewModel();

                    User existingUser = _userRepository.FindBy(u => u.ID == tm.UserID).FirstOrDefault();

                    if (existingUser != null)
                    {
                        tmVm.TeamMemberID = tm.ID;
                        tmVm.Fullname = existingUser.Fullname;

                        if (String.IsNullOrWhiteSpace(existingUser.AvatarPicPath))
                        {
                            tmVm.AvatarPicPath = "default_avatar.png";
                        }
                        else
                        {
                            tmVm.AvatarPicPath = existingUser.AvatarPicPath;
                        }

                        tmVm.Title = existingUser.Title;

                        //Set Main Role...
                        TeamRole firstRole = tm.Roles.FirstOrDefault();

                        if (firstRole != null)
                        {
                            tmVm.MainRole = firstRole.Name;
                        }
                    }

                    tVm.Members.Add(tmVm);              
                }

                // PROJECTS
                tVm.Projects = new List<ProjectViewModel>();

                foreach(Project proj in existingTeam.Projects)
                {
                    Project existingProject = _projectService.GetProjectById(proj.ID);

                    ProjectViewModel pVm = new ProjectViewModel();
                    pVm.ProjectID = existingProject.ID;

                    pVm.Name = existingProject.Name;
                    pVm.Code = existingProject.Code;

                    if (String.IsNullOrWhiteSpace(existingProject.LogoPath))
                    {
                        pVm.LogoPath = "default_project_logo.png";
                    }
                    else
                    {
                        pVm.LogoPath = existingProject.LogoPath;
                    }
                    
                    if (proj.MainProjectID.HasValue)
                    {
                        pVm.MainProjectID = existingProject.MainProjectID.GetValueOrDefault();
                    }

                    pVm.isGroupProject = existingProject.isGroupProject;

                    ProjectSchedule currentSchedule = existingProject.GetCurrentActivityForDate(DateTime.Now);

                    if (currentSchedule != null)
                    {
                        pVm.CurrentActivityName = currentSchedule.Activity;
                        pVm.CurrentActivityStatus = currentSchedule.ActivityStatus.Name;
                    }

                    tVm.Projects.Add(pVm);
                }

                response = request.CreateResponse<TeamCompleteViewModel>(HttpStatusCode.OK, tVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTeamsForUser(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Team> userTeams = _membershipService.GetTeamsForUser(userId);
                IEnumerable<TeamViewModel> teamsVM = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamViewModel>>(userTeams);
                
                response = request.CreateResponse<IEnumerable<TeamViewModel>>(HttpStatusCode.OK, teamsVM);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTeamsForUserAndGroup(HttpRequestMessage request, int groupId, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Team> userTeams = _membershipService.GetTeamsForUser(userId);
                List<Team> groupTeams = userTeams.Where(t => t.GroupID == groupId).ToList();

                IEnumerable<TeamViewModel> teamsVM = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamViewModel>>(groupTeams);

                response = request.CreateResponse<IEnumerable<TeamViewModel>>(HttpStatusCode.OK, teamsVM);

                return response;
            });
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request, int groupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //var teams = _teamsRepository.GetAll().OrderByDescending(g => g.Name).ToList();
                var teams = _teamsRepository.FindBy(t => t.GroupID == groupId).OrderByDescending(t => t.Name).ToList();

                IEnumerable<TeamViewModel> teamsVM = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamViewModel>>(teams);

                response = request.CreateResponse<IEnumerable<TeamViewModel>>(HttpStatusCode.OK, teamsVM);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("default")]
        [HttpGet]
        public HttpResponseMessage GetDefaultTeam(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var defaultTeam = _teamsRepository.FindBy(g => g.Name.Equals("Applications Support")).FirstOrDefault();

                if (defaultTeam != null)
                {
                    TeamViewModel teamVm = Mapper.Map<Team, TeamViewModel>(defaultTeam);
                    response = request.CreateResponse<TeamViewModel>(HttpStatusCode.OK, teamVm);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Default Team not found!");
                }

                return response;
            });
        }
    }
}