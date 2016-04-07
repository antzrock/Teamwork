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
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityBaseRepository<Group> _groupRepository;
        private readonly IEntityBaseRepository<GroupStatus> _groupStatusRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<GroupMember> _groupMemberRepository;
        private readonly IMembershipService _membershipService;

        public GroupService(IEntityBaseRepository<Group> groupRepository,
                            IEntityBaseRepository<GroupStatus> groupStatusRepository,
                            IEntityBaseRepository<User> userRepository,
                            IEntityBaseRepository<GroupMember> groupMemberRepository,
                            IMembershipService membershipService,
                            IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _groupStatusRepository = groupStatusRepository;
            _userRepository = userRepository;
            _groupMemberRepository = groupMemberRepository;
            _membershipService = membershipService;
            _unitOfWork = unitOfWork;
        }

        public bool IsGroupMemberDefaultInOtherGroups(int groupId, int userId)
        {
            bool isDefault = false;

            User existingUser = _membershipService.GetUser(userId);

            if (existingUser == null)
                throw new Exception("User doesn't exist in database.");

            List<GroupMember> otherGroupMembership = _groupMemberRepository.GetAll().Where(gm => gm.GroupID != groupId && gm.UserID == userId).ToList();

            foreach(GroupMember gm in otherGroupMembership)
            {
                if (gm.IsUserDefault)
                {
                    isDefault = true;
                    break;
                }
            }

            return isDefault;
        }

        public Group GetGroupById(int Id)
        {
            Group existingGroup = _groupRepository.AllIncluding(g => g.Members, g => g.Projects, g => g.Teams, g => g.Status).Where(g => g.ID == Id).FirstOrDefault();

            if (existingGroup == null)
                throw new Exception("Group doesn't exist in database.");

            return existingGroup;
        }

        public Group UpdateGroup(int groupId, string groupName, string groupCode, string groupDescription, int stateId, int userId, List<GroupMember> teamMembers)
        {
            Group existingGroup = GetGroupById(groupId);

            if (existingGroup == null)
                throw new Exception("Group doesn't exist in database.");

            // Check if state exists...
            GroupStatus existingGroupState = _groupStatusRepository.FindBy(gs => gs.ID == stateId).FirstOrDefault();

            if (existingGroupState == null)
                throw new Exception("Group state doesn't exist.");

            // Check if user exists...
            User existingUser = _userRepository.FindBy(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User doesn't exist.");

            existingGroup.Name = groupName;
            existingGroup.Code = groupCode;
            existingGroup.Description = groupDescription;
            existingGroup.GroupStatusID = existingGroupState.ID;
            existingGroup.Status = existingGroupState;
            existingGroup.UpdatedBy = existingUser.ID;
            existingGroup.UpdatedDate = DateTime.Now;

            // UPDATE MEMBERS...
            // Delete missing members...
            List<GroupMember> deletedMembers = existingGroup.Members.Where(gm => !teamMembers.Any(gm2 => gm2.ID == gm.ID)).ToList();

            foreach(GroupMember deleteThis in deletedMembers)
            {
                _groupMemberRepository.Delete(deleteThis);
            }

            // Edit existing members
            foreach(GroupMember editThis in existingGroup.Members)
            {
                GroupMember editFrom = teamMembers.FirstOrDefault(tm => tm.ID == editThis.ID);

                if (editFrom != null)
                {
                    // Edit Roles...
                    // Delete all roles...
                    List<GroupRole> deletedRoles = editThis.Roles.ToList();

                    foreach(GroupRole deleteThis in deletedRoles)
                    {
                        editThis.Roles.Remove(deleteThis);
                    }

                    // Add new roles...
                    List<GroupRole> newRoles = editFrom.Roles.ToList();

                    foreach(GroupRole addThis in newRoles)
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

                    _groupMemberRepository.Edit(editThis);
                }
            }

            // Add new members...
            List<GroupMember> newMembers = teamMembers.Where(tm => tm.ID == 0).ToList();

            foreach(GroupMember addThis in newMembers)
            {
                addThis.CreatedBy = existingUser.ID;
                addThis.CreatedDate = DateTime.Now;

                existingGroup.Members.Add(addThis);
            }

            _groupRepository.Edit(existingGroup);
            _unitOfWork.Commit();

            return existingGroup;
        }

        public Group CreateGroup(string groupName, string groupCode, string groupDescription, int stateId, int userId, List<GroupMember> teamMembers)
        {
            Group existingGroup = _groupRepository.FindBy(g => g.Name == groupName).FirstOrDefault();

            if (existingGroup != null)
                throw new Exception("Group name is already in use!");

            // Check if state exists...
            GroupStatus existingGroupState = _groupStatusRepository.FindBy(gs => gs.ID == stateId).FirstOrDefault();

            if (existingGroupState == null)
                throw new Exception("Group state doesn't exist.");

            // Check if user exists...
            User existingUser = _userRepository.FindBy(u => u.ID == userId).FirstOrDefault();

            if (existingUser == null)
                throw new Exception("User doesn't exist.");

            Group newGroup = new Group();

            newGroup.Name = groupName;
            newGroup.Code = groupCode;
            newGroup.Description = groupDescription;
            newGroup.GroupStatusID = existingGroupState.ID;
            newGroup.Status = existingGroupState;
            newGroup.CreatedBy = existingUser.ID;
            newGroup.CreatedDate = DateTime.Now;

            _groupRepository.Add(newGroup);
            _unitOfWork.Commit();

            if (newGroup != null)
            {
                newGroup.Members = new List<GroupMember>();

                foreach(GroupMember gm in teamMembers)
                {
                    gm.CreatedBy = existingUser.ID;
                    gm.CreatedDate = DateTime.Now;
                    newGroup.Members.Add(gm);
                }

                _groupRepository.Edit(newGroup);
                _unitOfWork.Commit();
            }

            return newGroup;
        }
    }
}
