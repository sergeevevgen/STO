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
    public class SparePart
    {
        public int Id { get; set; }
        [Required]
        public string SparePartName { get; set; }
        [Required]
        public int FactoryNumber { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public SparePartStatus Status { get; set; }
        [Required]
        public UnitMeasurement UMeasurement { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("SparePartId")]
        public virtual List<CarSparePart> CarSpareParts { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("SparePartId")]
        public virtual List<WorkSparePart> WorkSpareParts { get; set; }
    }
}
