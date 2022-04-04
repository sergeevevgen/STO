using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;
using STODatabaseImplement.Models;

namespace STODatabaseImplement.Implements
{
    public class TimeOfWorkStorage : ITimeOfWorkStorage
    {
        public void Delete(TimeOfWorkBindingModel model)
        {
            using var context = new STODatabase();
            TimeOfWork element = context.TimeOfWorks.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.TimeOfWorks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TimeOfWorkViewModel GetElement(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var timeOfWork = context.TimeOfWorks
            .FirstOrDefault(rec => rec.Id
            == model.Id);
            return timeOfWork != null ? CreateModel(timeOfWork) : null;
        }

        public List<TimeOfWorkViewModel> GetFilteredList(TimeOfWorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.TimeOfWorks
            .Where(rec => rec.Id.Equals(model.Id))
            .Select(CreateModel)
            .ToList();
        }

        public List<TimeOfWorkViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.TimeOfWorks
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(TimeOfWorkBindingModel model)
        {
            using var context = new STODatabase();
            context.TimeOfWorks.Add(CreateModel(model, new TimeOfWork()));
            context.SaveChanges();
        }

        public void Update(TimeOfWorkBindingModel model)
        {
            using var context = new STODatabase();
            var element = context.TimeOfWorks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        private static TimeOfWork CreateModel(TimeOfWorkBindingModel model, TimeOfWork timeOfWork)
        {
            timeOfWork.Hours = model.Hours;
            return timeOfWork;
        }

        private static TimeOfWorkViewModel CreateModel(TimeOfWork timeOfWork)
        {
            return new TimeOfWorkViewModel
            {
                Id = timeOfWork.Id,
                Hours = timeOfWork.Hours
            };
        }
    }
}
