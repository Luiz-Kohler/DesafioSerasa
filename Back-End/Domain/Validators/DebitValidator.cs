using Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validators
{
    public class DebitValidator : AbstractValidator<Debit>
    {
        public HashSet<string> CustomValidate(Debit debit)
        {
            return Validate(debit).Errors.Select(x => x.ErrorMessage).ToHashSet();
        }

        public DebitValidator()
        {
            RuleFor(d => d.CompanyId)
                   .GreaterThan(0).WithMessage("The Id of company must be greater than 0");
        }
    }
}
