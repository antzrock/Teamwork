using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface ITeamService
    {
        Team GetTeamById(int teamId);

        List<TeamStatus> GetAllTeamStates();

        List<TeamRole> GetAllTeamRoles();

        Team CreateTeam(string teamName, string teamCode, string teamDescription, int groupID, int statusID, int userID, List<TeamMember> teamMembers);

        Team UpdateTeam(int teamID, string teamName, string teamCode, string teamDescription, int groupID, int statusID, int userID, List<TeamMember> teamMembers);

        bool IsTeamMemberDefaultInOtherGroups(int teamId, int userId);
    }
}
