using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    public class CreateWorkBindingModel
    {
        public int WorkTypeId { get; set; }
        public string WorkName { get; set; }
        public int StoreKeeperId { get; set; }
        public int Count { get; set; }
        public decimal Price  { get; set; }
        public decimal NetPrice { get; set; }
    }
}
