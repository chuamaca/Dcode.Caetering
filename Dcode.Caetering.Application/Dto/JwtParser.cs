using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dcode.Caetering.Application.Dto
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            if (string.IsNullOrEmpty(jwt) || !jwt.Contains("."))
            {
                Console.WriteLine("Token inválido, no se puede leer.");
                return new List<Claim>();
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }
    }
}
