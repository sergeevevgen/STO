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
    public class WorkTypeStorage : IWorkTypeStorage
    {
        public void Delete(WorkTypeBindingModel model)
        {
            using var context = new STODatabase();
            WorkType element = context.WorkTypes.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.WorkTypes.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WorkTypeViewModel GetElement(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var worktype = context.WorkTypes
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .FirstOrDefault(rec => rec.WorkName == model.WorkName ||
            rec.Id == model.Id);
            return worktype != null ? CreateModel(worktype) : null;
        }

        public List<WorkTypeViewModel> GetFilteredList(WorkTypeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.WorkTypes
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .Where(rec => rec.WorkName.Contains(model.WorkName) || rec.Id == model.Id)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<WorkTypeViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.WorkTypes
            .Include(rec => rec.TimeOfWork)
            .Include(rec => rec.WorkSpareParts)
            .ThenInclude(rec => rec.SparePart)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(WorkTypeBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WorkType work = new WorkType()
                {
                    WorkName = model.WorkName,
                    TimeOfWorkId = model.TimeOfWorkId,
                };
                context.WorkTypes.Add(work);
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

        public void Update(WorkTypeBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.WorkTypes.FirstOrDefault(rec => rec.Id ==
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

        private static WorkType CreateModel(WorkTypeBindingModel model, WorkType work,
            STODatabase context)
        {
            work.WorkName = model.WorkName;
            work.TimeOfWorkId = model.TimeOfWorkId;

            if (model.Id.HasValue)
            {
                var workSpareParts = context.WorkSpareParts
                    .Where(rec => rec.WorkTypeId == model.Id.Value)
                    .ToList();

                context.WorkSpareParts
                    .RemoveRange(workSpareParts
                    .Where(rec => !model.WorkSpareParts.ContainsKey(rec.SparePartId))
                    .ToList());
                context.SaveChanges();

                foreach (var update in workSpareParts)
                {
                    update.Count = model.WorkSpareParts[update.WorkTypeId].Item2;
                    model.WorkSpareParts.Remove(update.WorkTypeId);
                }
                context.SaveChanges();
            }
            foreach (var uwsp in model.WorkSpareParts)
            {
                context.WorkSpareParts.Add(new WorkSparePart
                {
                    WorkTypeId = work.Id,
                    SparePartId = uwsp.Key,
                    Count = uwsp.Value.Item2
                });
                context.SaveChanges();
            }
            return work;
        }

        private static WorkTypeViewModel CreateModel(WorkType work)
        {
            return new WorkTypeViewModel
            {
                Id = work.Id,
                WorkName = work.WorkName,
                TimeOfWorkId = work.TimeOfWorkId,
                Hours = work.TimeOfWork.Hours,
                WorkSpareParts = work.WorkSpareParts
                .ToDictionary(recPC => recPC.WorkTypeId,
                recPC => (recPC.SparePart?.SparePartName, recPC.Count))
            };
        }
    }
}
