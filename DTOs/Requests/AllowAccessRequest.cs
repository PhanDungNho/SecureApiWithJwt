namespace SecureApiWithJwt.DTOs.Requests
{
    public class AllowAccessRequest
    {
        public int? RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;
        public string? AccessProperties { get; set; } = string.Empty; 
    }
}
