using System.ComponentModel.DataAnnotations;


namespace CARWeb.Shared.DTOs.AuthDTO
{
    public class UserDetailsDTO
    {
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
        public string UserPhone { get; set; } = string.Empty;
    }
}
