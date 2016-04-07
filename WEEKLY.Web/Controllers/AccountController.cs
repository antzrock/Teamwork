using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services;
using Weekly.Services.Abstract;
using WEEKLY.Web.Infrastructure.Core;
using WEEKLY.Web.Models;

namespace WEEKLY.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin, User")]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;
        private readonly IEntityBaseRepository<Permission> _permissionRepository;

        public AccountController(IMembershipService membershipService, IEntityBaseRepository<Permission> permissionRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _membershipService = membershipService;
            _permissionRepository = permissionRepository;
        }

        [Authorize]
        [Route("maintenance")]
        [HttpGet]
        public HttpResponseMessage GetUsersForMaintenance(HttpRequestMessage request, int? page, int? pageSize, string fieldFilter)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var currPage = page.GetValueOrDefault(0);
                var currPageSize = pageSize.GetValueOrDefault(10);

                var totalCount = _membershipService.GetUsersCount();

                List<User> allUsers = new List<User>();

                if (String.IsNullOrWhiteSpace(fieldFilter))
                {
                    allUsers = _membershipService.GetAllUsersForMaintenance(currPage, currPageSize);
                }
                else
                {
                    allUsers = _membershipService.GetAllUsersFilteredForMaintenance(currPage, currPageSize, fieldFilter);
                    totalCount = allUsers.Count();
                }

                List<UserViewModel> uVmList = new List<UserViewModel>();

                foreach(User usr in allUsers)
                {
                    UserViewModel uVm = new UserViewModel();

                    uVm.UserID = usr.ID;
                    uVm.Username = usr.Username;
                    uVm.Fullname = usr.Fullname;
                    uVm.Nickname = usr.Nickname;
                    uVm.Email = usr.Email;
                    uVm.AvatarPicPath = usr.AvatarPicPath;
                    uVm.ProfilePicPath = usr.ProfilePicPath;

                    UserStatusViewModel usVm = new UserStatusViewModel();
                    usVm.UserStatusID = usr.UserStatus.ID;
                    usVm.Name = usr.UserStatus.Name;

                    uVm.Status = usVm;

                    uVmList.Add(uVm);
                }

                PagedCollection<UserViewModel> pagedUsers = new PagedCollection<UserViewModel>();
                pagedUsers.Page = currPage;
                pagedUsers.TotalCount = totalCount;
                pagedUsers.TotalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize);
                pagedUsers.Items = (IEnumerable<UserViewModel>)uVmList;

                response = request.CreateResponse<PagedCollection<UserViewModel>>(HttpStatusCode.OK, pagedUsers);

                return response;
            });
        }

        [Authorize]
        [Route("groups")]
        [HttpGet]
        public HttpResponseMessage GetUserGroups(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Group> userGroups = _membershipService.GetUserGroups(userId);
                List<GroupViewModel> ugVmList = new List<GroupViewModel>();

                foreach(Group g in userGroups)
                {
                    GroupViewModel gVm = Mapper.Map<Weekly.Entities.Group, GroupViewModel>(g);

                    if (gVm != null)
                        ugVmList.Add(gVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, ugVmList);

                return response;
            });
        }

        [Authorize]
        [Route("permissions")]
        [HttpGet]
        public HttpResponseMessage GetUserPermissions(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                
                UserPermissionsViewModel userPermission = new UserPermissionsViewModel();
                userPermission.ApplicationPermissions = GetApplicationPermissions(userId);
                userPermission.GroupPermissions = GetGroupPermissions(userId);
                userPermission.TeamPermissions = GetTeamPermissions(userId);
                userPermission.ProjectPermissions = GetProjectPermissions(userId);
                userPermission.isUserAdmin = _membershipService.IsUserAdmin(userId);

                response = request.CreateResponse(HttpStatusCode.OK, userPermission);

                return response;
            });
        }

        [Authorize]
        [Route("roles")]
        [HttpPost]
        public HttpResponseMessage SaveApplicationRoles(HttpRequestMessage request, UpdateRolesViewModel appRoles)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Role> rolesList = new List<Role>();
                
                foreach(RoleViewModel rVm in appRoles.ApplicationRoles)
                {
                    Role role = new Role();
                    role.ID = rVm.RoleID;
                    role.Name = rVm.Name;
                    role.DefaultPermissions = new List<Permission>();

                    foreach(int id in rVm.Permissions)
                    {
                        Permission perm = new Permission();
                        perm = _permissionRepository.FindBy(p => p.ID == id).FirstOrDefault();

                        if (perm != null)
                            role.DefaultPermissions.Add(perm);
                    }

                    rolesList.Add(role);
                }

                _membershipService.SaveApplicationRoles(rolesList, appRoles.UserID);
                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });

                return response;
            });
        }

        [Authorize]
        [Route("project/roles")]
        [HttpPost]
        public HttpResponseMessage SaveProjectRoles(HttpRequestMessage request, UpdateProjectRoleViewModel projectRoles)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ScrumRole> rolesList = new List<ScrumRole>();

                foreach(ProjectRoleViewModel prVm in projectRoles.ProjectRoles)
                {
                    ScrumRole role = new ScrumRole();
                    role.ID = prVm.ProjectRoleID;
                    role.Name = prVm.Name;
                    role.DefaultPermissions = new List<Permission>();

                    foreach(int id in prVm.Permissions)
                    {
                        Permission perm = new Permission();
                        perm = _permissionRepository.FindBy(p => p.ID == id).FirstOrDefault();

                        if (perm != null)
                            role.DefaultPermissions.Add(perm);
                    }

                    rolesList.Add(role);
                }

                _membershipService.SaveProjectRoles(rolesList, projectRoles.UserID);
                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });

                return response;
            });
        }

        [Authorize]
        [Route("team/roles")]
        [HttpPost]
        public HttpResponseMessage SaveTeamRoles(HttpRequestMessage request, UpdateTeamRoleViewModel teamRoles)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TeamRole> rolesList = new List<TeamRole>();

                foreach(TeamRoleViewModel trVm in teamRoles.TeamRoles)
                {
                    TeamRole teamRole = new TeamRole();
                    teamRole.ID = trVm.TeamRoleID;
                    teamRole.Name = trVm.Name;
                    teamRole.DefaultPermissions = new List<Permission>();

                    foreach(int id in trVm.Permissions)
                    {
                        Permission perm = new Permission();
                        perm = _permissionRepository.FindBy(p => p.ID == id).FirstOrDefault();

                        if (perm != null)
                            teamRole.DefaultPermissions.Add(perm);
                    }

                    rolesList.Add(teamRole);
                }

                _membershipService.SaveTeamRoles(rolesList, teamRoles.UserID);
                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });

                return response;
            });
        }

        [Authorize]
        [Route("group/roles")]
        [HttpPost]
        public HttpResponseMessage SaveGroupRoles(HttpRequestMessage request, UpdateGroupRoleViewModel appRoles)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<GroupRole> rolesList = new List<GroupRole>();

                foreach (GroupRoleViewModel rVm in appRoles.GroupRoles)
                {
                    GroupRole role = new GroupRole();
                    role.ID = rVm.GroupRoleID;
                    role.Name = rVm.Name;
                    role.DefaultPermissions = new List<Permission>();

                    foreach (int id in rVm.Permissions)
                    {
                        Permission perm = new Permission();
                        perm = _permissionRepository.FindBy(p => p.ID == id).FirstOrDefault();

                        if (perm != null)
                            role.DefaultPermissions.Add(perm);
                    }

                    rolesList.Add(role);
                }

                _membershipService.SaveGroupRoles(rolesList, appRoles.UserID);
                response = request.CreateResponse(HttpStatusCode.OK, new { success = true });

                return response;
            });
        }
        
        [Authorize]
        [Route("group/roles")]
        [HttpGet]
        public HttpResponseMessage GetGroupRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<GroupRole> allGroupRoles = _membershipService.GetAllGroupRoles();
                List<GroupRoleViewModel> allRolesVm = new List<GroupRoleViewModel>();

                foreach (GroupRole role in allGroupRoles)
                {
                    GroupRoleViewModel grvm = new GroupRoleViewModel();
                    grvm.GroupRoleID = role.ID;
                    grvm.Name = role.Name;
                    grvm.Permissions = new int[role.DefaultPermissions.Count];
                    grvm.isSystemGenerated = role.isSystemGenerated;

                    int index = 0;

                    foreach (Permission p in role.DefaultPermissions)
                    {
                        grvm.Permissions[index++] = p.ID;
                    }

                    allRolesVm.Add(grvm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allRolesVm);

                return response;
            });
        }

        [Authorize]
        [Route("project/roles")]
        [HttpGet]
        public HttpResponseMessage GetProjectRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ScrumRole> allProjectRoles = _membershipService.GetAllProjectRoles();
                List<ProjectRoleViewModel> allProjectRolesVm = new List<ProjectRoleViewModel>();

                foreach(ScrumRole role in allProjectRoles)
                {
                    ProjectRoleViewModel prVm = new ProjectRoleViewModel();
                    prVm.ProjectRoleID = role.ID;
                    prVm.Name = role.Name;
                    prVm.Permissions = new int[role.DefaultPermissions.Count];
                    prVm.isSystemGenerated = role.isSystemGenerated;
                    int index = 0;

                    foreach(Permission p in role.DefaultPermissions)
                    {
                        prVm.Permissions[index++] = p.ID;
                    }

                    allProjectRolesVm.Add(prVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allProjectRolesVm);

                return response;
            });
        }


        [Authorize]
        [Route("team/roles")]
        [HttpGet]
        public HttpResponseMessage GetTeamRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TeamRole> allTeamRoles = _membershipService.GetAllTeamRoles();
                List<TeamRoleViewModel> allTeamRolesVm = new List<TeamRoleViewModel>();

                foreach (TeamRole role in allTeamRoles)
                {
                    TeamRoleViewModel trVm = new TeamRoleViewModel();
                    trVm.TeamRoleID = role.ID;
                    trVm.Name = role.Name;
                    trVm.Permissions = new int[role.DefaultPermissions.Count];
                    trVm.isSystemGenerated = role.isSystemGenerated;
                    int index = 0;

                    foreach (Permission p in role.DefaultPermissions)
                    {
                        trVm.Permissions[index++] = p.ID;
                    }

                    allTeamRolesVm.Add(trVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allTeamRolesVm);

                return response;
            });
        }

        [Authorize]
        [Route("roles")]
        [HttpGet]
        public HttpResponseMessage GetApplicationRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Role> allAppRoles = _membershipService.GetAllApplicationRoles();
                List<RoleViewModel> allRolesVm = new List<RoleViewModel>();

                foreach(Role role in allAppRoles)
                {
                    RoleViewModel rvm = new RoleViewModel();
                    rvm.RoleID = role.ID;
                    rvm.Name = role.Name;
                    rvm.Permissions = new int[role.DefaultPermissions.Count];
                    rvm.isSystemGenerated = role.isSystemGenerated;

                    int index = 0;

                    foreach(Permission p in role.DefaultPermissions)
                    {
                        rvm.Permissions[index++] = p.ID;
                    }

                    allRolesVm.Add(rvm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allRolesVm);

                return response;
            });
        }

        [Authorize]
        [Route("permissions")]
        [HttpGet]
        public HttpResponseMessage GetAllPermissions(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Permission> allPermissions = _membershipService.GetAllApplicationPermissions();
                List<PermissionViewModel> allPermVm = new List<PermissionViewModel>();

                foreach (Permission p in allPermissions)
                {
                    PermissionViewModel pvm = Mapper.Map<Permission, PermissionViewModel>(p);
                    allPermVm.Add(pvm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allPermVm);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                    if (_userContext.User != null)
                    {
                        Weekly.Entities.User _user = _membershipService.GetUser(user.Username);

                        //Check if user is active...
                        if (_user.UserStatus != null)
                        {
                            if (!_user.UserStatus.Name.Equals("Active"))
                            {
                                response = request.CreateResponse(HttpStatusCode.BadRequest, "Sorry, you have no permission to access this web application.");
                            }
                        }
                        else
                        {
                            response = request.CreateResponse(HttpStatusCode.NotFound, "Active user status not found in database!");
                        }

                        UserViewModel userVm = Mapper.Map<Weekly.Entities.User, UserViewModel>(_user);
                        
                        if (userVm != null)
                        {

                            if (String.IsNullOrWhiteSpace(userVm.AvatarPicPath))
                            {
                                userVm.AvatarPicPath = "default_avatar.png";
                            }

                            if (String.IsNullOrWhiteSpace(userVm.ProfilePicPath))
                            {
                                userVm.ProfilePicPath = "default_profile.png";
                            }

                            if (String.IsNullOrWhiteSpace(userVm.ProfileQuote))
                            {
                                userVm.ProfileQuote = "Together Everyone Achieves More";
                            }
                                                        
                        }

                        //Get Application Permissions...
                        List<PermissionViewModel> appPermissions = GetApplicationPermissions(userVm.UserID);

                        //Get Group Permissions...
                        List<GroupPermissionViewModel> groupPermissions = GetGroupPermissions(userVm.UserID);

                        //Get Team Permissions...
                        List<TeamPermissionViewModel> teamPermissions = GetTeamPermissions(userVm.UserID);

                        //Get Project Permissions...
                        List<ProjectPermissionViewModel> projectPermissions = GetProjectPermissions(userVm.UserID);

                        //Is User Admin...
                        bool isUserAdmin = _membershipService.IsUserAdmin(userVm.UserID);

                        //Get default Group...
                        Group defaultGroup = _membershipService.GetDefaultGroupForUser(userVm.UserID);
                        GroupViewModel defaultGroupVm = Mapper.Map<Group, GroupViewModel>(defaultGroup);

                        //Get default Team...
                        Team defaultTeam = _membershipService.GetDefaultTeamForUser(userVm.UserID);
                        TeamViewModel defaultTeamVm = Mapper.Map<Team, TeamViewModel>(defaultTeam);

                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true, userid = userVm.UserID, nickname = userVm.Nickname, fullname = userVm.Fullname, avatarpicpath = userVm.AvatarPicPath, profilepicpath = userVm.ProfilePicPath, profilequote = userVm.ProfileQuote, appPermissions = appPermissions, groupPermissions = groupPermissions, teamPermissions = teamPermissions, projectPermissions = projectPermissions, isUserAdmin = isUserAdmin, defaultGroup = defaultGroupVm, defaultTeam = defaultTeamVm });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }

        #region "Permission Methods"
        private List<PermissionViewModel> GetApplicationPermissions(int userId)
        {
            //Get App Permissions...
            List<Permission> ap = _membershipService.GetAppPermissions(userId);
            List<PermissionViewModel> appPermissions = new List<PermissionViewModel>();

            foreach (Permission p in ap)
            {
                PermissionViewModel pvm = Mapper.Map<Permission, PermissionViewModel>(p);
                appPermissions.Add(pvm);
            }

            return appPermissions;
        }

        private List<GroupPermissionViewModel> GetGroupPermissions(int userId)
        {
            List<Group> userGroups = _membershipService.GetGroupsForUser(userId);
            List<GroupPermissionViewModel> groupPermissions = new List<GroupPermissionViewModel>();

            foreach (Group grp in userGroups)
            {
                GroupPermissionViewModel gpVm = new GroupPermissionViewModel();
                gpVm.GroupID = grp.ID;
                gpVm.GroupName = grp.Name;
                gpVm.Permissions = new List<PermissionViewModel>();

                List<Permission> grpPermissions = _membershipService.GetGroupPermissions(userId, grp.ID);

                foreach (Permission p in grpPermissions)
                {
                    PermissionViewModel pvm = Mapper.Map<Permission, PermissionViewModel>(p);
                    gpVm.Permissions.Add(pvm);
                }

                groupPermissions.Add(gpVm);
            }

            return groupPermissions;
        }

        private List<TeamPermissionViewModel> GetTeamPermissions(int userId)
        {
            List<Team> userTeams = _membershipService.GetTeamsForUser(userId);
            List<TeamPermissionViewModel> teamPermissions = new List<TeamPermissionViewModel>();

            foreach (Team tm in userTeams)
            {
                TeamPermissionViewModel tpVm = new TeamPermissionViewModel();
                tpVm.TeamID = tm.ID;
                tpVm.TeamName = tm.Name;
                tpVm.Permissions = new List<PermissionViewModel>();

                List<Permission> tmPermissions = _membershipService.GetTeamPermissions(userId, tm.ID);

                foreach (Permission p in tmPermissions)
                {
                    PermissionViewModel pvm = Mapper.Map<Permission, PermissionViewModel>(p);
                    tpVm.Permissions.Add(pvm);
                }

                teamPermissions.Add(tpVm);
            }

            return teamPermissions;
        }

        private List<ProjectPermissionViewModel> GetProjectPermissions(int userId)
        {
            List<Project> userProjects = _membershipService.GetProjectsForUser(userId);
            List<ProjectPermissionViewModel> projectPermissions = new List<ProjectPermissionViewModel>();

            foreach (Project proj in userProjects)
            {
                ProjectPermissionViewModel ppVm = new ProjectPermissionViewModel();
                ppVm.ProjectID = proj.ID;
                ppVm.ProjectName = proj.Name;
                ppVm.Permissions = new List<PermissionViewModel>();

                List<Permission> projPermissions = _membershipService.GetProjectPermissions(userId, proj.ID);

                foreach (Permission p in projPermissions)
                {
                    PermissionViewModel pvm = Mapper.Map<Permission, PermissionViewModel>(p);
                    ppVm.Permissions.Add(pvm);
                }

                projectPermissions.Add(ppVm);
            }

            return projectPermissions;
        }

        #endregion

        [Authorize]
        [HttpGet]
        [Route("scrum/roles")]
        public HttpResponseMessage GetAllScrumRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Weekly.Entities.ScrumRole> _roles = _membershipService.GetScrumRoles();

                if (_roles != null)
                {
                    List<ScrumRoleViewModel> rolesVm = new List<ScrumRoleViewModel>();

                    foreach (Weekly.Entities.ScrumRole role in _roles)
                    {
                        ScrumRoleViewModel roleVm = Mapper.Map<Weekly.Entities.ScrumRole, ScrumRoleViewModel>(role);

                        if (roleVm != null)
                        {
                            rolesVm.Add(roleVm);
                        }
                    }

                    response = request.CreateResponse(HttpStatusCode.OK, rolesVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "No scrum roles registered in database!");
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("group")]
        public HttpResponseMessage GetAllUsersInGroup(HttpRequestMessage request, int groupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<User> allUsersInGroup = _membershipService.GetAllUsersInGroup(groupId);
                List<UserViewModel> allUsersVm = new List<UserViewModel>();
                
                foreach(User usr in allUsersInGroup)
                {
                    UserViewModel userVm = Mapper.Map<Weekly.Entities.User, UserViewModel>(usr);
                    allUsersVm.Add(userVm);
                }

                response = request.CreateResponse(HttpStatusCode.OK, allUsersVm);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAllUsers(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Weekly.Entities.User> _users = _membershipService.GetAllUsers();

                if (_users != null)
                {
                    List<UserViewModel> usersVm = new List<UserViewModel>();

                    foreach(Weekly.Entities.User usr in _users)
                    {
                        UserViewModel userVm = Mapper.Map<Weekly.Entities.User, UserViewModel>(usr);

                        if (userVm != null)
                        {
                            usersVm.Add(userVm);
                        }
                    }

                    response = request.CreateResponse(HttpStatusCode.OK, usersVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "No users registered in database!");
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("changepassword/{userId}")]
        public HttpResponseMessage GetByIdForChangePassword(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Weekly.Entities.User _user = _membershipService.GetUser(userId);

                if (_user != null)
                {
                    ChangePasswordViewModel userVm = new ChangePasswordViewModel();
                    userVm.UserID = _user.ID;
                    userVm.OldPassword = String.Empty;
                    userVm.NewPassword = String.Empty;

                    response = request.CreateResponse(HttpStatusCode.OK, userVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with id: " + userId + " not found!");
                }

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("{userId}")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int userId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Weekly.Entities.User _user = _membershipService.GetUser(userId);
                UserViewModel userVm = Mapper.Map<Weekly.Entities.User, UserViewModel>(_user);

                if (_user != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with id: " + userId + " not found!");
                }

                return response;
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getbyusername")]
        public HttpResponseMessage GetByUserName(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Weekly.Entities.User _user = _membershipService.GetUser(username);
                UserViewModel userVm = Mapper.Map<Weekly.Entities.User, UserViewModel>(_user);

                if (_user != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, userVm);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "User with username: " + username + " not found!");
                }

                return response;
            });
        }


        [Authorize]
        [Route("changepassword")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, ChangePasswordViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "Change password failed due to bad request data!");
                }
                else
                {
                    //_membershipService.ResetPassword(user.Email, user.HomeUrl);
                    _membershipService.ChangePassword(user.UserID, user.OldPassword, user.NewPassword);
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                }

                return response;
            });
        }

        [AllowAnonymous]
        [Route("resetpassword")]
        [HttpPost]
        public HttpResponseMessage ResetPassword(HttpRequestMessage request, ResetPasswordViewModel user)
        {
           return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "Reset password failed due to bad request data!");
                }
                else
                {
                    _membershipService.ResetPassword(user.Email, user.HomeUrl);
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                }

                return response;
            });
        }

        [Authorize]
        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, UserViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {
                    _membershipService.UpdateUser(user.UserID, user.Username, user.Email, user.ProfileQuote, user.AvatarPicPath, user.ProfilePicPath, user.Nickname);
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                }

                return response;
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {
                    Weekly.Entities.User _user = _membershipService.CreateUser(user.Username, user.Email, user.Password, user.Group.GroupID, user.Team.TeamID, new int[] { 1 }, user.Nickname);

                    if (_user != null)
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