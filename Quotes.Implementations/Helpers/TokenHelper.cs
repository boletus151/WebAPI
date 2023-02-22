using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Quotes.Implementations.Helpers
{
    public static class TokenHelper
    {
        public static string GetTokenClaim(string token, string claim = "oid")
        {
            if (token is null)
            {
                return null;
            }
            var jwtToken = new JwtSecurityToken(jwtEncodedString: token);

            var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(claim))?.Value;

            return claimValue;
        }

        public static string GetTokenExpTime(string token)
        {
            if (token is null)
            {
                return null;
            }
            var jwtToken = new JwtSecurityToken(jwtEncodedString: token);

            var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals("exp"))?.Value;
            return claimValue;
        }

        public static long GetTokenExpirationTime(string token)
        {
            var jwtToken = new JwtSecurityToken(jwtEncodedString: token);
            var tokenExp = jwtToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public static string GetTokenExpirationTimeString(string token)
        {
            var expTime = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(expTime).UtcDateTime;

            var str = $"{tokenDate.ToShortDateString()} {tokenDate.ToLongTimeString()} UTC";

            return str;
        }

        public static bool TokenIsValid(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }
    }

}
