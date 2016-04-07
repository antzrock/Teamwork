using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class User : IEntityBase, IAuditable
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }
        public int ID { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }

        public String Nickname { get; set; }

        public String Fullname { get; set; }

        public String Title { get; set; }

        public String AvatarPicPath { get; set; }

        public String ProfilePicPath { get; set; }

        public String ProfileQuote { get; set; }

        public String HashedPassword { get; set; }
        public String Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
     
        public virtual ICollection<Permission> ApplicationPermissions { get; set; }

        public virtual ICollection<GroupMember> GroupMembership { get; set; }

        public virtual ICollection<TeamMember> TeamMembership { get; set; }

        public virtual ICollection<ScrumTeamMember> ProjectScrumTeamMembership { get; set; }
        
        public virtual ICollection<WeekReportHeader> WeeklyReports { get; set; }

        // STATUS
        public int UserStatusID { get; set; }

        public UserStatus UserStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
