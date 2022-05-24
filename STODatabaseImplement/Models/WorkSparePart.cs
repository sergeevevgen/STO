using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class WorkSparePart
    {
        public int Id { get; set; }
        public int WorkTypeId { get; set; }
        public int SparePartId { get; set; }

        [Required]
        public decimal Count { get; set; }
        public virtual WorkType WorkType { get; set; }
        public virtual SparePart SparePart { get; set; }
    }
}
