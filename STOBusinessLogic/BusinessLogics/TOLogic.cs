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
    public class TOLogic : ITOLogic
    {
        private readonly ITOStorage _tOStorage;
        private readonly IServiceRecordLogic _serviceRecordLogic;
        public TOLogic(ITOStorage tOStorage, IServiceRecordLogic serviceRecordLogic)
        {
            _tOStorage = tOStorage;
            _serviceRecordLogic = serviceRecordLogic;
        }

        public void CloseTO(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel { Id = model.TOId });
            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }
            if (tO.Status != Enum.GetName(typeof(TOStatus), 2))
            {
                throw new Exception("ТО не в статусе \"Готов\"");
            }
            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                TOWorks = tO.TOWorks,
                Sum = tO.Sum,
                Status = TOStatus.Выдан_клиенту,
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                DateOver = tO.DateOver
            });
            string list = "";
            foreach(var work in tO.TOWorks)
            {
                list += work.Value.Item1 + "\n";
                list += work.Value.Item2 + "\n";
            }
            _serviceRecordLogic.CreateOrUpdate(new ServiceRecordBindingModel 
            {
                CarId = tO.CarId,
                DateBegin = tO.DateCreate,
                DateEnd = tO.DateOver,
                Description = list
            });
        }

        public void Update(TOBindingModel model)
        {
            if (model.Id.HasValue)
                _tOStorage.Update(model);
        }

        public void CreateTO(CreateTOBindingModel model)
        {
            _tOStorage.Insert(new TOBindingModel
            {
                CarId = model.CarId,
                EmployeeId = model.EmployeeId,
                TOWorks = model.TOWorks,
                Sum = model.Sum,
                Status = TOStatus.Принят,
                DateCreate = DateTime.Now
            });
        }

        public void FinishTO(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel { Id = model.TOId });
            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }

            if (tO.Status != Enum.GetName(typeof(TOStatus), 1))
            {
                throw new Exception("ТО не в статусе \"Выполняется\"");
            }

            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                CarId = tO.CarId,
                EmployeeId = tO.EmployeeId,
                TOWorks = tO.TOWorks,
                Sum = tO.Sum,
                Status = TOStatus.Готов,
                DateCreate = tO.DateCreate,
                DateImplement = tO.DateImplement,
                DateOver = DateTime.Now
            });
        }

        public List<TOViewModel> Read(TOBindingModel model)
        {
            if (model == null)
            {
                return _tOStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TOViewModel> { _tOStorage.GetElement(model) };
            }
            return _tOStorage.GetFilteredList(model);
        }

        public void TakeTOInWork(ChangeTOStatusBindingModel model)
        {
            var tO = _tOStorage.GetElement(new TOBindingModel { Id = model.TOId });
            if (tO == null)
            {
                throw new Exception("ТО не найдено");
            }

            if (tO.Status != Enum.GetName(typeof(TOStatus), 0))
            {
                throw new Exception("ТО не в статусе \"Принят\"");
            }

            _tOStorage.Update(new TOBindingModel
            {
                Id = tO.Id,
                EmployeeId = model.EmployeeId,
                CarId = tO.CarId,
                TOWorks = tO.TOWorks,
                Sum = tO.Sum,
                Status = TOStatus.Выполняется,
                DateCreate = tO.DateCreate,
                DateImplement = DateTime.Now
            });
        }
    }
}
