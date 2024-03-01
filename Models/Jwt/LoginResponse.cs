namespace LessonApi.Models.Jwt
{    
    
    public class LoginResponse
    {       
        public bool Result { get; set; }      
        public string Token { get; set; }       
        public DateTime TokenExpireDate { get; set; }
    }
}
