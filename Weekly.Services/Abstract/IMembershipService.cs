using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int groupId, int teamId, int[] roles, string nickname);

        User GetUser(int userId);
        List<Role> GetUserRoles(string username);

        User GetUser(string userName);

        User GetUserByFullname(string fullName);

        void ResetPassword(string email, string homeUrl);

        void UpdateUser(int userId, String username, String email, String profilequote, String avatarfilename, String profilefilename, String nickname);

        void ChangePassword(int userId, string oldPassword, string newPassword);

        List<User> GetAllUsers();

        List<ScrumRole> GetScrumRoles();

        List<Permission> GetAllApplicationPermissions();

        List<Permission> GetAppPermissions(int userId);

        List<Permission> GetGroupPermissions(int userId, int groupId);

        List<Permission> GetTeamPermissions(int userId, int teamId);

        List<Permission> GetProjectPermissions(int userId, int projectId);

        List<Group> GetGroupsForUser(int userId);

        List<Team> GetTeamsForUser(int userId);

        List<Project> GetProjectsForUser(int userId);

        bool IsUserAdmin(int userId);

        Group GetDefaultGroupForUser(int userId);

        Team GetDefaultTeamForUser(int userId);

        List<User> GetAllUsersInGroup(int groupId);

        List<Role> GetAllApplicationRoles();

        void SaveApplicationRoles(List<Role> appRoles, int userId);

        List<GroupRole> GetAllGroupRoles();

        void SaveGroupRoles(List<GroupRole> groupRoles, int userId);

        List<TeamRole> GetAllTeamRoles();

        void SaveTeamRoles(List<TeamRole> teamRoles, int userId);

        List<ScrumRole> GetAllProjectRoles();

        void SaveProjectRoles(List<ScrumRole> projectRoles, int userId);

        List<Group> GetUserGroups(int userId);

        int GetUsersCount();

        List<User> GetAllUsersForMaintenance(int currPage, int currPageSize);

        List<User> GetAllUsersFilteredForMaintenance(int currPage, int currPageSize, string fieldFilter);
    }
}
