using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.StorageContracts
{
    public interface ICarStorage
    {
        List<CarViewModel> GetFullList();
        List<CarViewModel> GetFilteredList(CarBindingModel model);

        CarViewModel GetElement(CarBindingModel model);

        void Insert(CarBindingModel model);

        void Update(CarBindingModel model);

        void Delete(CarBindingModel model);
    }
}
