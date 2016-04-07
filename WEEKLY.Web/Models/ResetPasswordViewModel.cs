using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEEKLY.Web.Infrastructure.Validators;

namespace WEEKLY.Web.Models
{
    public class ResetPasswordViewModel
    {
        public String Email { get; set; }

        public String HomeUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ResetPasswordViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}