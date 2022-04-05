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
    public class TOStorage : ITOStorage
    {
        public void Delete(TOBindingModel model)
        {
            using var context = new STODatabase();
            TO element = context.TOs.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.TOs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TOViewModel GetElement(TOBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var to = context.TOs
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.Car)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.Work)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return to != null ? CreateModel(to) : null;
        }

        public List<TOViewModel> GetFilteredList(TOBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.TOs
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.Car)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.Work)
            .Where(rec => rec.Id.Equals(model.Id))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<TOViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.TOs
            .Include(rec => rec.TOCars)
            .ThenInclude(rec => rec.Car)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.Work)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(TOBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                //Сначала надо создать значение в таблице TO,
                //а уже потом добавлять внешние ключи в таблицу TOCars, TOWorks
                TO to = new TO()
                {
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                    DateOver = model.DateOver
                };
                context.TOs.Add(to);
                context.SaveChanges();
                CreateModel(model, to, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(TOBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.TOs.FirstOrDefault(rec => rec.Id ==
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

        private static TO CreateModel(TOBindingModel model, TO tO, STODatabase context)
        {
            tO.Sum = model.Sum;
            tO.Status = model.Status;
            tO.DateCreate = model.DateCreate;
            tO.DateImplement = model.DateImplement;
            tO.DateOver = model.DateOver;
            if (model.Id.HasValue)
            {
                var tOWorks = context.TOWorks
                    .Where(rec => rec.TOId == model.Id.Value)
                    .ToList();

                context.TOWorks
                    .RemoveRange(tOWorks
                    .Where(rec => !model.TOWorks
                    .ContainsKey(rec.WorkId))
                    .ToList());
                context.SaveChanges();

                foreach (var update in tOWorks)
                {
                    update.Count =
                        model.TOWorks[update.TOId].Item2;
                    model.TOWorks.Remove(update.WorkId);
                }
                context.SaveChanges();
            }

            foreach (var uwsp in model.TOWorks)
            {
                context.TOWorks.Add(new TOWork
                {
                    TOId = model.Id.Value,
                    WorkId = uwsp.Key,
                    Count = uwsp.Value.Item2
                });
                context.SaveChanges();
            }
            return tO;
        }

        private static TOViewModel CreateModel(TO to)
        {
            return new TOViewModel
            {
                Id = to.Id,
                Sum = to.Sum,
                Status = to.Status.ToString(),
                DateCreate = to.DateCreate,
                TOWorks = to.TOWorks
                .ToDictionary(recPC => recPC.TOId,
                recPC => (recPC.TO?.Status.ToString(), recPC.Count))
            };
        }
    }
}
