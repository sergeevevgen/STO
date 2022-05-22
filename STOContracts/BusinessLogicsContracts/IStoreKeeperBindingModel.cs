using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.ViewModels;
using STOContracts.BindingModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IStoreKeeperBindingModel
    {
        List<StoreKeeperViewModel> Read(StoreKeeperBindingModel model);

        void CreateOrUpdate(StoreKeeperBindingModel model);

        void Delete(StoreKeeperBindingModel model);
    }
}
