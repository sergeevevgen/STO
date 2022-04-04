using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class TimeOfWork
    {
        public int Id { get; set; }
        [Required]
        public int Hours { get; set; }
        
        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("TimeOfWorkId")]
        public virtual List<Work> Works { get; set; }
    }
}
