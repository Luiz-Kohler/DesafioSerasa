using System.Collections.Generic;
using System.Linq;
using Utils.Exceptions;

namespace Application.Services
{
    public abstract class BaseService
    {
        public HashSet<string> Errors { get; private set; } = new HashSet<string>();

        public void HandlerErrors()
        {
            if (Errors.Any())
                throw new BadRequestException(Errors);
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public void AddErrors(HashSet<string> errors)
        {
            Errors = errors;
        }
    }
}
