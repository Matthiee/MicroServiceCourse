using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

        public CreateActivityRejected(string reason, string code)
        {
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }
    }
}
