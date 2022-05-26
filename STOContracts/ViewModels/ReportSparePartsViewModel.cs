using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    //Модель для получения отчёта по запчастям (роль Кладовщик)
    public class ReportCurrenciesViewModel
    {
        public string SparePartName { get; set; }
        public DateTime DateAdding { get; set; }
       /*???*/ public string Works { get; set; }
        public string WorkTypes { get; set; }
    }
}
