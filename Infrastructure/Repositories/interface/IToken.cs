using LessonApi.Models.Jwt;

namespace LessonApi.Infrastructure.Repositories;

public interface IToken
{
    public Task<CreateTokenResponse> CreateToken(CreateTokenRequest request);
}
