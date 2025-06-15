using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.DepartmentSectionDTO
{
    public class CreateDepartmentDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<CreateSectionDTO> Sections { get; set; } = new List<CreateSectionDTO>();
    }

    public class CreateSectionDTO
    {
        public Guid MainId { get; set; } = Guid.NewGuid();  
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
