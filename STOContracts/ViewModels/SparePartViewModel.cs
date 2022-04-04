using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class SparePartViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Запчасть")]
        public string SparePartName { get; set; }

        [DisplayName("Заводской номер")]
        public int FactoryNumber { get; set; }

        [DisplayName("Стоимость")]
        public decimal Price { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Единица измерения")]
        public string UMeasurement { get; set; }

        public Dictionary<int, string> Cars { get; set; }
    }
}
