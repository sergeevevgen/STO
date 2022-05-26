using Microsoft.AspNetCore.Mvc;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.ViewModels;

namespace STORestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLogic _logic;
        //private readonly IMessageInfoLogic _messageLogic;
        public EmployeeController(IEmployeeLogic logic/*, IMessageInfoLogic messageInfoLogic*/)
        {
            _logic = logic;
            //_messageLogic = messageInfoLogic;
        }

        [HttpGet]
        public EmployeeViewModel Login(string login, string password)
        {
            var list = _logic.Read(new EmployeeBindingModel
            {
                Login = login,
                Password = password,
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(EmployeeBindingModel model) =>
        _logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(EmployeeBindingModel model) =>
        _logic.CreateOrUpdate(model);

        /*[HttpGet]
        public List<MessageInfoViewModel> GetClientsMessages(int clientid) => _messageLogic.Read(new MessageInfoBindingModel { ClientId = clientid });*/
    }
}
