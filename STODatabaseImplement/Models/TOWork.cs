using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class TOWork
    {
        public int Id { get; set; }
        public int TOId { get; set; }
        public int WorkId { get; set; }

        [Required]
        public decimal Count { get; set; }
        public virtual TO TO { get; set; }
        public virtual Work Work { get; set; }
    }
}
