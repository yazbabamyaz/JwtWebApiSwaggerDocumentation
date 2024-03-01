using LessonApi.Models.Jwt;

namespace LessonApi.Infrastructure.Repositories.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IToken _token;

        public AuthRepository(IToken token)
        {
            _token = token;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            LoginResponse response = new();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.UserName == "mustafa" && request.Password == "123")
            {
                //giriş başarılı ise token oluşturulur.
                //CreateToken metoduna Username ve Role bilgisi gönderdim ki Oluşturacağı Token'da claim olarak eklesin.
                var tokenİnfo = await _token.CreateToken(new CreateTokenRequest { Username = request.UserName, Role="User" });

                response.Result = true;
                response.Token = tokenİnfo.Token;
                response.TokenExpireDate = tokenİnfo.TokenExpireDate;
            }

            return response;
        }


       
    }
}
