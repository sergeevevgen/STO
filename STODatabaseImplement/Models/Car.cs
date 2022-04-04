using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string OwnerPhoneNumber { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("CarId")]
        public virtual List<ServiceRecord> ServiceRecords { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("CarId")]
        public virtual List<TOCar> TOCars { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("CarId")]
        public virtual List<CarSparePart> CarSpareParts { get; set; }

        public int EmpoyeeId { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
