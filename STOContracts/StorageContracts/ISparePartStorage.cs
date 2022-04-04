using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.StorageContracts
{
    public interface ISparePartStorage
    {
        List<SparePartViewModel> GetFullList();
        List<SparePartViewModel> GetFilteredList(SparePartBindingModel model);

        SparePartViewModel GetElement(SparePartBindingModel model);

        void Insert(SparePartBindingModel model);

        void Update(SparePartBindingModel model);

        void Delete(SparePartBindingModel model);
    }
}
