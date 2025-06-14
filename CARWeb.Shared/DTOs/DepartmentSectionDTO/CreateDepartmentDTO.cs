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

        public List<CreateSectionDTO> Section { get; set; } = new List<CreateSectionDTO>();
    }

    public class CreateSectionDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}
