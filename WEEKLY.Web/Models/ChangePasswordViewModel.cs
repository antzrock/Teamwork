using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEEKLY.Web.Infrastructure.Validators;

namespace WEEKLY.Web.Models
{
    public class ChangePasswordViewModel
    {
        public int UserID { get; set; }

        public String OldPassword { get; set; }


        public String NewPassword { get; set; }

        public String ConfirmNewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ChangePasswordViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}