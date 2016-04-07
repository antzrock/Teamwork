using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Extensions;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository;
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
        private readonly IEntityBaseRepository<Group> _groupsRepository;
        private readonly IEntityBaseRepository<GroupRole> _groupRolesRepository;
        private readonly IEntityBaseRepository<GroupMember> _groupMemberRepository;
        private readonly IEntityBaseRepository<Team> _teamsRepository;
        private readonly IEntityBaseRepository<TeamRole> _teamRolesRepository;
        private readonly IEntityBaseRepository<TeamMember> _teamMemberRepository;
        private readonly IEntityBaseRepository<ScrumRole> _scrumRoleRepository;
        private readonly IEntityBaseRepository<UserStatus> _userStatusRepository;
        private readonly IEntityBaseRepository<MemberStatus> _memberStatusRepository;
        private readonly IEntityBaseRepository<Permission> _permissionRepository;
        private readonly IProjectService _projectService;
        private readonly IEncryptionService _encryptionService;
        private readonly IActiveDirectoryService _adService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public MembershipService(IEntityBaseRepository<User> userRepository, 
                                 IEntityBaseRepository<Role> roleRepository,
                                 IEntityBaseRepository<UserRole> userRoleRepository, 
                                 IEntityBaseRepository<Group> groupRepository,
                                 IEntityBaseRepository<GroupMember> groupMemberRepository,
                                 IEntityBaseRepository<GroupRole> groupRolesRepository, 
                                 IEntityBaseRepository<Team> teamRepository,
                                 IEntityBaseRepository<TeamRole> teamRolesRepository,
                                 IEntityBaseRepository<TeamMember> teamMemberRespository, 
                                 IEntityBaseRepository<ScrumRole> scrumRoleRepository,
                                 IEntityBaseRepository<UserStatus> userStatusRepository,
                                 IEntityBaseRepository<MemberStatus> memberStatusRepository,
                                 IEntityBaseRepository<Permission> permissionRepository,
                                 IProjectService projectService,
        IEncryptionService encryptionService, IUnitOfWork unitOfWork, IActiveDirectoryService adService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _groupsRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
            _groupRolesRepository = groupRolesRepository;
            _teamsRepository = teamRepository;
            _teamRolesRepository = teamRolesRepository;
            _teamMemberRepository = teamMemberRespository;
            _scrumRoleRepository = scrumRoleRepository;
            _userStatusRepository = userStatusRepository;
            _memberStatusRepository = memberStatusRepository;
            _encryptionService = encryptionService;
            _emailService = emailService;
            _permissionRepository = permissionRepository;
            _projectService = projectService;
            _unitOfWork = unitOfWork;
            _adService = adService;
        }

        #region Helper 

        private void addUserToTeam(User user, int teamId, bool defaultTeam)
        {
            Team existingTeam = _teamsRepository.GetSingle(teamId);

            if (existingTeam == null)
                throw new ApplicationException("Team doesn't exist.");

            // TEAM ROLE (DEFAULT)
            TeamRole existingTeamRole = _teamRolesRepository.FindBy(tr => tr.Name == "TeamMember").FirstOrDefault();

            if (existingTeamRole == null)
                throw new ApplicationException("Team role doesn't exist.");

            // MEMBER STATUS (DEFAULT)
            MemberStatus defaultMemberStatus = _memberStatusRepository.FindBy(ms => ms.Name == "Active").FirstOrDefault();

            if (defaultMemberStatus == null)
                throw new ApplicationException("Member status (ACTIVE) does not exist");

            List<TeamRole> defaultTeamRoles = new List<TeamRole>();
            defaultTeamRoles.Add(existingTeamRole);

            var teamMember = new TeamMember()
            {
                TeamID = existingTeam.ID,
                Team = existingTeam,
                UserID = user.ID,
                User = user,
                Roles = defaultTeamRoles,
                MemberStatusID = defaultMemberStatus.ID,
                MemberStatus = defaultMemberStatus,
                IsUserDefault = defaultTeam,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };

            _teamMemberRepository.Add(teamMember);
        }

        private void addUserToGroup(User user, int groupId, bool isDefaultGroup)
        {
            Group existingGroup = _groupsRepository.GetSingle(groupId);

            if (existingGroup == null)
                throw new ApplicationException("Group doesn't exist.");

            // GROUP ROLE (DEFAULT)
            GroupRole existingGroupRole = _groupRolesRepository.FindBy(gr => gr.Name == "GroupMember").FirstOrDefault();

            if (existingGroupRole == null)
                throw new ApplicationException("Group role doesn't exist.");

            List<GroupRole> defaultGroupRoles = new List<GroupRole>();
            defaultGroupRoles.Add(existingGroupRole);

            // MEMBER STATUS (DEFAULT)
            MemberStatus defaultMemberStatus = _memberStatusRepository.FindBy(ms => ms.Name == "Active").FirstOrDefault();

            if (defaultMemberStatus == null)
                throw new ApplicationException("Member status (ACTIVE) does not exist");


            var groupMember = new GroupMember()
            {
                GroupID = existingGroup.ID,
                Group = existingGroup,
                UserID = user.ID,
                User = user,
                Roles = defaultGroupRoles,
                MemberStatusID = defaultMemberStatus.ID,
                MemberStatus = defaultMemberStatus,
                IsUserDefault = isDefaultGroup,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };

            _groupMemberRepository.Add(groupMember);
        }

        private void addUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.ID,
                Role = role,
                UserId = user.ID,
                User = user,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _userRoleRepository.Add(userRole);
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
        #endregion

        public List<Group> GetUserGroups(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.GroupMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User with ID: " + userId + " does not exist!");

            List<Group> userGroups = new List<Group>();

            foreach(GroupMember gm in existingUser.GroupMembership)
            {
                Group existingGroup = _groupsRepository.FindBy(g => g.ID == gm.GroupID).FirstOrDefault();

                if (existingGroup != null)
                    userGroups.Add(existingGroup);
            }

            return userGroups;
        }

        public void UpdateUser(int userId, String username, String email, String profilequote, String avatarfilename, String profilefilename, String nickname)
        {
            var existingUser = _userRepository.GetSingle(userId);

            if (existingUser == null)
            {
                throw new Exception("User with ID: " + userId + " does not exist!");
            }

            if (!existingUser.Username.Equals(username))
            {
                //Username changed, check if already taken....
                var usernameUser = _userRepository.GetSingleByUsername(username);

                if (usernameUser != null)
                {
                    throw new Exception("Username is already in use");
                }
                else
                {
                    existingUser.Username = username;
                }
            }

            if (!existingUser.Email.Equals(email))
            {
                //Email changed, check if already taken...
                var emailUser = _userRepository.GetSingleByEmail(email);

                if (emailUser != null)
                {
                    throw new Exception("Email is already in use");
                }
                else
                {
                    existingUser.Email = email;
                }

            }
            
            if (String.IsNullOrWhiteSpace(existingUser.ProfileQuote) ||  !existingUser.ProfileQuote.Equals(profilequote))
            {
                existingUser.ProfileQuote = profilequote;
            }

            if (String.IsNullOrWhiteSpace(existingUser.AvatarPicPath) || !existingUser.AvatarPicPath.Equals(avatarfilename))
            {
                existingUser.AvatarPicPath = avatarfilename;
            }

            if (String.IsNullOrWhiteSpace(existingUser.ProfilePicPath) || !existingUser.ProfilePicPath.Equals(profilefilename))
            {
                existingUser.ProfilePicPath = profilefilename;
            }

            if (String.IsNullOrWhiteSpace(existingUser.Nickname) || !existingUser.Nickname.Equals(nickname))
            {
                existingUser.Nickname = nickname;
            }

            //Update fullname from AD
            existingUser.Fullname = _adService.GetFullnameUsingEmail(existingUser.Email);

            //Update Job Title from AD
            existingUser.Title = _adService.GetTitleUsingEmail(existingUser.Email);

            existingUser.UpdatedBy = existingUser.ID;
            existingUser.UpdatedDate = DateTime.Now;
            
            _userRepository.Edit(existingUser);

            _unitOfWork.Commit();

        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var existingUser = _userRepository.GetSingle(userId);

            if (existingUser == null)
            {
                throw new Exception("User ID is not registered!");
            }

            //Validate old password...
            if (!isPasswordValid(existingUser, oldPassword))
            {
                throw new Exception("Old password is invalid!");
            }

            //Generate new password salt...
            var passwordSalt = _encryptionService.CreateSalt();

            existingUser.Salt = passwordSalt;
            existingUser.HashedPassword = _encryptionService.EncryptPassword(newPassword, passwordSalt);

            _userRepository.Edit(existingUser);
            _unitOfWork.Commit();   
        }

        public void ResetPassword(string email, string homeUrl)
        {
            var existingUser = _userRepository.GetSingleByEmail(email);

            if (existingUser == null)
            {
                throw new Exception("Email is not registered");
            }

            var passwordSalt = _encryptionService.CreateSalt();
            String randomPassword = GeneratePassword();

            existingUser.Salt = passwordSalt;
            existingUser.HashedPassword = _encryptionService.EncryptPassword(randomPassword, passwordSalt);
            _userRepository.Edit(existingUser);
            _unitOfWork.Commit();

            //Create email
            MailMessage emailMessage = new MailMessage();
            emailMessage.To.Add(new MailAddress(email));
            emailMessage.Subject = "Teamwork: Password reset request";

            // Read the file as one string.
            String bodyString = GetPasswordResetTemplate();
            emailMessage.Body = String.Format(bodyString, existingUser.Fullname, randomPassword, homeUrl);
            emailMessage.IsBodyHtml = true;

            _emailService.SendAsync(emailMessage);
            
        }

        public User CreateUser(string username, string email, string password, int groupId, int teamId, int[] roles, string nickname)
        {
            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var existingUserEmail = _userRepository.GetSingleByEmail(email);

            if (existingUserEmail != null)
            {
                throw new Exception("Email is already in use");
            }

            var existingUserStatus = _userStatusRepository.FindBy(us => us.Name == "Active").FirstOrDefault();

            if (existingUserStatus == null)
            {
                throw new Exception("User status (ACTIVE) not found in database!");
            }

            var passwordSalt = _encryptionService.CreateSalt();
            var fullName = _adService.GetFullnameUsingEmail(email);
            var jobTitle = _adService.GetTitleUsingEmail(email);

            var user = new User()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                Fullname = fullName,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now,
                Nickname = nickname,
                Title = jobTitle,
                CreatedBy = 1,
                AvatarPicPath = "default_avatar.png",
                ProfilePicPath = "default_profile.png",
                CreatedDate = DateTime.Now,
                UserStatusID = existingUserStatus.ID,
                UserStatus = existingUserStatus
            };

            _userRepository.Add(user);

            _unitOfWork.Commit();
            
            // ROLE
            Role defaultRole = _roleRepository.FindBy(r => r.Name == "User").FirstOrDefault();

            if (defaultRole != null)
            {
                addUserToRole(user, defaultRole.ID);
            }

            _unitOfWork.Commit();

            // GROUP MEMBERSHIP
            addUserToGroup(user, groupId, true);
            _unitOfWork.Commit();

            // TEAM MEMBERSHIP
            addUserToTeam(user, teamId, true);
            _unitOfWork.Commit();
            
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.All.ToList();
        }

        public User GetUser(int userId)
        {
            return _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();
        }

        public User GetUser(string userName)
        {
            return _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership, u => u.TeamMembership).Where(u => u.Username == userName).FirstOrDefault();
        }

        public User GetUserByFullname(string fullName)
        {
            return _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership, u => u.TeamMembership).Where(u => u.Fullname == fullName).FirstOrDefault();
        }

        public List<ScrumRole> GetScrumRoles()
        {
            return _scrumRoleRepository.All.ToList();
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();

            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    _result.Add(userRole.Role);
                }
            }

            return _result.Distinct().ToList();
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }

        #region "Password Helpers"
        private String GeneratePassword(int genlen = 20, bool usenumbers = true, bool uselowalphabets = true, bool usehighalphabets = true, bool usesymbols = true)
        {

            var upperCase = new char[]
                {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
                };

            var lowerCase = new char[]
                {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
                };

            var numerals = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var symbols = new char[]
                {
                '~', '`', '!', '@', '#', '$', '%', '^', '&', '*'
                };

            char[] total = (new char[0])
                            .Concat(usehighalphabets ? upperCase : new char[0])
                            .Concat(uselowalphabets ? lowerCase : new char[0])
                            .Concat(usenumbers ? numerals : new char[0])
                            .Concat(usesymbols ? symbols : new char[0])
                            .ToArray();

            var rnd = new Random();

            var chars = Enumerable
                .Repeat<int>(0, genlen)
                .Select(i => total[rnd.Next(total.Length)])
                .ToArray();

            return new string(chars);
        }

        private String GetPasswordResetTemplate()
        {
            StringBuilder templateStr = new StringBuilder();

            templateStr.Append("<!DOCTYPE html>");
            templateStr.Append("<html style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \">");
            templateStr.Append("  <head>");
            templateStr.Append("  <meta name=\"viewport\" content=\"width = device - width\">");
            templateStr.Append("  <meta http-equiv=\"Content - Type\" content=\"text / html; charset = UTF - 8\">");
            templateStr.Append("  <title>Teamwork: Password Reset Email</title>");
            templateStr.Append("</head>");
            templateStr.Append("<body bgcolor=\"#f6f6f6\" style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6em; -webkit-font-smoothing: antialiased; height: 100%; -webkit-text-size-adjust: none; width: 100% !important; margin: 0; padding: 0;\">");
            templateStr.Append("  <table class=\"body - wrap\" bgcolor=\"#f6f6f6\" style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6em; width: 100%; margin: 0; padding: 20px;\"><tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6em; margin: 0; padding: 0;\">");
            templateStr.Append("    <tr>");
            templateStr.Append("      <td style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \"></td>");
            templateStr.Append("      <td class=\"container\" bgcolor=\"#FFFFFF\" style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6em; clear: both !important; display: block !important; max-width: 600px !important; Margin: 0 auto; padding: 20px; border: 1px solid #f0f0f0;\">");
            templateStr.Append("        <div class=\"content\" style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; display: block; max - width: 600px; margin: 0 auto; padding: 0; \">");
            templateStr.Append("          <table style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; width: 100 %; margin: 0; padding: 0; \"><tr style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \">");
            templateStr.Append("            <td style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \">");
            //Parameter here {0}
            templateStr.Append("              <h1 style=\"font - family: 'Helvetica Neue', Helvetica, Arial, 'Lucida Grande', sans - serif; font - size: 34px; line - height: 1.2em; color: #111111; font-weight: 200; margin: 40px 0 20px; padding: 0;\">Hello, {0} </h1>");

            //Parameter here {1}
            templateStr.Append("              <p style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 22px; line - height: 1.6em; font - weight: 200; margin: 0 0 30px; padding: 0; \">Your teamwork password has been reset: <span style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 22px; color: blue; line - height: 1.6em; font - weight: normal; margin: 0 0 30px; padding: 0; \">{1} </span></p>");

            templateStr.Append("              <p style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 22px; line - height: 1.6em; font - weight: normal; margin: 0 0 10px; padding: 0; \">Thanks and have a nice day!</p>");
            templateStr.Append("              <p style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 22px; line - height: 1.6em; font - weight: normal; margin: 0 0 10px; padding: 0; \">Teamwork Team</p>");
            templateStr.Append("              <table class=\"btn - primary\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; width: auto !important; Margin: 0 0 10px; padding: 0; \"><tr style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \">");
            templateStr.Append("                <tr>");
            templateStr.Append("                  <td style=\"font - family: 'Helvetica Neue', Helvetica, Arial, 'Lucida Grande', sans - serif; font - size: 14px; line - height: 1.6em; border - radius: 25px; text - align: center; vertical - align: top; background: #348eda; margin: 0; padding: 0;\" align=\"center\" bgcolor=\"#348eda\" valign=\"top\">");

            //Parameter here {2}
            templateStr.Append("                    <a href=\"{2}\" style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 2; color: #ffffff; border-radius: 25px; display: inline-block; cursor: pointer; font-weight: bold; text-decoration: none; background: #348eda; margin: 0; padding: 0; border-color: #348eda; border-style: solid; border-width: 10px 20px;\">Login to Teamwork</a>");

            templateStr.Append("                  </td>");
            templateStr.Append("                </tr>");
            templateStr.Append("              </table>");
            templateStr.Append("            </td>");
            templateStr.Append("          </table>");
            templateStr.Append("        </div>");
            templateStr.Append("      </td>");
            templateStr.Append("    </tr>");
            templateStr.Append("  </table>");
            templateStr.Append("  <table class=\"footer - wrap\" style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; clear: both !important; width: 100 %; margin: 0; padding: 0; \"><tr style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \">");
            templateStr.Append("    <tr>");
            templateStr.Append("      <td style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \"></td>");
            templateStr.Append("      <td class=\"container\" style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; clear: both !important; display: block !important; max - width: 600px !important; margin: 0 auto; padding: 0; \">");
            templateStr.Append("      </td>");
            templateStr.Append("      <td style=\"font - family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif; font - size: 100 %; line - height: 1.6em; margin: 0; padding: 0; \"></td>");
            templateStr.Append("    </tr>");
            templateStr.Append("  </table>");
            templateStr.Append("</body>");
            templateStr.Append("</html>");
            
            return templateStr.ToString();
        }

        public List<Permission> GetAppPermissions(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.UserRoles).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetAppPermissions!");

            List<Permission> userPermissions = new List<Permission>();

            foreach(UserRole usrRole in existingUser.UserRoles)
            {
                foreach(Permission usrPermission in usrRole.Role.DefaultPermissions)
                {
                    //Check if permission already added....
                    Permission checkPermission = userPermissions.Where(p => p.ID == usrPermission.ID).FirstOrDefault();

                    if (checkPermission == null)
                        userPermissions.Add(usrPermission);
                }
            }

            return userPermissions;
        }

        public List<Permission> GetGroupPermissions(int userId, int groupId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetGroupPermissions!");

            List<Permission> usrGroupPermissions = new List<Permission>();

            if (existingUser.GroupMembership != null)
            {
                foreach (GroupMember gm in existingUser.GroupMembership)
                {
                    foreach (GroupRole gr in gm.Roles)
                    {
                        foreach (Permission grpPermissions in gr.DefaultPermissions)
                        {
                            //Check if permission already added...
                            Permission checkPermission = usrGroupPermissions.Where(p => p.ID == grpPermissions.ID).FirstOrDefault();

                            if (checkPermission == null)
                                usrGroupPermissions.Add(grpPermissions);
                        }
                    }
                }
            }

            return usrGroupPermissions;
        }

        public List<Permission> GetTeamPermissions(int userId, int teamId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetTeamPermissions!");

            List<Permission> usrTeamPermissions = new List<Permission>();

            if (existingUser.TeamMembership != null)
            {
                foreach (TeamMember tm in existingUser.TeamMembership)
                {
                    if (tm.TeamID == teamId)
                    {
                        foreach (TeamRole tr in tm.Roles)
                        {
                            foreach (Permission tp in tr.DefaultPermissions)
                            {
                                Permission checkPermission = usrTeamPermissions.Where(p => p.ID == tp.ID).FirstOrDefault();

                                if (checkPermission == null)
                                    usrTeamPermissions.Add(tp);
                            }
                        }
                    }
                }
            }

            return usrTeamPermissions;

        }

        public List<Permission> GetProjectPermissions(int userId, int projectId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.ProjectScrumTeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetTeamPermissions!");

            List<Permission> usrProjectPermissions = new List<Permission>();

            if (existingUser.ProjectScrumTeamMembership != null)
            {
                foreach(ScrumTeamMember stm in existingUser.ProjectScrumTeamMembership)
                {
                    if (stm.ProjectID == projectId)
                    {
                        foreach(ScrumRole sr in stm.Roles)
                        {
                            foreach(Permission p in sr.DefaultPermissions)
                            {
                                Permission checkPermission = usrProjectPermissions.Where(perm => perm.ID == p.ID).FirstOrDefault();

                                if (checkPermission == null)
                                    usrProjectPermissions.Add(p);
                            }
                        }
                    }
                }
            }

            return usrProjectPermissions;
        }

        public List<Group> GetGroupsForUser(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetGroupPermissions!");

            List<Group> userGroups = new List<Group>();

            foreach(GroupMember gm in existingUser.GroupMembership)
            {
                Group grp = _groupsRepository.FindBy(gr => gr.ID == gm.GroupID).FirstOrDefault();

                if (grp != null)
                    userGroups.Add(grp);
            }

            return userGroups;
        }

        public List<Team> GetTeamsForUser(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetGroupPermissions!");

            List<Team> userTeams = new List<Team>();

            foreach (TeamMember tm in existingUser.TeamMembership)
            {
                Team team = _teamsRepository.FindBy(t => t.ID == tm.TeamID).FirstOrDefault();

                if (team != null)
                    userTeams.Add(team);
            }

            return userTeams;
        }

        public List<Project> GetProjectsForUser(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.ProjectScrumTeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetGroupPermissions!");

            List<Project> userProjects = _projectService.GetAllProjects(existingUser.ID);

            return userProjects;
        }

        public bool IsUserAdmin(int userId)
        {
            bool isAdmin = false;
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.UserRoles).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in IsUserAdmin!");

            UserRole AdminRole = existingUser.UserRoles.Where(ur => ur.Role.Name == "SuperAdmin").FirstOrDefault();

            if (AdminRole == null)
                isAdmin = false;
            else
                isAdmin = true;

            return isAdmin;
        }

        public Group GetDefaultGroupForUser(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.GroupMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetDefaultGroupForUser!");

            Group defaultGroup = new Group();
            GroupMember defaultGroupMember = new GroupMember();

            defaultGroupMember = existingUser.GroupMembership.Where(gm => gm.IsUserDefault == true).FirstOrDefault();
            
            if (defaultGroupMember == null)
                throw new Exception("Default Group is not set for user in GetDefaultGroupForUser");

            defaultGroup = _groupsRepository.FindBy(g => g.ID == defaultGroupMember.GroupID).FirstOrDefault();

            if (defaultGroup == null)
                throw new Exception("Default Group does not exist in database in GetDefaultGroupForUser");
            
            return defaultGroup;
        }

        public int GetUsersCount()
        {
            return _userRepository.GetAll().Count();
        }

        public List<User> GetAllUsersForMaintenance(int currPage, int currPageSize)
        {
            return _userRepository.AllIncluding(u => u.UserStatus).OrderBy(u => u.Fullname).Skip(currPage * currPageSize).Take(currPageSize).ToList();
        }

        public List<User> GetAllUsersFilteredForMaintenance(int currPage, int currPageSize, string fieldFilter)
        {
            return _userRepository.AllIncluding(u => u.UserStatus).Where(u => u.Fullname.Contains(fieldFilter) || u.Nickname.Contains(fieldFilter) || u.Email.Contains(fieldFilter) || u.UserStatus.Name.Contains(fieldFilter)).OrderBy(u => u.Fullname).Skip(currPage * currPageSize).Take(currPageSize).ToList();
        }

        public Team GetDefaultTeamForUser(int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in GetDefaultTeamForUser!");

            Team defaultTeam = new Team();
            TeamMember defaultTeamMember = new TeamMember();

            defaultTeamMember = existingUser.TeamMembership.Where(tm => tm.IsUserDefault == true).FirstOrDefault();

            if (defaultTeamMember == null)
                throw new Exception("Default Team is not set for user in GetDefaultTeamForUser");

            defaultTeam = _teamsRepository.FindBy(t => t.ID == defaultTeamMember.TeamID).FirstOrDefault();

            if (defaultTeam == null)
                throw new Exception("Default Team does not exist in database in GetDefaultTeamForUser");

            return defaultTeam;
        }

        public List<User> GetAllUsersInGroup(int groupId)
        {
            Group existingGroup = _groupsRepository.AllIncluding(g => g.Members).Where(g => g.ID == groupId).FirstOrDefault();

            if (existingGroup == null)
                throw new Exception("Group does not exist in database in GetAllUsersForGroup!");

            List<User> allUsersInGroup = new List<User>();

            foreach(GroupMember gm in existingGroup.Members)
            {
                User usr = _userRepository.FindBy(u => u.ID == gm.UserID).FirstOrDefault();

                if (usr != null)
                    allUsersInGroup.Add(usr);
            }

            return allUsersInGroup;
        }

        public List<Permission> GetAllApplicationPermissions()
        {
            return _permissionRepository.GetAll().ToList();
        }

        public List<GroupRole> GetAllGroupRoles()
        {
            return _groupRolesRepository.GetAll().ToList();
        }

        public List<TeamRole> GetAllTeamRoles()
        {
            return _teamRolesRepository.GetAll().ToList();
        }

        public List<ScrumRole> GetAllProjectRoles()
        {
            return _scrumRoleRepository.GetAll().ToList();
        }

        public List<Role> GetAllApplicationRoles()
        {
            return _roleRepository.GetAll().ToList();
        }

        private bool ApplicationRoleHasUsers(int roleId)
        {
            bool hasUsers = false;

            if (_userRoleRepository.AllIncluding(ur => ur.RoleId == roleId).Count() > 0)
                hasUsers = true;

            return hasUsers;
        }

        public void SaveProjectRoles(List<ScrumRole> projectRoles, int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in SaveProjectRoles!");

            List<ScrumRole> existingProjectRoles = _scrumRoleRepository.AllIncluding(sr => sr.DefaultPermissions).ToList();

            if (existingProjectRoles != null)
            {
                //Delete missing roles...
                List<ScrumRole> deletedRoles = existingProjectRoles.Where(pr => !projectRoles.Any(pr2 => pr2.ID == pr.ID)).ToList();

                foreach(ScrumRole deleteThis in deletedRoles)
                {
                    if (deleteThis.isSystemGenerated)
                        throw new Exception("Cannot delete system generated project roles");

                    _scrumRoleRepository.Delete(deleteThis);
                }

                //Edit existing roles...
                foreach(ScrumRole editThis in existingProjectRoles)
                {
                    ScrumRole editFrom = projectRoles.FirstOrDefault(pr => pr.ID == editThis.ID);

                    if (editFrom != null)
                    {
                        if(!editThis.isSystemGenerated)
                        {
                            editThis.Name = editFrom.Name;
                        }

                        editThis.UpdatedBy = existingUser.ID;
                        editThis.UpdatedDate = DateTime.Now;

                        //Delete all permissions
                        List<Permission> deletedPermissions = editThis.DefaultPermissions.ToList();

                        foreach (Permission deleteThis in deletedPermissions)
                        {
                            editThis.DefaultPermissions.Remove(deleteThis);
                        }

                        //Add updated permissions...
                        foreach (Permission addThis in editFrom.DefaultPermissions)
                        {
                            editThis.DefaultPermissions.Add(addThis);
                        }

                        _scrumRoleRepository.Edit(editThis);
                    }

                    //Add new roles...
                    List<ScrumRole> newRoles = projectRoles.Where(pr => pr.ID == 0).ToList();

                    foreach(ScrumRole addThis in newRoles)
                    {
                        addThis.CreatedBy = existingUser.ID;
                        addThis.CreatedDate = DateTime.Now;

                        _scrumRoleRepository.Add(addThis);
                    }

                    _unitOfWork.Commit();
                }
            }

        }

        public void SaveTeamRoles(List<TeamRole> teamRoles, int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in SaveTeamRoles!");

            List<TeamRole> existingTeamRoles = _teamRolesRepository.AllIncluding(tr => tr.DefaultPermissions).ToList();

            if (existingTeamRoles != null)
            {
                //Delete missing roles...
                List<TeamRole> deletedRoles = existingTeamRoles.Where(tr => !teamRoles.Any(tr2 => tr2.ID == tr.ID)).ToList();

                foreach (TeamRole deleteThis in deletedRoles)
                {
                    if (deleteThis.isSystemGenerated)
                        throw new Exception("Cannot delete system generated team roles");

                    _teamRolesRepository.Delete(deleteThis);
                }

                //Edit existing roles...
                foreach (TeamRole editThis in existingTeamRoles)
                {
                    TeamRole editFrom = teamRoles.FirstOrDefault(tr => tr.ID == editThis.ID);

                    if (editFrom != null)
                    {
                        if (!editThis.isSystemGenerated)
                        {
                            editThis.Name = editFrom.Name;
                        }

                        editThis.UpdatedBy = existingUser.ID;
                        editThis.UpdatedDate = DateTime.Now;

                        //Delete all permissions
                        List<Permission> deletedPermissions = editThis.DefaultPermissions.ToList();

                        foreach (Permission deleteThis in deletedPermissions)
                        {
                            editThis.DefaultPermissions.Remove(deleteThis);
                        }

                        //Add updated permissions...
                        foreach (Permission addThis in editFrom.DefaultPermissions)
                        {
                            editThis.DefaultPermissions.Add(addThis);
                        }

                        _teamRolesRepository.Edit(editThis);
                    }

                    //Add new roles...
                    List<TeamRole> newRoles = teamRoles.Where(gr => gr.ID == 0).ToList();

                    foreach (TeamRole addThis in newRoles)
                    {
                        addThis.CreatedBy = existingUser.ID;
                        addThis.CreatedDate = DateTime.Now;

                        _teamRolesRepository.Add(addThis);
                    }

                    _unitOfWork.Commit();
                }
            }
        }

        public void SaveGroupRoles(List<GroupRole> groupRoles, int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in SaveGroupRoles!");

            List<GroupRole> existingGroupRoles = _groupRolesRepository.AllIncluding(gr => gr.DefaultPermissions).ToList();

            if (existingGroupRoles != null)
            {
                //Delete missing roles...
                List<GroupRole> deletedRoles = existingGroupRoles.Where(gr => !groupRoles.Any(gr2 => gr2.ID == gr.ID)).ToList();

                foreach (GroupRole deleteThis in deletedRoles)
                {
                    if (deleteThis.isSystemGenerated)
                        throw new Exception("Cannot delete system generated group roles");

                    _groupRolesRepository.Delete(deleteThis);
                }

                //Edit existing roles...
                foreach (GroupRole editThis in existingGroupRoles)
                {
                    GroupRole editFrom = groupRoles.FirstOrDefault(gr => gr.ID == editThis.ID);

                    if (editFrom != null)
                    {
                        if (!editThis.isSystemGenerated)
                        {
                            editThis.Name = editFrom.Name;
                        }

                        editThis.UpdatedBy = existingUser.ID;
                        editThis.UpdatedDate = DateTime.Now;

                        //Delete all permissions
                        List<Permission> deletedPermissions = editThis.DefaultPermissions.ToList();

                        foreach (Permission deleteThis in deletedPermissions)
                        {
                            editThis.DefaultPermissions.Remove(deleteThis);
                        }

                        //Add updated permissions...
                        foreach (Permission addThis in editFrom.DefaultPermissions)
                        {
                            editThis.DefaultPermissions.Add(addThis);
                        }

                        _groupRolesRepository.Edit(editThis);
                    }

                    //Add new roles...
                    List<GroupRole> newRoles = groupRoles.Where(gr => gr.ID == 0).ToList();

                    foreach (GroupRole addThis in newRoles)
                    {
                        addThis.CreatedBy = existingUser.ID;
                        addThis.CreatedDate = DateTime.Now;

                        _groupRolesRepository.Add(addThis);
                    }

                    _unitOfWork.Commit();
                }
            }
        }

        public void SaveApplicationRoles(List<Role> appRoles, int userId)
        {
            User existingUser = _userRepository.AllIncluding(u => u.UserStatus, u => u.TeamMembership).Where(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User does not exist in SaveApplicationRoles!");

            List<Role> existingAppRoles = _roleRepository.AllIncluding(r => r.DefaultPermissions).ToList();

            if (existingAppRoles != null)
            {
                //Delete missing roles...
                List<Role> deletedRoles = existingAppRoles.Where(ar => !appRoles.Any(ar2 => ar2.ID == ar.ID)).ToList();

                foreach (Role deleteThis in deletedRoles)
                {
                    if (deleteThis.isSystemGenerated)
                        throw new Exception("Cannot delete system generated roles...");

                    _roleRepository.Delete(deleteThis);
                }

                //Edit existing roles...
                foreach (Role editThis in existingAppRoles)
                {
                    Role editFrom = appRoles.FirstOrDefault(r => r.ID == editThis.ID);

                    if (editFrom != null && !editThis.isSystemGenerated)
                    {
                        editThis.Name = editFrom.Name;
                        editThis.UpdatedBy = existingUser.ID;
                        editThis.UpdatedDate = DateTime.Now;

                        //Delete all permissions...
                        List<Permission> deletedPermissions = editThis.DefaultPermissions.ToList();

                        foreach (Permission deleteThis in deletedPermissions)
                        {
                            editThis.DefaultPermissions.Remove(deleteThis);
                        }

                        //Add updated permissions...
                        foreach (Permission addThis in editFrom.DefaultPermissions)
                        {
                            editThis.DefaultPermissions.Add(addThis);
                        }

                        _roleRepository.Edit(editThis);
                    }
                }

                //Add new roles...
                List<Role> newRoles = appRoles.Where(r => r.ID == 0).ToList();

                foreach (Role addThis in newRoles)
                {
                    addThis.CreatedBy = existingUser.ID;
                    addThis.CreatedDate = DateTime.Now;

                    _roleRepository.Add(addThis);
                }

                _unitOfWork.Commit();
            }
        }
        #endregion

    }
}
