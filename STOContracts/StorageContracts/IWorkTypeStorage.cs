using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.StorageContracts
{
    public interface IWorkTypeStorage
    {
        List<WorkTypeViewModel> GetFullList();
        List<WorkTypeViewModel> GetFilteredList(WorkTypeBindingModel model);

        WorkTypeViewModel GetElement(WorkTypeBindingModel model);

        void Insert(WorkTypeBindingModel model);

        void Update(WorkTypeBindingModel model);

        void Delete(WorkTypeBindingModel model);
    }
}
