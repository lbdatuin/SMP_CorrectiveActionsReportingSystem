using System.ComponentModel.DataAnnotations;

namespace CARWeb.Shared.DTOs.AuthDTO
{
    public class LoginDTO {

        //[Required, EmailAddress]
        //public string Email { get; set; } = String.Empty;

        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
