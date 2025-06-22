using CARWeb.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.CAREntryDTO
{
    public class GetCARListDTO
    {
        public int Id { get; set; }
        public string SysRefNo { get; set; } = string.Empty;
        public string CARNo { get; set; } = string.Empty;
        public string UnmentDept { get; set; } = string.Empty;
        public DateTime IssuanceDate { get; set; }
        public string DetailsOfConformity { get; set; } = string.Empty;
        public string CARDetails { get; set; } = string.Empty;
        public CARStatus Status { get; set; }
    }
}
