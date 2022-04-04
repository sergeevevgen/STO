using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class TOViewModel
    { 
        public int? Id { get; set; }
        public int CarId { get; set; }
        public Dictionary<int, (string, decimal)> TOWorks { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата оформления ТО")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата начала выполнения ТО")]
        public DateTime? DateImplement { get; set; }

        [DisplayName("Дата завершения ТО")]
        public DateTime? DateOver { get; set; }
    }
}
