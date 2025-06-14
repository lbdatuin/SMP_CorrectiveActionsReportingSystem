using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.AuthDTO
{
    public class EditProfileDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string UserFirstName { get; set; } = string.Empty;
        [Required]
        public string UserLastName { get; set; } = string.Empty;
        [Required]
        public string UserAddress { get; set; } = string.Empty;
        [Required]
        public string UserCity { get; set; } = string.Empty;
        [Required]
        public string UserRegion { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(09|\\+639)\\d{9}$", ErrorMessage = "The number should start with +639 or 09 followed by 9 numbers.")]
        public string UserPhone { get; set; } = string.Empty;

        public string? Avatar { get; set; }

    }

}
