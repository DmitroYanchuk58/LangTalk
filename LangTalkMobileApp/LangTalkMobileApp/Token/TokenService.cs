using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.Token
{
    public class TokenService
    {
        private string _token;

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                IdUser = GetUserIdFromToken(value).Value;
            }
        }

        public Guid IdUser { get; private set; }

        private static Guid? GetUserIdFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
            var id = Guid.Parse(idClaim?.Value ?? throw new InvalidOperationException("Token does not contain a valid 'sub' claim."));
            return id;
        }
    }

}
