using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CARWeb.Shared.Models.CAREntry;

namespace CARWeb.Shared.Models.CARLabel
{
    public class CARType
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;

        [JsonIgnore]
        public List<NonConformity>? NonConformities { get; set; }

        [JsonIgnore]
        public List<CARHeader>? CARHeaders { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
