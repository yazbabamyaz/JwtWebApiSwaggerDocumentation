using LessonApi.Infrastructure.Repositories;
using LessonApi.Models.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost]
        [AllowAnonymous]//herhangi bir yetki bilgisi istemez.
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            //kullanıcı doğru giriş yaparsa token bilgisi ve geçerlilik süresini döndüreceği bir nesne gelecek:LoginResponse
           var result=await _authRepo.LoginUserAsync(request);
            return Ok(result);//200 status code
        }
    }
}
