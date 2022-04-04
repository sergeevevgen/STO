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
        public List<int> WorksIds { get; set; }
        public decimal Sum { get; set; }
    }
}
