using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class WorkTypeViewModel
    {
        public int Id { get; set; }
        public string WorkName { get; set; }

        /// <summary>
        /// Время выполнения работы
        /// </summary>
        public int TimeOfWorkId { get; set; }
        public int Hours { get; set; }

        /// <summary>
        /// Необходимые детали и расходники (int - id, string - название, decimal, потому что может быть не целое (например, 0.8 л масла))
        /// </summary>
        public Dictionary<int, (string, decimal)> WorkSpareParts { get; set; }
    }
}
