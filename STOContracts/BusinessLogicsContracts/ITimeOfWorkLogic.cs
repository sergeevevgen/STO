using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface ITimeOfWorkLogic
    {
        List<TimeOfWorkViewModel> Read(TimeOfWorkBindingModel model);

        void CreateOrUpdate(TimeOfWorkBindingModel model);

        void Delete(TimeOfWorkBindingModel model);
    }
}
