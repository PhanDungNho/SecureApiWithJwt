namespace SecureApiWithJwt.DTOs.Requests
{
    public class RoleRequest
    {
        public string RoleName { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = true;
    }
}
