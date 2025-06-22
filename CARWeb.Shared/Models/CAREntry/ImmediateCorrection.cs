using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class ImmediateCorrection
    {
        [Key]
        public int Id { get; set; }

        public string ActionsToCorrectDescription { get; set; } = string.Empty;
        public List<string> ActionsToCorrectFiles { get; set; } = new List<string>();

        public string ActionsToDealDescription { get; set; } = string.Empty;
        public List<string> ActionsToDealFiles { get; set; } = new List<string>();

        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
