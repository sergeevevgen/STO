using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;
using STOContracts.Enums;

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
            _workStorage.Insert(new WorkBindingModel
            {
                WorkName = model.WorkName,
                WorkTypeId = model.WorkTypeId,
                Price = model.Price,
                StoreKeeperId = model.StoreKeeperId,
                WorkStatus = WorkStatus.Принят,
                WorkSpareParts = _workTypeStorage.GetElement(new WorkTypeBindingModel { Id = model.WorkTypeId }).WorkSpareParts
            });
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
            var work = _workStorage.GetElement(new WorkBindingModel { Id = model.WorkId });
            if (work == null)
            {
                throw new Exception("Работа не найдена");
            }
            if (work.WorkStatus != Enum.GetName(typeof(WorkStatus), 2))
            {
                throw new Exception("Работа не в статусе \"Выполняется\"");
            }
            _workStorage.Update(new WorkBindingModel
            {
                Id = work.Id,
                StoreKeeperId = work.StoreKeeperId,
                WorkStatus = WorkStatus.Готов,
                WorkName = work.WorkName,
                Price = work.Price,
                NetPrice = work.NetPrice,
                WorkTypeId = work.WorkTypeId,
                WorkSpareParts = work.WorkSpareParts
            });
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

        public List<WorkTypeViewModel> ReadType(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return _workTypeStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WorkTypeViewModel>() { _workTypeStorage.GetElement(model) };
            }

            return _workTypeStorage.GetFilteredList(model);
        }

        public void TakeWorkInWork(ChangeWorkStatusBindingModel model)
        {
            var work = _workStorage.GetElement(new WorkBindingModel { Id = model.WorkId });
            if (work == null)
            {
                throw new Exception("Работа не найдена");
            }
            if (work.WorkStatus != Enum.GetName(typeof(WorkStatus), 0))
            {
                throw new Exception("Работа не в статусе \"Принят\"");
            }
            _workStorage.Update(new WorkBindingModel
            {
                Id = work.Id,
                StoreKeeperId = work.StoreKeeperId,
                WorkStatus = WorkStatus.Выполняется,
                WorkName = work.WorkName,
                Price = work.Price,
                NetPrice = work.NetPrice,
                WorkTypeId = work.WorkTypeId,
                WorkSpareParts = work.WorkSpareParts
            });
        }
    }
}
