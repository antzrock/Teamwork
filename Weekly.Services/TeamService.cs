using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityBaseRepository<Team> _teamRepository;
        private readonly IEntityBaseRepository<TeamStatus> _teamStatusRepository;
        private readonly IEntityBaseRepository<TeamMember> _teamMemberRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<TeamRole> _teamRoleRepository;
        private readonly IEntityBaseRepository<Group> _groupRepository;
        private readonly IMembershipService _membershipService;

        public TeamService( IEntityBaseRepository<Team> teamRepository,
                            IEntityBaseRepository<TeamStatus> teamStatusRepository,
                            IEntityBaseRepository<TeamMember> teamMemberRepository,
                            IEntityBaseRepository<TeamRole> teamRoleRepository,
                            IEntityBaseRepository<Group> groupRepository,
                            IEntityBaseRepository<User> userRepository,
                            IMembershipService membershipService,
                            IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _teamStatusRepository = teamStatusRepository;
            _teamMemberRepository = teamMemberRepository;
            _teamRoleRepository = teamRoleRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _membershipService = membershipService;
            _unitOfWork = unitOfWork;
        }

        public bool IsTeamMemberDefaultInOtherGroups(int teamId, int userId)
        {
            bool isDefault = false;

            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new Exception("User doesn't exist in database.");

            List<TeamMember> otherTeamMembership = _teamMemberRepository.GetAll().Where(tm => tm.TeamID != teamId && tm.UserID == userId).ToList();

            foreach(TeamMember tm in otherTeamMembership)
            {
                if (tm.IsUserDefault)
                {
                    isDefault = true;
                    break;
                }
            }

            return isDefault;
        }

        public Team UpdateTeam(int teamID, string teamName, string teamCode, string teamDescription, int groupID, int statusID, int userID, List<TeamMember> teamMembers)
        {
            //Check team exists...
            Team existingTeam = _teamRepository.AllIncluding(t => t.Group, t => t.Status, t => t.Members).Where(t => t.ID == teamID).FirstOrDefault();

            if (existingTeam == null)
                throw new Exception("Team to update not found in database");

            //Check group exists...
            Group existingGroup = _groupRepository.FindBy(g => g.ID == groupID).FirstOrDefault();

            if (existingGroup == null)
                throw new Exception("Group not found in database");

            //Check team status exists...
            TeamStatus existingTeamStatus = _teamStatusRepository.FindBy(ts => ts.ID == statusID).FirstOrDefault();

            if (existingTeamStatus == null)
                throw new Exception("Team status not found in database.");

            //User exists...
            User existingUser = _userRepository.FindBy(u => u.ID == userID).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User not found in database.");


            existingTeam.Name = teamName;
            existingTeam.Code = teamCode;
            existingTeam.Description = teamDescription;
            existingTeam.GroupID = existingGroup.ID;
            existingTeam.Group = existingGroup;
            existingTeam.TeamStatusID = existingTeamStatus.ID;
            existingTeam.Status = existingTeamStatus;
            existingTeam.UpdatedBy = existingUser.ID;
            existingTeam.UpdatedDate = DateTime.Now;

            // UPDATE MEMBERS...
            // Delete missing members...
            List<TeamMember> deletedMembers = existingTeam.Members.Where(tm => !teamMembers.Any(tm2 => tm2.ID == tm.ID)).ToList();

            foreach(TeamMember deleteThis in deletedMembers)
            {
                _teamMemberRepository.Delete(deleteThis);
            }

            // Edit existing members
            foreach(TeamMember editThis in existingTeam.Members)
            {
                TeamMember editFrom = teamMembers.FirstOrDefault(tm => tm.ID == editThis.ID);

                if (editFrom != null)
                {
                    // Edit Roles...
                    // Delete all roles...
                    List<TeamRole> deletedRoles = editThis.Roles.ToList();

                    foreach(TeamRole deleteThis in deletedRoles)
                    {
                        editThis.Roles.Remove(deleteThis);
                    }

                    List<TeamRole> newRoles = editFrom.Roles.ToList();

                    foreach(TeamRole addThis in newRoles)
                    {
                        editThis.Roles.Add(addThis);
                    }

                    editThis.UserID = editFrom.UserID;
                    editThis.User = editFrom.User;
                    editThis.UpdatedBy = existingUser.ID;
                    editThis.UpdatedDate = DateTime.Now;
                    editThis.MemberStatusID = editFrom.MemberStatusID;
                    editThis.MemberStatus = editFrom.MemberStatus;
                    editThis.IsUserDefault = editFrom.IsUserDefault;

                    _teamMemberRepository.Edit(editThis);
                }
            }

            // Add new members...
            List<TeamMember> newMembers = teamMembers.Where(tm => tm.ID == 0).ToList();

            foreach(TeamMember addThis in newMembers)
            {
                addThis.CreatedBy = existingUser.ID;
                addThis.CreatedDate = DateTime.Now;

                existingTeam.Members.Add(addThis);
            }

            _teamRepository.Edit(existingTeam);
            _unitOfWork.Commit();

            return existingTeam;
        }

        public Team CreateTeam(string teamName, string teamCode, string teamDescription, int groupID, int statusID, int userID, List<TeamMember> teamMembers)
        {
            //Check group exists...
            Group existingGroup = _groupRepository.FindBy(g => g.ID == groupID).FirstOrDefault();

            if (existingGroup == null)
                throw new Exception("Group not found in database");

            //Check team status exists...
            TeamStatus existingTeamStatus = _teamStatusRepository.FindBy(ts => ts.ID == statusID).FirstOrDefault();

            if (existingTeamStatus == null)
                throw new Exception("Team status not found in database.");

            //User exists...
            User existingUser = _userRepository.FindBy(u => u.ID == userID).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User not found in database.");

            Team newTeam = new Team();
            newTeam.Name = teamName;
            newTeam.Code = teamCode;
            newTeam.Description = teamDescription;
            newTeam.Status = existingTeamStatus;
            newTeam.TeamStatusID = existingTeamStatus.ID;
            newTeam.GroupID = existingGroup.ID;
            newTeam.Group = existingGroup;
            newTeam.Members = new List<TeamMember>();

            //Members...
            foreach(TeamMember tm in teamMembers)
            {
                tm.CreatedBy = existingUser.ID;
                tm.CreatedDate = DateTime.Now;

                newTeam.Members.Add(tm);
            }

            newTeam.CreatedBy = existingUser.ID;
            newTeam.CreatedDate = DateTime.Now;

            _teamRepository.Add(newTeam);
            _unitOfWork.Commit();

            return newTeam;
        }

        public List<TeamRole> GetAllTeamRoles()
        {
            List<TeamRole> allRoles = _teamRoleRepository.GetAll().ToList();

            if (allRoles == null)
                throw new Exception("No Team member role registered in database");

            return allRoles;
        }

        public Team GetTeamById(int teamId)
        {
            Team existingTeam = _teamRepository.AllIncluding(t => t.Members, t => t.Projects, t => t.Status, t => t.Group).Where(t => t.ID == teamId).FirstOrDefault();

            if (existingTeam == null)
                throw new Exception("Team doesn't exist in database.");

            return existingTeam;
        }

        public List<TeamStatus> GetAllTeamStates()
        {
            List<TeamStatus> allStates = _teamStatusRepository.GetAll().ToList();

            if (allStates == null)
                throw new Exception("No Team state registered in database");

            return allStates;
        }
    }
}
