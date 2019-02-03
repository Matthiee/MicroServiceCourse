using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Common.Exceptions
{

    [Serializable]
    public class MicroServiceException : Exception
    {
        public string Code { get; private set; }

        public MicroServiceException() { }

        public MicroServiceException(string code)
        {
            Code = code;
        }

        public MicroServiceException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        protected MicroServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
