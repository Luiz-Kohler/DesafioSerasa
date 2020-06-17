using Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validators
{
    public class CompanyValidator : AbstractValidator<Company>
    {

        public HashSet<string> CustomValidate(Company company)
        {
            return Validate(company).Errors.Select(x => x.ErrorMessage).ToHashSet();
        }

        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotEmpty().WithMessage("The company name must be informed")
                   .NotNull().WithMessage("The company name must be informed")
                   .Length(2, 50).WithMessage("The company name length must have between 2 and 50 characters");

            RuleFor(c => c.Reliability)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .InclusiveBetween(0, 100).WithMessage("The reliability of company need to be between 0 and 100");
        }
    }
}
