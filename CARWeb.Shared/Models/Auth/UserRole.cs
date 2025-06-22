using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.Auth
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }

        public List<AccessRole> AccessRoles { get; set; }
    }
}
