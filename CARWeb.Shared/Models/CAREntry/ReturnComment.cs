using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class ReturnComment
    {
        [Key]
        public int Id { get; set; }
        public string From { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
