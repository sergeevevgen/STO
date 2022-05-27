using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.ViewModels;

namespace STOContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<TOViewModel>? TOs { get; set; }
        public List<WorkViewModel>? Works { get; set; }
        public int EmployeeId { get; set; }
        public int StoreKeeperId { get; set; }
    }
}
