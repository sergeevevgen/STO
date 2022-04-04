﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.StorageContracts
{
    public interface IServiceRecordStorage
    {
        List<ServiceRecordViewModel> GetFullList();
        List<ServiceRecordViewModel> GetFilteredList(ServiceRecordBindingModel model);

        ServiceRecordViewModel GetElement(ServiceRecordBindingModel model);

        void Insert(ServiceRecordBindingModel model);

        void Update(ServiceRecordBindingModel model);

        void Delete(ServiceRecordBindingModel model);
    }
}
