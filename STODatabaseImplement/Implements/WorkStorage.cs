using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;
using STODatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace STODatabaseImplement.Implements
{
    public class WorkStorage : IWorkStorage
    {
        public void Delete(WorkBindingModel model)
        {
            using var context = new STODatabase();
            Work element = context.Works.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Works.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WorkViewModel GetElement(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var work = context.Works
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.TO)
            .Include(rec => rec.StoreKeeper)
            .FirstOrDefault(rec => rec.WorkName == model.WorkName ||
            rec.Id == model.Id);
            return work != null ? CreateModel(work) : null;
        }

        public List<WorkViewModel> GetFilteredList(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.Works
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.TO)
            .Include(rec => rec.StoreKeeper)
            .Where(rec => rec.WorkName.Contains(model.WorkName) || rec.StoreKeeperId == model.StoreKeeperId)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<WorkViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.Works
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Include(rec => rec.TOWorks)
            .ThenInclude(rec => rec.TO)
            .Include(rec => rec.StoreKeeper)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(WorkBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Work work = new Work()
                {
                    WorkName = model.WorkName,
                    NetPrice = model.NetPrice,
                    TimeOfWorkId = model.TimeOfWorkId,
                    Price = model.Price,
                    StoreKeeperId = model.StoreKeeperId.Value,
                    WorkStatus = model.WorkStatus
                };
                context.Works.Add(work);
                context.SaveChanges();
                CreateModel(model, work, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WorkBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Works.FirstOrDefault(rec => rec.Id ==
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

        private static Work CreateModel(WorkBindingModel model, Work work,
            STODatabase context)
        {
            if (model.Id.HasValue)
            {
                var workSpareParts = context.WorkSpareParts
                    .Where(rec => rec.WorkId == model.Id.Value)
                    .ToList();

                context.WorkSpareParts
                    .RemoveRange(workSpareParts
                    .Where(rec => !model.WorkSpareParts.ContainsKey(rec.SparePartId))
                    .ToList());
                context.SaveChanges();

                foreach (var update in workSpareParts)
                {
                    update.Count = model.WorkSpareParts[update.WorkId].Item2;
                    model.WorkSpareParts.Remove(update.WorkId);
                }
                context.SaveChanges();
            }
            foreach (var uwsp in model.WorkSpareParts)
            {
                context.WorkSpareParts.Add(new WorkSparePart
                {
                    WorkId = work.Id,
                    SparePartId = uwsp.Key,
                    Count = uwsp.Value.Item2
                });
                context.SaveChanges();
            }
            return work;
        }

        private static WorkViewModel CreateModel(Work work)
        {
            return new WorkViewModel
            {
                Id = work.Id,
                WorkName = work.WorkName,
                NetPrice = work.NetPrice,
                TimeOfWorkId = work.TimeOfWorkId,
                Price = work.Price,
                StoreKeeperId = work.StoreKeeperId,
                StoreKeeperFIO = work.StoreKeeper.FIO,
                Hours = work.TimeOfWork.Hours,
                WorkStatus = work.WorkStatus.ToString(),
                WorkSpareParts = work.WorkSpareParts
                .ToDictionary(recPC => recPC.WorkId,
                recPC => (recPC.SparePart?.SparePartName, recPC.Count))
            };
        }
    }
}
