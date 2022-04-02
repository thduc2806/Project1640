using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Users
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password required")
                .MinimumLength(3).WithMessage("Password less than 6 characters");

        }
    }
}
