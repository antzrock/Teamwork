using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEEKLY.Web.Models;

namespace WEEKLY.Web.Infrastructure.Validators
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(c => c.UserID).GreaterThan(0).WithMessage("User ID must be greater than zero.");
            RuleFor(c => c.OldPassword).NotEmpty().WithMessage("Old password is required");
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage("New password is required");
        }
    }


    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");
        }
    }

    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(u => u.Status).NotEmpty()
                .WithMessage("Status is required");
        }
    }

    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");

            RuleFor(r => r.Group).NotEmpty()
                .WithMessage("Group is required");

            RuleFor(r => r.Team).NotEmpty()
                .WithMessage("Team is required");
        }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");
        }
    }
}