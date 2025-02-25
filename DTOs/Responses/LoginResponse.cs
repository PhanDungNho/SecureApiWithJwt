namespace SecureApiWithJwt.DTOs.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
    }
}
