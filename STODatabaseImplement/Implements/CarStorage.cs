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
        public void Delete(CarBindingModel model)
        {
            using var context = new STODatabase();
            Car element = context.Cars.FirstOrDefault(rec => rec.Id ==
            model.Id);
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

        public void Insert(CarBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                //Сначала надо создать значение в таблице Car,
                //а уже потом добавлять внешние ключи в таблицу ServiceRecords, TOCars, CarSpareParts
                Car car = new Car()
                {
                    CarBrand = model.CarBrand,
                    CarModel = model.CarModel,
                    VIN = model.VIN,
                    OwnerPhoneNumber = model.OwnerPhoneNumber
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
                var element = context.Cars.FirstOrDefault(rec => rec.Id ==
                model.Id);
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

        public List<CarViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.Cars
            .Include(rec => rec.ServiceRecords)
            .Include(rec => rec.CarSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.TO)
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
            var work = context.Cars
            .Include(rec => rec.ServiceRecords)
            .Include(rec => rec.CarSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.TO)
            .FirstOrDefault(rec => rec.CarModel == model.CarModel ||
            rec.Id == model.Id);
            return work != null ? CreateModel(work) : null;
        }

        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.Cars
            .Include(rec => rec.ServiceRecords)
            .Include(rec => rec.CarSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.TO)
            .Where(rec => rec.CarModel.Contains(model.CarModel))
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
            };
        }

        private static Car CreateModel(CarBindingModel model, Car car,
            STODatabase context)
        {
            car.CarBrand = model.CarBrand;
            car.CarModel = model.CarModel;
            car.VIN = model.VIN;
            car.OwnerPhoneNumber = model.OwnerPhoneNumber;
            return car;
        }
    }
}
