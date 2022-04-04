using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class CarSparePart
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int SparePartId { get; set; }

        public virtual Car Car { get; set; }
        public virtual SparePart SparePart { get; set; }
    }
}
