using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    public class CreateTOBindingModel
    {
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public Dictionary<int, (string, int)> TOWorks { get; set; }
        public decimal Sum { get; set; }
    }
}
