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
    public class EmployeeStorage : IEmployeeStorage
    {
        public void Delete(EmployeeBindingModel model)
        {
            using var context = new STODatabase();
            Employee employee = context.Employees.FirstOrDefault(rec=> rec.Id == model.Id);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public EmployeeViewModel GetElement(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new STODatabase();
            var employee = context.Employees
                .Include(rec => rec.STOs)
                .FirstOrDefault(rec => rec.Id == model.Id 
                || rec.Login == model.Login);
            return employee != null ? CreateModel(employee) : null;

        }

        public List<EmployeeViewModel> GetFilteredList(EmployeeBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeViewModel> GetFullList()
        {
            throw new NotImplementedException();
        }

        public void Insert(EmployeeBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(EmployeeBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
