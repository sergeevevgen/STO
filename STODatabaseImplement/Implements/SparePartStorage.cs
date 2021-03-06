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

    /// <summary>
    /// Сделано
    /// </summary>
    public class SparePartStorage : ISparePartStorage
    {
        public void Delete(SparePartBindingModel model)
        {
            using var context = new STODatabase();
            SparePart element = context.SpareParts.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.SpareParts.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public SparePartViewModel GetElement(SparePartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var work = context.SpareParts
            .FirstOrDefault(rec => rec.FactoryNumber == model.FactoryNumber ||
            rec.Id == model.Id);
            return work != null ? CreateModel(work) : null;
        }

        public List<SparePartViewModel> GetFilteredList(SparePartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.SpareParts
            .Where(rec => rec.SparePartName.Contains(model.SparePartName) || (rec.FactoryNumber != string.Empty && rec.FactoryNumber.Equals(model.FactoryNumber)) || rec.Id == model.Id || rec.Price < model.Price)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<SparePartViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.SpareParts
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(SparePartBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
               
                SparePart sparePart = new SparePart()
                {
                    SparePartName = model.SparePartName,
                    FactoryNumber = model.FactoryNumber,
                    Price = model.Price,
                    Status = model.Status,
                    UMeasurement = model.UMeasurement
                };
                context.SpareParts.Add(sparePart);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(SparePartBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.SpareParts
                    .FirstOrDefault(rec => rec.Id == model.Id);
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

        private static SparePartViewModel CreateModel(SparePart sparePart)
        {
            return new SparePartViewModel
            {
                Id = sparePart.Id,
                SparePartName = sparePart.SparePartName,
                FactoryNumber = sparePart.FactoryNumber,
                Price = sparePart.Price,
                Status = sparePart.Status.ToString(),
                UMeasurement = sparePart.UMeasurement.ToString(),
            };
        }

        private static SparePart CreateModel(SparePartBindingModel model, SparePart sparePart)
        {
            sparePart.SparePartName = model.SparePartName;
            sparePart.Price = model.Price;
            sparePart.FactoryNumber = model.FactoryNumber;
            sparePart.Status = model.Status;
            sparePart.UMeasurement = model.UMeasurement;
            return sparePart;
        }
    }
}
