using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Layer.Security
{
    public static class clsToken
    {
        public static JwtOptions? jwtOptions { get; set; }
        public static string CreateToken(int UserID)
        {

            //taking a username and password and validating them against the db and returning the userid.


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions?.Issuer,
                Audience = jwtOptions?.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.SingingKey)),
                SecurityAlgorithms.HmacSha256),


                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, UserID.ToString()),
                    
                }),



                Expires = DateTime.UtcNow.AddMonths(1), // Set expiration to 1 month
            
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }

}
