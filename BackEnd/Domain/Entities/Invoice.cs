using Domain.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; }

        public Invoice()
        {

        }

        public Invoice(int companyId)
        {
            CompanyId = companyId;
        }

        public override HashSet<string> GetErrors()
        {
            return new InvoiceValidator().CustomValidate(this);
        }

        public override bool IsValid()
        {
            return !GetErrors().Any();
        }
    }
}
