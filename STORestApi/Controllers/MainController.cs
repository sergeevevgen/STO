using Microsoft.AspNetCore.Mvc;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.ViewModels;

namespace STORestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ITOLogic _tO;
        private readonly ICarLogic _car;
        public MainController(ITOLogic tO, ICarLogic car)
        {
            _car = car;
            _tO = tO;
        }

        [HttpGet]
        public List<CarViewModel> GetCarList() => _car
            .Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _car
            .Read(new CarBindingModel { Id = carId })?[0];

        [HttpGet]
        public List<TOViewModel> GetTOs(int employeeId) => _tO
            .Read(new TOBindingModel { EmployeeId = employeeId });

        [HttpPost]
        public void CreateTO(CreateTOBindingModel model) => _tO
            .CreateTO(model);
    }
}
