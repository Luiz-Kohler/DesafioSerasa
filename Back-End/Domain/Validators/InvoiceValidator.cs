using Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validators
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public HashSet<string> CustomValidate(Invoice invoice)
        {
            return Validate(invoice).Errors.Select(x => x.ErrorMessage).ToHashSet();
        }

        public InvoiceValidator()
        {
            RuleFor(i => i.CompanyId)
                   .GreaterThan(0).WithMessage("The company Id is invalid");
        }
    }
}
