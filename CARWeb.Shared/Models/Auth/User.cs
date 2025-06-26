using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.DepartmentSection;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CARWeb.Shared.Models.Auth
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string Designation { get; set; } = string.Empty;
        public UserRoles Role { get; set; }
        public List<AccessRole> AccessRoles { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public string VerificationToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime RefreshTokenCreatedAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }

        public UserDetails UserDetails { get; set; }

        [JsonIgnore]
        public List<AuditTrail>? AuditTrails { get; set; }
    }
}
