using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.DepartmentSectionDTO
{
    public class GetDepartmentDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<GetSectionDTO> Sections { get; set; } = new List<GetSectionDTO>();
    }
    public class GetSectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
