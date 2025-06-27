using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Models.DepartmentSection;

namespace CARWeb.Shared.Models.CAREntry.EntryUser
{
    public class IssuedToItem
    {
        [Key]
        public int Id { get; set; }
        public DSection DSection { get; set; }
        public int DSectionId { get; set; }
        public IssuedTo IssuedTo { get; set; }
        public int IssuedToId {  get; set; }
    }
}
