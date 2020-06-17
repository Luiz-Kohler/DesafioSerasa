using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public abstract HashSet<string> GetErrors();
        public abstract bool IsValid();
    }
}
