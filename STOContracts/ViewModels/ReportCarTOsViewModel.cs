using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    //Модель для получения списка то по выбранному авто (роль Работник)
    public class ReportCarTOsViewModel
    {
        public string Car { get; set; }
        public List<string> TOs { get; set; }
    }
}
