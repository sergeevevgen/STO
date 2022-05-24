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
    public class Work
    {
        public int Id { get; set; }
        public int StoreKeeperId { get; set; }
        public int WorkTypeId { get; set; }
        [Required]
        public string WorkName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal NetPrice { get; set; }
        
        [Required]
        public WorkStatus WorkStatus { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("WorkId")]
        public virtual List<TOWork> TOWorks { get; set; }
        public virtual StoreKeeper StoreKeeper { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
