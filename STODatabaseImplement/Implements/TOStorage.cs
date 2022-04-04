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
        public List<TOViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.TOs
            .Include(rec => rec.DepositClients)
            .ThenInclude(rec => rec.Client)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

    }
}
