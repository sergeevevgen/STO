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
    public class TimeOfWorkLogic : ITimeOfWorkLogic
    {
        private readonly ITimeOfWorkStorage _timeOfWorkStorage;

        public TimeOfWorkLogic(ITimeOfWorkStorage timeOfWorkStorage)
        {
            _timeOfWorkStorage = timeOfWorkStorage;
        }

        public void CreateOrUpdate(TimeOfWorkBindingModel model)
        {
            var element = _timeOfWorkStorage.GetElement(new TimeOfWorkBindingModel
            {
                Hours = model.Hours
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такая продолжительность работ");
            }

            if (model.Id.HasValue)
            {
                _timeOfWorkStorage.Update(model);
            }
            else
            {
                _timeOfWorkStorage.Insert(model);
            }
        }

        public void Delete(TimeOfWorkBindingModel model)
        {
            var element = _timeOfWorkStorage.GetElement(new TimeOfWorkBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _timeOfWorkStorage.Delete(model);
        }

        public List<TimeOfWorkViewModel> Read(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return _timeOfWorkStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<TimeOfWorkViewModel>() { _timeOfWorkStorage.GetElement(model) };
            }

            return _timeOfWorkStorage.GetFilteredList(model);
        }
    }
}
