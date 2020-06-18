using Domain.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Debit : BaseEntity
    {
        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; }

        public Debit()
        {

        }

        public Debit(int companyId)
        {
            CompanyId = companyId;
        }

        public override HashSet<string> GetErrors()
        {
            return new DebitValidator().CustomValidate(this);
        }

        public override bool IsValid()
        {
            return !GetErrors().Any();
        }
    }
}
