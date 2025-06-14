using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CARLabel
{
    public class NonConformity
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public string NoSeries { get; set; } = string.Empty;
        public CARType CARType { get; set; }
        public int CARTypeId { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
