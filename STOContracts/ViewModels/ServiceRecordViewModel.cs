using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    /// <summary>
    /// Запись сервиса автомобиля
    /// </summary>
    public class ServiceRecordViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата начала")]
        public DateTime DateBegin { get; set; }
        [DisplayName("Дата окончания")]
        public DateTime? DateEnd { get; set; }
        [DisplayName("Запись")]
        public string Description { get; set; }
    }
}
