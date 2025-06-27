using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Models.DepartmentSection;

namespace CARWeb.Shared.Models.CAREntry.EntryUser
{
    public class IssuedBy
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
        public List<IssuedByItem> IssuedByItems { get; set; }
    }
}
