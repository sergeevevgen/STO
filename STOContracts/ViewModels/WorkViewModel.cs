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
        public int TimeOfWorkId { get; set; }

        [DisplayName("Стоимость с учётом запчастей")]
        public decimal NetPrice { get; set; }
        public Dictionary<int, (string, int)> WorkSpareParts { get; set; }
    }
}
