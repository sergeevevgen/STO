using STOContracts.ViewModels;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportSparePartsWorkViewModel> SpareParts { get; set; }
        public List<ReportCarTOsViewModel> TOs { get; set; }
    }
}
