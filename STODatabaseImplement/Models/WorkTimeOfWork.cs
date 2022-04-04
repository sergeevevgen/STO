using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    /// <summary>
    /// Многие-ко-многим (Работы и время работы)
    /// </summary>
    public class WorkTimeOfWork
    {
        public int Id { get; set; }

        public int WorkId { get; set; }
        public int TimeOfWorkId { get; set; }

        public virtual Work Work { get; set; }
        public virtual TimeOfWork TimeOfWork { get; set; }
    }
}
