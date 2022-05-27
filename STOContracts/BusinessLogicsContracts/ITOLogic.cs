using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface ITOLogic
    {
        List<TOViewModel> Read(TOBindingModel model);

        void CreateTO(CreateTOBindingModel model);
        void Update(TOBindingModel model);

        void TakeTOInWork(ChangeTOStatusBindingModel model);

        void FinishTO(ChangeTOStatusBindingModel model);

        void CloseTO(ChangeTOStatusBindingModel model);
    }
}
