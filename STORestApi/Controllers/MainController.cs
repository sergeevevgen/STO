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
        private readonly ITimeOfWorkLogic _time;
        private readonly ISparePartLogic _part;
        public MainController(ITOLogic tO, ICarLogic car, IWorkLogic work, IServiceRecordLogic service, ITimeOfWorkLogic time, ISparePartLogic part)
        {
            _car = car;
            _tO = tO;
            _work = work;
            _service = service;
            _time = time;
            _part = part;
        }

        [HttpGet]
        public List<CarViewModel> GetCarList() => _car
            .Read(null);

        [HttpGet]
        public CarViewModel GetCar(int carId) => _car
            .Read(new CarBindingModel { Id = carId })?[0];

        [HttpGet]
        public List<TOViewModel> GetTOs(int employeeId) => _tO
            .Read(new TOBindingModel { EmployeeId = employeeId });

        [HttpGet]
        public List<WorkTypeViewModel> GetWorkTypeList() => _work.ReadType(null)?.ToList();

        [HttpGet]
        public WorkTypeViewModel GetWorkType(int workid) => _work.ReadType(new WorkTypeBindingModel { Id = workid })?[0];

        [HttpGet]
        public decimal GetWork(int workId) => _work.Read(new WorkBindingModel { Id = workId })[0].NetPrice;

        [HttpGet]
        public List<ServiceRecordViewModel> GetRecords(int carId) => _service.Read(new ServiceRecordBindingModel { CarId = carId });

        [HttpGet]
        public TOViewModel GetTO(int toid) => _tO.Read(new TOBindingModel { Id = toid })?[0];

        [HttpPost]
        public void UpdateTO(TOBindingModel model) => _tO.Update(model);

        [HttpPost]
        public void CreateTO(CreateTOBindingModel model) => _tO
            .CreateTO(model);
        [HttpPost]
        public void AddCar(CarBindingModel model) => _car.CreateOrUpdate(model);

        [HttpGet]
        public List<WorkViewModel> GetWorkList() => _work.Read(null);

        [HttpPost]
        public void CreateWork(CreateWorkBindingModel model) => _work.CreateWork(model);

        [HttpGet]
        public List<TimeOfWorkViewModel> GetTimeList() => _time.Read(null);

        [HttpGet]
        public TimeOfWorkViewModel GetTime(int timeId) => _time.Read(new TimeOfWorkBindingModel { Id = timeId })?[0];

        [HttpPost]
        public void CreateWorkType(WorkTypeBindingModel model) => _work.CreateOrUpdate(model);

        [HttpGet]
        public List<SparePartViewModel> GetPartList() => _part.Read(null);

        [HttpPost]
        public void CreatePart(SparePartBindingModel model) => _part.CreateOrUpdate(model);

        [HttpGet]
        public SparePartViewModel GetPart(int partId) => _part.Read(new SparePartBindingModel { Id = partId })?[0];

        [HttpPost]
        public void CreateTime(TimeOfWorkBindingModel model) => _time.CreateOrUpdate(model);
    }
}
