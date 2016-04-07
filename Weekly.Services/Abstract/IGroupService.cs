using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IGroupService
    {
        Group CreateGroup(string groupName, string groupCode, string groupDescription, int stateId, int userId, List<GroupMember> teamMembers);

        Group UpdateGroup(int groupId, string groupName, string groupCode, string groupDescription, int stateId, int userId, List<GroupMember> teamMembers);

        Group GetGroupById(int Id);

        bool IsGroupMemberDefaultInOtherGroups(int groupId, int userId);
    }
}
