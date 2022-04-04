using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STOContracts.Enums;
namespace STODatabaseImplement.Models
{
    public class TO
    {
        public int Id { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public TOStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public DateTime? DateOver { get; set; }

        [ForeignKey("TOId")]
        public virtual List<TOCar> TOCars { get; set; }

        [ForeignKey("TOId")]
        public virtual List<TOWork> TOWorks { get; set; }
    }
}
