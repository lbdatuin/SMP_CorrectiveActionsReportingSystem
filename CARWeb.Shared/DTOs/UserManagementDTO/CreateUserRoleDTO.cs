using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.UserManagementDTO
{
    public class CreateUserRoleDTO
    {
        public string Role { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
    }
}
