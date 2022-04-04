using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public string WorkName { get; set; }
        [Required]
        public decimal NetPrice { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("WorkId")]
        public virtual List<WorkTimeOfWork> WorkTimeOfWorks { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("WorkId")]
        public virtual List<WorkSparePart> WorkSpareParts { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("WorkId")]
        public virtual List<TOWork> TOWorks { get; set; }
    }
}
