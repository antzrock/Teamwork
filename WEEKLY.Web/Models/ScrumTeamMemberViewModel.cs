using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ScrumTeamMemberViewModel
    {
        public int ScrumTeamMemberID { get; set; }

        //USER
        public int UserID { get; set; }

        public string Fullname { get; set; }

        //ROLE
        public int[] Roles { get; set; }
        
        //PROJECT
        public int ProjectID { get; set; }

        //STATUS
        public string Status { get; set; }

        public string AvatarPicUrl { get; set; }

        public string JobTitle { get; set; }

        public string MainRole { get; set; }
    }
}