using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MicroServices.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private readonly JwtOptions options;
        private readonly SecurityKey issuerSigninKey;
        private readonly SigningCredentials signingCredentials;
        private readonly JwtHeader jwtHeader;
        private readonly TokenValidationParameters tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey));
            signingCredentials = new SigningCredentials(issuerSigninKey, SecurityAlgorithms.HmacSha256);
            jwtHeader = new JwtHeader(signingCredentials);

            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = this.options.Issuer,
                IssuerSigningKey = issuerSigninKey,

            };
        }

        public AuthToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);

            var payload = new JwtPayload
            {
                { JwtRegisteredClaimNames.Sub, userId },
                { JwtRegisteredClaimNames.Iss, options.Issuer },
                { JwtRegisteredClaimNames.Iat, now },
                { JwtRegisteredClaimNames.Exp, exp },
                { JwtRegisteredClaimNames.UniqueName, userId }
            };

            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AuthToken { Expires = exp, Token = token };
        }
    }
}
