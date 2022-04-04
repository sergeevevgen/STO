using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    public class WorkBindingModel
    {
        public int? Id { get; set; }
        public string WorkName { get; set; }

        /// <summary>
        /// Время выполнения работы
        /// </summary>
        public int TimeOfWorkId { get; set; }
        public decimal NetPrice { get; set; }

        /// <summary>
        /// Необходимые детали и расходники (int - id, string - название, int - кол-во)
        /// </summary>
        public Dictionary<int, (string, int)> WorkSpareParts { get; set; }
    }
}
