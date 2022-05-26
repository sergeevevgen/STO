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
        private readonly IWorkLogic _work;
        private readonly IServiceRecordLogic _service;
        public MainController(ITOLogic tO, ICarLogic car, IWorkLogic work, IServiceRecordLogic service)
        {
            _car = car;
            _tO = tO;
            _work = work;
            _service = service;
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

        [HttpGet]
        public List<WorkTypeViewModel> GetWorkTypeList() => _work.ReadType(null)?.ToList();

        [HttpGet]
        public List<ServiceRecordViewModel> GetRecords(int car) => _service.Read(new ServiceRecordBindingModel { CarId = car });

        [HttpPost]
        public void CreateTO(CreateTOBindingModel model) => _tO
            .CreateTO(model);
    }
}
