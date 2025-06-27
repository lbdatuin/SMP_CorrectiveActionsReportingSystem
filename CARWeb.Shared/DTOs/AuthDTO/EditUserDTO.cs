using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Enums;

namespace CARWeb.Shared.DTOs.AuthDTO
{
    public class EditUserDTO
    {

        [Required]
        public string Username { get; set; } = string.Empty;

        public List<CreateAccessRole> AccessRoles { get; set; } = new List<CreateAccessRole>();

        //USER DETAILS

        [Required]
        public string UserFirstName { get; set; } = string.Empty;
        [Required]
        public string UserLastName { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }

    }
}
