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
    public class SparePartStorage : ISparePartStorage
    {
        public List<SparePartViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.SpareParts
            .Include(rec => rec.CarSpareParts)
            .ThenInclude(rec => rec.LoanProgram)
            .Include(rec => rec.DepositCurrencies)
            .ThenInclude(rec => rec.Deposit)
            .Include(rec => rec.Manager)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }


    }
}
