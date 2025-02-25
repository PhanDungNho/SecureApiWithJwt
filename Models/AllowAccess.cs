using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SecureApiWithJwt.Models
{
    public class AllowAccess
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? TableName { get; set; } = string.Empty;
        public string? AccessProperties { get; set; } = string.Empty;

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
