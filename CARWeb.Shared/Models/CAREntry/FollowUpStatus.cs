using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class FollowUpStatus
    {
        [Key]
        public int Id { get; set; }
        public DateTime F1Date { get; set; } = DateTime.Now;  
        public List<string> F1Evidences { get; set; } = new List<string>();
        public string F1StatusOfActions { get; set; } = string.Empty;
        public string F1VerifiedBy { get; set; } = string.Empty;

        public DateTime F2Date { get; set; } = DateTime.Now;
        public List<string> F2Evidences { get; set; } = new List<string>();
        public string F2StatusOfActions { get; set; } = string.Empty;
        public string F2VerifiedBy { get; set; } = string.Empty;

        public DateTime F3Date { get; set; } = DateTime.Now;
        public List<string> F3Evidences { get; set; } = new List<string>();
        public string F3StatusOfActions { get; set; } = string.Empty;
        public string F3VerifiedBy { get; set; } = string.Empty;

        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
