using FluentValidation;
using Shop.UI.Pages.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Validation
{
    public class CreateUserRequestValidation : AbstractValidator<ConfigureUsers.CreateUserViewModel>
    {
        public CreateUserRequestValidation()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.Password);
        }
    }
}
