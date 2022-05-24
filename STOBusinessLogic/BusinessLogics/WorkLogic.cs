using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class WorkLogic : IWorkLogic
    {
        private readonly IWorkStorage _workStorage;
        private readonly IWorkTypeStorage _workTypeStorage;

        public WorkLogic(IWorkStorage workStorage, IWorkTypeStorage workTypeStorage)
        {
            _workStorage = workStorage;
            _workTypeStorage = workTypeStorage;
        }

        public void CreateOrUpdate(WorkTypeBindingModel model)
        {
            var element = _workTypeStorage.GetElement(new WorkTypeBindingModel
            {
                WorkName = model.WorkName,
                TimeOfWorkId = model.TimeOfWorkId,
                WorkSpareParts = model.WorkSpareParts
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть работа с таким названием");
            }

            if (model.Id.HasValue)
            {
                _workTypeStorage.Update(model);
            }
            else
            {
                _workTypeStorage.Insert(model);
            }
        }

        public void CreateWork(CreateWorkBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(WorkTypeBindingModel model)
        {
            var element = _workTypeStorage.GetElement(new WorkTypeBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _workTypeStorage.Delete(model);
        }

        public void FinishWork(ChangeWorkStatusBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<WorkViewModel> Read(WorkBindingModel model)
        {
            if (model == null)
            {
                return _workStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WorkViewModel>() { _workStorage.GetElement(model) };
            }

            return _workStorage.GetFilteredList(model);
        }

        public void TakeWorkInWork(ChangeWorkStatusBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
