using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual Car Car { get; set; }
    }
}
