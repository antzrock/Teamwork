using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WEEKLY.Web.Infrastructure.Validators;

namespace WEEKLY.Web.Models
{
    public class RegistrationViewModel : IValidatableObject
    {
        public String Username { get; set; }
        public String Password { get; set; }
        
        public String Email { get; set; }

        public GroupViewModel Group { get; set; }

        public TeamViewModel Team { get; set; }

        public String Nickname { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RegistrationViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}