using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.Auth
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Activity { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
