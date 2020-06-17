using System;
using System.Collections.Generic;

namespace Utils.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public HashSet<string> Errors { get; private set; }

        public BadRequestException(HashSet<string> errors)
        {
            this.Errors = errors;
        }

        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception inner) : base(message, inner) { }
        protected BadRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
