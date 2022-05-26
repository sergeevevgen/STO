using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StorageContracts;
using STOContracts.ViewModels;
using System.Text.RegularExpressions;

namespace STOBusinessLogic.BusinessLogics
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeStorage _employeeStorage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;

        public EmployeeLogic(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void CreateOrUpdate(EmployeeBindingModel model)
        {
            var element = _employeeStorage.GetElement(new EmployeeBindingModel
            {
                Login = model.Login
            });
            
            if(element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такой работник");
            }

            if (!Regex.IsMatch(model.Login, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }

            //if (model.Password.Length > _passwordMaxLength || model.Password.Length <
            //_passwordMinLength || !Regex.IsMatch(model.Password,
            //@"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            //{
            //    throw new Exception($"Пароль длинной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
            //}


            if (model.Id.HasValue)
            {
                _employeeStorage.Update(model);
            }
            else
            {
                _employeeStorage.Insert(model);
            }
        }

        public void Delete(EmployeeBindingModel model)
        {
            var element = _employeeStorage.GetElement(new EmployeeBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _employeeStorage.Delete(model);
        }

        public List<EmployeeViewModel> Read(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return _employeeStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<EmployeeViewModel>() { _employeeStorage.GetElement(model) };
            }

            return _employeeStorage.GetFilteredList(model);
        }
    }
}
