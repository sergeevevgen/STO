using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.StorageContracts
{
    public interface ITOStorage
    {
        List<TOViewModel> GetFullList();
        List<TOViewModel> GetFilteredList(TOBindingModel model);

        TOViewModel GetElement(TOBindingModel model);

        void Insert(TOBindingModel model);

        void Update(TOBindingModel model);

        void Delete(TOBindingModel model);
    }
}
