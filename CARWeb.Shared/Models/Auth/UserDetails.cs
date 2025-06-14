using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CARWeb.Shared.Models.Auth
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Avatar { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
