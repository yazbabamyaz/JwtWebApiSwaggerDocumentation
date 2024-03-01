using LessonApi.Models.Jwt;

namespace LessonApi.Infrastructure.Repositories;



public interface IAuthRepository
{
    public Task<LoginResponse> LoginUserAsync(LoginRequest request);
}
