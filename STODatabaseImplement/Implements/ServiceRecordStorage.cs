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
    public class ServiceRecordStorage : IServiceRecordStorage
    {
        public void Delete(ServiceRecordBindingModel model)
        {
            using var context = new STODatabase();
            ServiceRecord element = context.ServiceRecords.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.ServiceRecords.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ServiceRecordViewModel GetElement(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var serviceRecord = context.ServiceRecords
            .Include(rec => rec.Car)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return serviceRecord != null ? CreateModel(serviceRecord) : null;
        }

        public List<ServiceRecordViewModel> GetFilteredList(ServiceRecordBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.ServiceRecords
                .Include(rec => rec.Car)
                .Where(rec => rec.Id.Equals(model.Id))
                .Select(CreateModel)
                .ToList();
        }

        public List<ServiceRecordViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.ServiceRecords
                .Include(rec => rec.Car)
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(ServiceRecordBindingModel model)
        {
            using var context = new STODatabase();
            context.ServiceRecords.Add(CreateModel(model, new ServiceRecord()));
            context.SaveChanges();
        }

        public void Update(ServiceRecordBindingModel model)
        {
            using var context = new STODatabase();
            var element = context.ServiceRecords
            .FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        private static ServiceRecord CreateModel(ServiceRecordBindingModel model, ServiceRecord serviceRecord)
        {
            serviceRecord.Date = DateTime.Now;
            serviceRecord.Description = model.Description;
            return serviceRecord;
        }

        private static ServiceRecordViewModel CreateModel(ServiceRecord serviceRecord)
        {
            return new ServiceRecordViewModel
            {
                Id = serviceRecord.Id,
                Date = serviceRecord.Date,
                Description = serviceRecord.Description
            };
        }
    }
}
