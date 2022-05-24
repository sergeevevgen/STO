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
    public class ServiceRecordLogic : IServiceRecordLogic
    {
        private readonly IServiceRecordStorage _serviceRecordStorage;

        public ServiceRecordLogic(IServiceRecordStorage serviceRecordStorage)
        {
            _serviceRecordStorage = serviceRecordStorage;
        }

        public void CreateOrUpdate(ServiceRecordBindingModel model)
        {
            var element = _serviceRecordStorage.GetElement(new ServiceRecordBindingModel
            {
                Description = model.Description
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть запись с таким названием");
            }

            if (model.Id.HasValue)
            {
                _serviceRecordStorage.Update(model);
            }
            else
            {
                _serviceRecordStorage.Insert(model);
            }
        }

        public void Delete(ServiceRecordBindingModel model)
        {
            var element = _serviceRecordStorage.GetElement(new ServiceRecordBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _serviceRecordStorage.Delete(model);
        }

        public List<ServiceRecordViewModel> Read(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return _serviceRecordStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ServiceRecordViewModel> { _serviceRecordStorage.GetElement(model) };
            }

            return _serviceRecordStorage.GetFilteredList(model);
        }
    }
}
