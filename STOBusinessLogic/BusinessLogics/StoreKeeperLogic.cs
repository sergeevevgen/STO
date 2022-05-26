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
    public class StoreKeeperLogic : IStoreKeeperLogic
    {
        private readonly IStoreKeeperStorage _storage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;

        public StoreKeeperLogic(IStoreKeeperStorage storage)
        {
            _storage = storage;
        }

        public void CreateOrUpdate(StoreKeeperBindingModel model)
        {
            var element = _storage.GetElement(new StoreKeeperBindingModel
            {
                Login = model.Login,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть кладовщик с такой почтой");
            }

            if (!Regex.IsMatch(model.Login, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            /*if (model.Password.Length > _passwordMaxLength || model.Password.Length <
            _passwordMinLength || !Regex.IsMatch(model.Password,
            @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длинной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
            }*/

            if (model.Id.HasValue)
            {
                _storage.Update(model);
            }
            else
            {
                _storage.Insert(model);
            }
        }

        public void Delete(StoreKeeperBindingModel model)
        {
            var element = _storage.GetElement(new StoreKeeperBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storage.Delete(model);
        }

        public List<StoreKeeperViewModel> Read(StoreKeeperBindingModel model)
        {
            if (model == null)
            {
                return _storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<StoreKeeperViewModel>() { _storage.GetElement(model) };
            }

            return _storage.GetFilteredList(model);
        }
    }
}
