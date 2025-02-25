namespace SecureApiWithJwt.DTOs.Responses
{
    public class AllowAccessResponse
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;
        public string? AccessProperties { get; set; } = string.Empty;
    }
}
