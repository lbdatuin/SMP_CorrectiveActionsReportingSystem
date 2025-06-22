using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.Auth
{
    public class AccessRole
    {
        [Key]
        public int Id { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
