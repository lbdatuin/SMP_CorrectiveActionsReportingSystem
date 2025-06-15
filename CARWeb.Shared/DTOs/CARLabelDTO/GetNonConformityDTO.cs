using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.CARLabelDTO
{
    public class GetNonConformityDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public string NoSeries { get; set; } = string.Empty;
        public int CARTypeId { get; set; }
        public string CARTypeCode { get; set; }
    }
}
