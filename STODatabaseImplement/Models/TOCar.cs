using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    /// <summary>
    /// Связь ТО и автомобилей
    /// </summary>
    public class TOCar
    {
        public int Id { get; set; }
        public int TOId { get; set; }
        public int CarId { get; set; }

        public virtual TO TO { get; set; }
        public virtual Car Car { get; set; }
    }
}
