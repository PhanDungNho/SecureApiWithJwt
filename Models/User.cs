using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SecureApiWithJwt.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
