using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class GroupMemberViewModel
    {
        public int GroupMemberID { get; set; }

        public GroupViewModel Group { get; set; }

        public int UserID { get; set; }

        public string Fullname { get; set; }

        public string Title { get; set; }

        public string AvatarPicPath { get; set; }

        public string Status { get; set; }

        public string MainRole { get; set; }

        public int[] Roles { get; set; }

        public bool IsUserDefault { get; set; }
    }
}