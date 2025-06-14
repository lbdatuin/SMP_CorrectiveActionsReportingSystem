using CARWeb.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CARWeb.Shared.DTOs.AuthDTO
{
    public class RegisterDTO {

        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPass { get; set; } = string.Empty;
        public UserRoles Role { get; set; }

        //USER DETAILS

        [Required]
        public string UserFirstName { get; set; } = string.Empty;
        [Required]
        public string UserLastName { get; set; } = string.Empty;

        public string UserAddress { get; set; } = string.Empty;

        public string UserCity { get; set; } = string.Empty;

        public string UserRegion { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        [RegularExpression("^(09|\\+639)\\d{9}$", ErrorMessage = "The number should start with +639 or 09 followed by 9 numbers.")]
        public string UserPhone { get; set; } = string.Empty;

    }
}