using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CARWeb.Shared.Models.Auth;

namespace CARWeb.Shared.Models.DepartmentSection
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<DSection>? Sections { get; set; }
        public List<User>? Users { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
