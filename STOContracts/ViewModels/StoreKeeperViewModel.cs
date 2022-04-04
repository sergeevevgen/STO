using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class StoreKeeperViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DisplayName("Логин")]
        public string login { get; set; }

        [DisplayName("Пароль")]
        public string password { get; set; }
    }
}
