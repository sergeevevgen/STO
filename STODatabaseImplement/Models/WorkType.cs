using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class WorkType
    {
        public int Id { get; set; }
        [Required]

        public string WorkName { get; set; }

        public int TimeOfWorkId { get; set; }

        [ForeignKey("WorkTypeId")]
        public virtual List<WorkSparePart> WorkSpareParts { get; set; }
        public virtual TimeOfWork TimeOfWork { get; set; }
    }
}
