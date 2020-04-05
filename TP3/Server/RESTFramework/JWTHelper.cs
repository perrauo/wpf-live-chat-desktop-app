using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.RESTFramework
{
    public class JWTHelper
    {
        private static string privateKey = "MIIBOAIBAAJAdiNTkw2Qfm/A/UePE5XxmQMPZwR8KrOd8DDjcyfskmkXFx10wDdVk9m6sH6Eshm7rTZ1qVkVnPOwv9oap3QGWwIDAQABAkB0g4rdsbccvCNeqcDW1D+f";

        public static string Generate(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload
            {
                { "username ", username }
            };

            var signedToken = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(signedToken); ;
        }

        public static bool Verify(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey)),
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false
            };

            // Check the token's signature.
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                // Token malformed.
                return false;
            }
            return true;
        }

        public static string ExtractUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var payload = tokenHandler.ReadJwtToken(token).Payload;
            return (string) payload.First().Value; // the token only contain one key-value pair: username
        }
    }
}
