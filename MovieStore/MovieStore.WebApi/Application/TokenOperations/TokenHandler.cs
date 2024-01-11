using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieStore.WebApi.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.TokenOperations.Commands.CommandHandler
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Token CreateAccessToken(Customer customer)
        {
            Token token = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            token.ExpireDate = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.ExpireDate,
                notBefore: DateTime.Now,
                claims:SetClaims(customer),
                signingCredentials: signingCredentials
                );
           
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            
            return token;
        }

        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
        private ICollection<Claim> SetClaims(Customer customer)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim(ClaimTypes.Surname, customer.Surname),
                new Claim(ClaimTypes.Email, customer.Email)
            };
            return claims;
        }
    }
}
