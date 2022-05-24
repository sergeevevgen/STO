﻿using System;
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
    public class CarLogic : ICarLogic
    {
        private readonly ICarStorage _carStorage;

        public CarLogic(ICarStorage carStorage)
        {
            _carStorage = carStorage;
        }

        public void CreateOrUpdate(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel
            {
                VIN = model.VIN
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такое авто");
            }

            if (model.Id.HasValue)
            {
                _carStorage.Update(model);
            }
            else
            {
                _carStorage.Insert(model);
            }
        }

        public void Delete(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _carStorage.Delete(model);
        }

        public List<CarViewModel> Read(CarBindingModel model)
        {
            if (model == null)
            {
                return _carStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<CarViewModel>() { _carStorage.GetElement(model) };
            }

            return _carStorage.GetFilteredList(model);
        }
    }
}
