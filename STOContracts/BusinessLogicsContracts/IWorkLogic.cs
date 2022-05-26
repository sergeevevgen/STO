using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IWorkLogic
    {
        List<WorkViewModel> Read(WorkBindingModel model);
        List<WorkTypeViewModel> ReadType(WorkTypeBindingModel model);
        void CreateOrUpdate(WorkTypeBindingModel model);

        void CreateWork(CreateWorkBindingModel model);

        void TakeWorkInWork(ChangeWorkStatusBindingModel model);

        void FinishWork(ChangeWorkStatusBindingModel model);

        void Delete(WorkTypeBindingModel model);
    }
}
