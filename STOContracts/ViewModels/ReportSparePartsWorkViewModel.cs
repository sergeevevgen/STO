using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class ReportSparePartsWorkViewModel
    {
        public string Work { get; set; }
        public List<Tuple<string, decimal>> SpareParts { get; set; }
    }
}
