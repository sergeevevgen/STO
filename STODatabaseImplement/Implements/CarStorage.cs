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
            .Include(rec => rec.DepositClients)
            .ThenInclude(rec => rec.Client)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.Cars
            .Include(rec => rec.DepositClients)
            .ThenInclude(rec => rec.Client)
            .Where(rec => rec.DepositName.Contains(model.CarBrand))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var deposit = context.Cars
            .Include(rec => rec.DepositClients)
            .ThenInclude(rec => rec.Client)
            .FirstOrDefault(rec => rec.DepositName == model.CarBrand || rec.Id == model.Id);
            return deposit != null ? CreateModel(deposit) : null;
        }
        public void Insert(CarBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Car car = new Car()
                {
                    CarBrand = model.CarBrand,
                    CarModel = model.CarModel,
                    Id = (int)model.Id, //TODO: nado?
                };
                context.Cars.Add(car);
                context.SaveChanges();
                CreateModel(model, car, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(CarBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(CarBindingModel model)
        {
            using var context = new STODatabase();
            Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        //TODO: доделать
        private static Car CreateModel(CarBindingModel model, Car deposit, STODatabase context)
        {
            deposit.CarModel = model.CarModel;
            deposit.CarBrand = model.CarBrand;
            deposit.Id = (int)model.Id; //TODO: nado?
            if (model.Id.HasValue)
            {
                var clientDeposits = context.ClientDeposits.Where(rec => rec.DepositId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ClientDeposits.RemoveRange(clientDeposits.Where(rec => !model.ClientDeposits.ContainsKey(rec.ClientId)).ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var cd in model.ClientDeposits)
            {
                context.ClientDeposits.Add(new ClientDeposit
                {
                    DepositId = deposit.Id,
                    ClientId = cd.Key,
                });
                context.SaveChanges();
            }
            return deposit;
        }
        private static CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                CarBrand = car.CarBrand,
                CarModel = car.CarModel,
                OwnerPhoneNumber = car.OwnerPhoneNumber
                .ToDictionary(recDC => recDC.ClientId, recDC => recDC.Client?.ClientFIO)
                //DepositCurrencies = deposit.DepositCurrencies
                //.ToDictionary(recDC => recDC.CurrencyId, recDC => recDC.Currency?.CurrencyName)
            };
        }
    }
}
