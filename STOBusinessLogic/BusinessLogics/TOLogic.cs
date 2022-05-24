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

        public TOLogic(ITOStorage tOStorage)
        {
            _tOStorage = tOStorage;
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
        }

        public void CreateTO(CreateTOBindingModel model)
        {
            _tOStorage.Insert(new TOBindingModel
            {
                CarId = model.CarId,
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
