﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    public class EmployeeBindingModel
    {
        public int? Id { get; set; }

        public string FIO { get; set; }

        public string login { get; set; }

        public int password { get; set; }
    }
}
