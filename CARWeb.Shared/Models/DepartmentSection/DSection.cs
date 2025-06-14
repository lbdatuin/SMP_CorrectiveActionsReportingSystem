using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.DepartmentSection
{
    public class DSection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
