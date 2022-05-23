using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class WorkViewModel
    {
        public int Id { get; set; }

        [DisplayName("Наименование работы")]
        public string WorkName { get; set; }
        public int? StoreKeeperId { get; set; }
        public string StoreKeeperFIO { get; set; }
        public int TimeOfWorkId { get; set; }
        public int Hours { get; set; }
        /// <summary>
        /// Стоимость услуги
        /// </summary>
        [DisplayName("Стоимость услуги")]
        public decimal Price { get; set; }

        [DisplayName("Стоимость с учётом запчастей")]
        public decimal NetPrice { get; set; }
        /// <summary>
        /// Наименование, стоимость
        /// </summary>
        public Dictionary<int, (string, decimal)> WorkSpareParts { get; set; }
    }
}
