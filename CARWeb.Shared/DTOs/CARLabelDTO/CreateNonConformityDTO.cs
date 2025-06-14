using CARWeb.Shared.Models.CARLabel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.CARLabelDTO
{
    public class CreateNonConformityDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public string NoSeries { get; set; } = string.Empty;
        public int CARTypeId { get; set; }
    }
}
