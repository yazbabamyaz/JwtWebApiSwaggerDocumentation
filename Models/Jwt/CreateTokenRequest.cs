namespace LessonApi.Models.Jwt
{
    
    public class CreateTokenRequest
    {
        
        public string? Username { get; set; }
        public string? Role { get; set; }
    }
}
