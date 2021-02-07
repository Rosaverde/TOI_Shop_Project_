using FluentValidation;
using Shop.Application.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Validation
{
    public class AddCustomerInformationRequestValidation : AbstractValidator<AddCustomerInformation.Request>
    {

        public AddCustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(7);
            RuleFor(x => x.Address1).NotEmpty();
            RuleFor(x => x.Address2).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.PostCode).NotEmpty();
        }
    }
}
