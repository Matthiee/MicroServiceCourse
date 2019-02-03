using System;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MicroServices.Common.Auth
{
    public interface IJwtHandler
    {
        AuthToken Create(Guid userId);
    }
}
