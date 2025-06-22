using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Models.CARLabel;

namespace CARWeb.Shared.Models.CAREntry
{
    public class StandardItem
    {
        [Key]
        public int Id { get; set; }
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
        public Standard Standard { get; set; }
        public int StandardId { get; set; }
    }
}
