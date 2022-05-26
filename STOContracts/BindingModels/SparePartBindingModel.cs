using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.Enums;

namespace STOContracts.BindingModels
{
    public class SparePartBindingModel
    {
        public int? Id { get; set; }
        public string SparePartName { get; set; }

        public string FactoryNumber { get; set; }

        public decimal Price { get; set; }

        public SparePartStatus Status { get; set; }

        public UnitMeasurement UMeasurement { get; set; }

        /// <summary>
        /// Словарь подходящих моделей машин
        /// </summary>
        public Dictionary<int, (string, string)>  Cars { get; set; }


    }
}
