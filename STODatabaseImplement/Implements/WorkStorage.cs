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
    /// <summary>
    /// Сделано
    /// </summary>
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
                .Include(rec => rec.WorkType)
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
                .Include(rec => rec.WorkType)
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
                .Include(rec => rec.WorkType)
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
                context.Works.Add(CreateModel(model, new Work()));
                context.SaveChanges();
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
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static Work CreateModel(WorkBindingModel model, Work work)
        {
            work.WorkName = model.WorkName;
            work.NetPrice = model.NetPrice;
            work.WorkTypeId = model.WorkTypeId.Value;
            work.Price = model.Price;
            work.StoreKeeperId = model.StoreKeeperId.Value;
            work.WorkStatus = model.WorkStatus;
            return work;
        }

        private static WorkViewModel CreateModel(Work work)
        {
            return new WorkViewModel
            {
                Id = work.Id,
                WorkName = work.WorkName,
                NetPrice = work.NetPrice,
                WorkTypeId = work.WorkTypeId,
                Price = work.Price,
                StoreKeeperId = work.StoreKeeperId,
                StoreKeeperFIO = work.StoreKeeper.FIO,
                Hours = work.WorkType.TimeOfWork.Hours,
                WorkStatus = work.WorkStatus.ToString(),
                WorkSpareParts = work.WorkType.WorkSpareParts
                .ToDictionary(recPC => recPC.WorkTypeId,
                recPC => (recPC.SparePart?.SparePartName, recPC.Count))
            };
        }
    }
}
