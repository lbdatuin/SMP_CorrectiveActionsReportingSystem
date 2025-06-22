using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class StatusOfEffectiveness
    {
        [Key]
        public int Id { get; set; }
        public bool IsS1 { get; set; } = false;
        public bool IsS2 { get; set; } = false;
        public bool IsS3 { get; set; } = false;
        public string VerifiedBy { get; set; } = string.Empty;
        public string NotedBy { get; set; } = string.Empty;
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
