using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Enums;

namespace CARWeb.Shared.DTOs.UserManagementDTO
{
    public class GetUsersDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRoles Role { get; set; }
        public bool IsActive { get; set; }
        public List<GetAccessRolesDTO> AccessRoles { get; set; } 
    }

    public class GetAccessRolesDTO
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
