using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    /// <summary>
    /// Автомобили, обслуживающиеся на СТО
    /// </summary>
    public class CarViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Бренд авто (VW, Ford)
        /// </summary>
        [DisplayName("Марка автомобиля")]
        public string CarBrand { get; set; }

        /// <summary>
        /// Модель авто (Polo, Fusion)
        /// </summary>
        [DisplayName("Модель автомобиля")]
        public string CarModel { get; set; }

        /// <summary>
        /// VIN-номер авто (длина 17 символов)
        /// </summary>
        [DisplayName("VIN автомобиля")]
        public string VIN { get; set; }

        /// <summary>
        /// Номер телефона владельца
        /// </summary>
        [DisplayName("Телефон владельца")]
        public string OwnerPhoneNumber { get; set; }
    }
}
