using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class TimeOfWorkViewModelcs
    {
        public int Id { get; set; }

        [DisplayName("Продолжительность (ч.)")]
        public int Hours { get; set; }
    }
}
