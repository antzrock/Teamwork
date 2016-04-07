using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEEKLY.Web.Infrastructure.Validators;

namespace WEEKLY.Web.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        
        public String Email { get; set; }

        public String Fullname { get; set; }

        public String Nickname { get; set; }

        public String Title { get; set; }

        public String AvatarPicPath { get; set; }
        
        public String ProfilePicPath { get; set; }

        public String ProfileQuote { get; set; }

        public UserStatusViewModel Status { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}