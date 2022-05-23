using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FIO { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("EmployeeId")]
        public virtual List<TO> TOs { get; set; }
    }
}
