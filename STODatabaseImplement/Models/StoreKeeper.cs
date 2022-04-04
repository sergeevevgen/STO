using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class StoreKeeper
    {
        public int? Id { get; set; }

        public string FIO { get; set; }

        public string login { get; set; }

        public int password { get; set; }
    }
}
