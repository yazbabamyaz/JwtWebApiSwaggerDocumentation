using LessonApi.Models.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LessonApi.Infrastructure.Repositories.Concrete
{
    /// <summary>
    /// Token üretmekle görevli bir metot mevcut
    /// </summary>
    public class Token : IToken
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<CreateTokenResponse> CreateToken(CreateTokenRequest request)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _configuration["AppSettings:ValidIssuer"],
                    audience: _configuration["AppSettings:ValidAudience"],
                    claims: new List<Claim> {
                    new Claim("userName", request.Username),
                    //new Claim(ClaimTypes.Role,"Admin"),                    
                    new Claim(ClaimTypes.Role, request.Role),
                   
                    },
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(20)),//dateTimeNow'a 20dk ekle
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            //return Task.FromResult(new CreateTokenResponse
            //{
            //    Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            //    TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(20))//dateTimeNow'a 20dk ekle
            //});
            //ya da
            CreateTokenResponse tokenRes= new CreateTokenResponse();
            tokenRes.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
            tokenRes.TokenExpireDate= dateTimeNow.Add(TimeSpan.FromMinutes(20));

            return Task.FromResult(tokenRes);            

            //async metotlar içinde senkron değer döndürmek için kullanırız.Geriye benden Task<t> gibi bir değer döndürmemi bekler. o yüzden Task.FromResult kullanıldı.
            //Kısaca CreateTokenResponse'ı Task<CreateTokenResponse> 'a dönüştürdüm.
        }
    }
}
