namespace LessonApi.Models.Jwt
{
   
    public class CreateTokenResponse
    {
       
        public string? Token { get; set; }

       
        public DateTime TokenExpireDate { get; set; }
    }
}
