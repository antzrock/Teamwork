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
    [RoutePrefix("api/groups")]
    public class GroupsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Group> _groupsRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<GroupStatus> _groupStatusRepository;
        private readonly IEntityBaseRepository<GroupRole> _groupRoleRespository;
        private readonly IEntityBaseRepository<MemberStatus> _memberStatusRepository;
        private readonly IMembershipService _membershipService;
        private readonly IProjectService _projectService;
        private readonly IGroupService _groupService;

        public GroupsController(IEntityBaseRepository<Group> groupsRepository,
                                IEntityBaseRepository<User> userRepository,
                                IEntityBaseRepository<GroupStatus> groupStatusRepository,
                                IEntityBaseRepository<GroupRole> groupRoleRepository,
                                IEntityBaseRepository<MemberStatus> memberStatusRepository,
                                IMembershipService membershipService,
                                IProjectService projectService,
                                IGroupService groupService,
                                IEntityBaseRepository<Error> _errorsRepository,
                                IUnitOfWork _unitOfWork) :
            base(_errorsRepository, _unitOfWork)
        {
            _groupsRepository = groupsRepository;
            _userRepository = userRepository;
            _groupStatusRepository = groupStatusRepository;
            _groupRoleRespository = groupRoleRepository;
            _memberStatusRepository = memberStatusRepository;
            _membershipService = membershipService;
            _projectService = projectService;
            _groupService = groupService;
        }

        [Authorize]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage RegisterGroup(HttpRequestMessage request, GroupCompleteViewModel groupVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "Group is invalid!");
                }
                else
                {
                    //Check team members at least one...
                    if (groupVm.Members.Count <= 0)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, "Group must have at least 1 member");
                    }

                    //Get team members...
                    List<GroupMember> teamMembers = new List<GroupMember>();

                    foreach (GroupMemberViewModel gmVm in groupVm.Members)
                    {
                        GroupMember groupMember = new GroupMember();
                        groupMember.Roles = new List<GroupRole>();

                        //USER
                        User existingUser = _membershipService.GetUserByFullname(gmVm.Fullname);

                        if (existingUser == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "User with fullname: " + gmVm.Fullname + " not found!");
                        }

                        groupMember.ID = gmVm.GroupMemberID;
                        groupMember.UserID = existingUser.ID;
                        groupMember.User = existingUser;

                        // ROLES
                        foreach (int roleId in gmVm.Roles)
                        {
                            //Check if role exists...
                            GroupRole existingRole = _groupRoleRespository.FindBy(gr => gr.ID == roleId).FirstOrDefault();

                            if (existingRole != null)
                            {
                                groupMember.Roles.Add(existingRole);
                            }
                        }

                        // STATUS
                        MemberStatus existingMemberStatus = _projectService.GetMemberStatus(gmVm.Status);

                        if (existingMemberStatus == null)
                        {
                            return request.CreateResponse(HttpStatusCode.NotFound, "Group Team member status : " + gmVm.Status + " not found!");
                        }
                        else
                        {
                            groupMember.MemberStatusID = existingMemberStatus.ID;
                            groupMember.MemberStatus = existingMemberStatus;
                        }

                        // CHECK IF USER IS SET AS DEFAULT IN OTHER GROUPS....
                        bool defaultSetInOtherGroups = _groupService.IsGroupMemberDefaultInOtherGroups(groupVm.GroupID, existingUser.ID);

                        if (defaultSetInOtherGroups && gmVm.IsUserDefault)
                            return request.CreateResponse(HttpStatusCode.BadRequest, existingUser.Fullname + " already has a default group. Only one group can be set as default for a user");

                        // IS DEFAULT GROUP OF USER
                        groupMember.IsUserDefault = gmVm.IsUserDefault;

                        teamMembers.Add(groupMember);
                    }

                    Group _group = new Group();

                    if (groupVm.GroupID <= 0)
                    {
                        // Add new group....
                        _group = _groupService.CreateGroup(groupVm.Name, groupVm.Code, groupVm.Description, groupVm.Status.GroupStatusID, groupVm.UserID, teamMembers);
                    }
                    else
                    {
                        // Update existing group...
                        _group = _groupService.UpdateGroup(groupVm.GroupID, groupVm.Name, groupVm.Code, groupVm.Description, groupVm.Status.GroupStatusID, groupVm.UserID, teamMembers);
                    }

                    if (_group != null)
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
        public HttpResponseMessage GetAllGroupsForMaintenance(HttpRequestMessage request, int? page, int? pageSize, string fieldFilter)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var currPage = page.GetValueOrDefault(0);
                var currPageSize = pageSize.GetValueOrDefault(10);

                var totalCount = _groupsRepository.GetAll().Count();

                List<Group> allGroups = new List<Group>();

                if (String.IsNullOrWhiteSpace(fieldFilter))
                {
                    allGroups = _groupsRepository.AllIncluding(t => t.Status).OrderBy(t => t.Name).Skip(currPage * currPageSize).Take(currPageSize).ToList();
                }
                else
                {
                    allGroups = _groupsRepository.AllIncluding(t => t.Status).Where(t => t.Name.Contains(fieldFilter) || t.Code.Contains(fieldFilter) || t.Description.Contains(fieldFilter) || t.Status.Name.Contains(fieldFilter)).OrderBy(t => t.Name).Skip(currPage * currPageSize).Take(currPageSize).ToList();
                    totalCount = allGroups.Count();
                }

                List<GroupMaintenanceViewModel> gmVmList = new List<GroupMaintenanceViewModel>();

                foreach(Group grp in allGroups)
                {
                    GroupMaintenanceViewModel gmVm = new GroupMaintenanceViewModel();

                    gmVm.GroupID = grp.ID;
                    gmVm.Name = grp.Name;
                    gmVm.Code = grp.Code;
                    gmVm.Description = grp.Description;

                    GroupStatusViewModel gsVm = new GroupStatusViewModel();
                    gsVm.GroupStatusID = grp.Status.ID;
                    gsVm.Name = grp.Status.Name;

                    gmVm.Status = gsVm;

                    gmVmList.Add(gmVm);
                }

                PagedCollection<GroupMaintenanceViewModel> pagedGroup = new PagedCollection<GroupMaintenanceViewModel>();
                pagedGroup.Page = currPage;
                pagedGroup.TotalCount = totalCount;
                pagedGroup.TotalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize);
                pagedGroup.Items = (IEnumerable<GroupMaintenanceViewModel>)gmVmList;

                response = request.CreateResponse<PagedCollection<GroupMaintenanceViewModel>>(HttpStatusCode.OK, pagedGroup);
                
                return response;
            });
        }

        [Authorize]
        [Route("members/states")]
        [HttpGet]
        public HttpResponseMessage GetAllGroupMemberStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<MemberStatus> allMemberStates = _memberStatusRepository.GetAll().ToList();
                List<MemberStatusViewModel> allMsVm = new List<MemberStatusViewModel>();

                foreach(MemberStatus ms in allMemberStates)
                {
                    MemberStatusViewModel msVm = Mapper.Map<MemberStatus, MemberStatusViewModel>(ms);

                    if (msVm != null)
                        allMsVm.Add(msVm);
                }

                response = request.CreateResponse<IEnumerable<MemberStatusViewModel>>(HttpStatusCode.OK, allMsVm);

                return response;
            });
        }

        [Authorize]
        [Route("members/roles")]
        [HttpGet]
        public HttpResponseMessage GetAllGroupRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<GroupRole> allGroupRoles = _groupRoleRespository.GetAll().ToList();
                List<GroupRoleViewModel> allGrVm = new List<GroupRoleViewModel>();
                
                foreach(GroupRole gr in allGroupRoles)
                {
                    GroupRoleViewModel grVm = Mapper.Map<GroupRole, GroupRoleViewModel>(gr);

                    if (grVm != null)
                        allGrVm.Add(grVm);
                }

                response = request.CreateResponse<IEnumerable<GroupRoleViewModel>>(HttpStatusCode.OK, allGrVm);

                return response;
            });
        }

        [Authorize]
        [Route("states")]
        [HttpGet]
        public HttpResponseMessage GetAllGroupStates(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<GroupStatus> allGroupStates = _groupStatusRepository.GetAll().ToList();
                List<GroupStatusViewModel> allGsVm = new List<GroupStatusViewModel>();

                foreach(GroupStatus gs in allGroupStates)
                {
                    GroupStatusViewModel gsVm = Mapper.Map<GroupStatus, GroupStatusViewModel>(gs);

                    if (gsVm != null)
                        allGsVm.Add(gsVm);
                }

                response = request.CreateResponse<IEnumerable<GroupStatusViewModel>>(HttpStatusCode.OK, allGsVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetGroupForUpdateById(HttpRequestMessage request, int groupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Group existingGroup = _groupService.GetGroupById(groupId);

                if (existingGroup == null)
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Group not found in database!");

                GroupCompleteViewModel gVm = new GroupCompleteViewModel();

                gVm.GroupID = existingGroup.ID;
                gVm.Code = existingGroup.Code;
                gVm.Name = existingGroup.Name;
                gVm.Description = existingGroup.Description;
                gVm.Members = new List<GroupMemberViewModel>();

                //STATUS...
                gVm.Status = Mapper.Map<GroupStatus, GroupStatusViewModel>(existingGroup.Status);

                //MEMBERS...
                foreach(GroupMember gm in existingGroup.Members)
                {
                    GroupMemberViewModel gmVm = new GroupMemberViewModel();

                    //USER
                    User existingUser = _userRepository.FindBy(u => u.ID == gm.UserID).FirstOrDefault();

                    if (existingUser == null)
                        return request.CreateErrorResponse(HttpStatusCode.NotFound, "Group Member not found in database!");

                    gmVm.Fullname = existingUser.Fullname;

                    //STATUS
                    MemberStatus existingMemberStatus = _memberStatusRepository.FindBy(ms => ms.ID == gm.MemberStatusID).FirstOrDefault();

                    if (existingMemberStatus == null)
                        return request.CreateErrorResponse(HttpStatusCode.NotFound, "Group member status not found in database!");

                    gmVm.Status = existingMemberStatus.Name;

                    //ROLES
                    gmVm.Roles = new int[gm.Roles.Count];
                    int roleCount = 0;

                    foreach(GroupRole gr in gm.Roles)
                    {
                        gmVm.Roles[roleCount] = gr.ID;

                        ++roleCount;
                    }

                    // IS USER DEFAULT
                    gmVm.IsUserDefault = gm.IsUserDefault;

                    gVm.Members.Add(gmVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, gVm);

                return response;
            });
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetGroupForViewById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Group existingGroup = _groupService.GetGroupById(id);

                if (existingGroup == null)
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Group not found in database!");

                GroupCompleteViewModel gVm = new GroupCompleteViewModel();

                gVm.GroupID = existingGroup.ID;
                gVm.Code = existingGroup.Code;
                gVm.Name = existingGroup.Name;
                gVm.Description = existingGroup.Description;

                //Populate group members...
                gVm.Members = new List<GroupMemberViewModel>();

                foreach(GroupMember gm in existingGroup.Members)
                {
                    GroupMemberViewModel gmVm = new GroupMemberViewModel();

                    User existingUser = _userRepository.FindBy(u => u.ID == gm.UserID).FirstOrDefault();
                    
                    if (existingUser != null)
                    {
                        gmVm.UserID = existingUser.ID;
                        gmVm.Fullname = existingUser.Fullname;
                        
                        if (String.IsNullOrWhiteSpace(existingUser.AvatarPicPath))
                        {
                            gmVm.AvatarPicPath = "default_avatar.png";
                        }
                        else
                        {
                            gmVm.AvatarPicPath = existingUser.AvatarPicPath;
                        }

                        gmVm.Title = existingUser.Title;

                        //Set Main Role...
                        GroupRole firstRole = gm.Roles.FirstOrDefault();

                        if (firstRole != null)
                        {
                            gmVm.MainRole = firstRole.Name;
                        }
                        
                    }

                    gVm.Members.Add(gmVm);
                }

                //Populate teams...
                gVm.Teams = new List<TeamViewModel>();

                foreach(Team team in existingGroup.Teams)
                {
                    TeamViewModel tVm = new TeamViewModel();

                    tVm.TeamID = team.ID;
                    tVm.Name = team.Name;
                    tVm.Code = team.Code;

                    gVm.Teams.Add(tVm);
                }

                //Populate projects...
                gVm.Projects = new List<ProjectViewModel>();

                foreach(Project proj in existingGroup.Projects.Where(g => g.isGroupProject == true))
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

                    gVm.Projects.Add(pVm);
                }

                response = request.CreateResponse<GroupCompleteViewModel>(HttpStatusCode.OK, gVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("{userId}")]
        public HttpResponseMessage GetGroupsForUser(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Group> groups = _membershipService.GetGroupsForUser(userId);

                IEnumerable<GroupViewModel> groupsVM = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(groups);

                response = request.CreateResponse<IEnumerable<GroupViewModel>>(HttpStatusCode.OK, groupsVM);

                return response;
            });
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var groups = _groupsRepository.GetAll().OrderByDescending(g => g.Name).ToList();

                IEnumerable<GroupViewModel> groupsVM = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(groups);

                response = request.CreateResponse<IEnumerable<GroupViewModel>>(HttpStatusCode.OK, groupsVM);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("default")]
        [HttpGet]
        public HttpResponseMessage GetDefaultGroup(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var defaultGroup = _groupsRepository.FindBy(g => g.Name.Equals("Information Technology")).FirstOrDefault();
                
                if (defaultGroup != null)
                {
                    GroupViewModel groupVm = Mapper.Map<Group, GroupViewModel>(defaultGroup);
                    response = request.CreateResponse<GroupViewModel>(HttpStatusCode.OK, groupVm);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Default Group not found!");
                }
                
                return response;
            });
        }
    }
}