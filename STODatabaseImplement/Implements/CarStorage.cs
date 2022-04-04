using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using STOContracts.BindingModels;
using STODatabaseImplement.Models;

namespace STODatabaseImplement.Implements
{
    public class CarStorage : ICarStorage
    {
        public List<CarViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.Cars
                .Include(rec => rec.TOCars)
                .ThenInclude(rec => rec.TO)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        private static CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                CarBrand = car.CarBrand,
                CarModel = car.CarModel,
                VIN = car.VIN,
                OwnerPhoneNumber = car.OwnerPhoneNumber
                .ToDictionary(recDC => recDC.ClientId, recDC => recDC.Client?.ClientFIO)
            }
        }
    }
}
