using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.ViewModels;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportCarTOsViewModel> CarTOs { get; set; }
        public List<ReportSparePartsWorkViewModel> SparePartsWork { get; set; }
    }
}
