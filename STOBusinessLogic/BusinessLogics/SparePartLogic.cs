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
    public class SparePartLogic : ISparePartLogic
    {
        private readonly ISparePartStorage _sparePartStorage;

        public SparePartLogic(ISparePartStorage sparePartStorage)
        {
            _sparePartStorage = sparePartStorage;
        }

        public void CreateOrUpdate(SparePartBindingModel model)
        {
            var element = _sparePartStorage.GetElement(new SparePartBindingModel
            {
                FactoryNumber = model.FactoryNumber
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть запчасть с таким названием");
            }

            if (model.Id.HasValue)
            {
                _sparePartStorage.Update(model);
            }
            else
            {
                _sparePartStorage.Insert(model);
            }
        }

        public void Delete(SparePartBindingModel model)
        {
            var element = _sparePartStorage.GetElement(new SparePartBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _sparePartStorage.Delete(model);
        }

        public List<SparePartViewModel> Read(SparePartBindingModel model)
        {
            if (model == null)
            {
                return _sparePartStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<SparePartViewModel> { _sparePartStorage.GetElement(model) };
            }

            return _sparePartStorage.GetFilteredList(model);
        }
    }
}
