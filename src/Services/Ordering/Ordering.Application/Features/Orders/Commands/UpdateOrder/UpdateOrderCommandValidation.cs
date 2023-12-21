using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Please enter order id");

            RuleFor(c => c.UserName).NotEmpty().WithMessage("Please enter User name")
                .NotNull().WithMessage("User Name not null")
                .EmailAddress().WithMessage("Username should be valid email");

            RuleFor(c => c.EmailAddress).EmailAddress().WithMessage("Email Address should be valid Email");

            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Please enter your first name")
                .MaximumLength(100).WithMessage("First Name max lenght should be 100 characters");

            RuleFor(c => c.TotalPrice).GreaterThan(0).WithMessage("Total Price should be greater than zero");
        }
    }
}
