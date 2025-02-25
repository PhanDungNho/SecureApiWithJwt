using System.ComponentModel.DataAnnotations;

namespace SecureApiWithJwt.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = true;

    }
}
