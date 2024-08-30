using Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Config
{
    public class JwtConfig
    {
        private static IConfiguration _configuration;

        public JwtConfig(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        
        public static string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name!)
            };

            //从appsetings.json读取secretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            //加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            //生成credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     //Issuer
                _configuration["Jwt:Audience"],   //Audience
                 claims,                          //Claims,
                DateTime.Now,                     //notBefore
                DateTime.Now.AddDays(1),          //expires
                signingCredentials                //Credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public static IEnumerable<Claim> ReadJwtToken(string token)
        {
            var jwtSecurityHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = jwtSecurityHandler.ReadJwtToken(token);

            return jwtSecurityToken.Claims;
        }
    }
}
