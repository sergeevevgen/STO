using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STOEmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace STOEmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Employee);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/client/updatedata",
                new EmployeeBindingModel
                {
                    Id = Program.Employee.Id,
                    FIO = fio,
                    Login = login,
                    Password = password
                });
                Program.Employee.FIO = fio;
                Program.Employee.Login = login;
                Program.Employee.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore
        = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Employee =
                APIClient.GetRequest<EmployeeViewModel>($"api/employee/login?login={login}&password={password}");

                if (Program.Employee == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/employee/register",
                new EmployeeBindingModel
                {
                    FIO = fio,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        public IActionResult TO()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<TOViewModel>>($"api/main/gettos?employeeId={Program.Employee.Id}"));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cars = APIClient.GetRequest<List<CarViewModel>>("api/main/getcarlist");
            ViewBag.Works = new MultiSelectList(APIClient.GetRequest<List<WorkTypeViewModel>>("api/main/getworktypelist"), "Id", "WorkName", "Hours");
            return View();
        }

        [HttpPost]
        public void Create(int carId,  List<int> workId)
        {
            var listworks = new List<WorkTypeViewModel>();
            foreach(var work in workId)
            {
                listworks.Add(APIClient.GetRequest<WorkTypeViewModel>($"api/main/getworktype?workId={workId}"));
            }
            if (!(carId == 0) || listworks !=  null)
            {
                APIClient.PostRequest("api/main/createto",
                new CreateTOBindingModel
                {
                    EmployeeId = Program.Employee.Id,
                    CarId = carId,
                    TOWorks = listworks.ToDictionary(rec => rec.Id, rec => (rec.WorkName, 1))
                });
                Response.Redirect("TO");
            }
            
            Response.Redirect("TO");
            return;
        }

        [HttpGet]
        public IActionResult Update(int toId)
        {
            ViewBag.Works = APIClient.GetRequest<List<WorkTypeViewModel>>($"api/main/getworktypelist");
            ViewBag.TO = APIClient.GetRequest<TOViewModel>($"api/main/getto?toId={toId}");
            return View();
        }

        [HttpPost]
        public void Update(int toId, List<int> workId, int carId)
        {
            var listworks = new List<WorkTypeViewModel>();
            var car = APIClient.GetRequest<CarViewModel>($"api/main/getcar?carId={carId}");
            foreach (var work in workId)
            {
                listworks.Add(APIClient.GetRequest<WorkTypeViewModel>($"api/main/getworktype?workId={workId}"));
            }

            if (car == null || listworks != null)
            {
                APIClient.PostRequest("api/main/updateto",
                new TOBindingModel
                {
                    Id = toId,
                    EmployeeId = Program.Employee.Id,
                    CarId = carId,
                    TOWorks = listworks.ToDictionary(rec => rec.Id, rec => (rec.WorkName, 1))
                });
                Response.Redirect("TO");
            }

            Response.Redirect("TO");
            return;
        }

        public IActionResult Cars()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<CarViewModel>>("api/main/getcarlist"));
        }

        [HttpGet]
        public IActionResult Records(int Id)
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<ServiceRecordViewModel>>($"api/main/getrecords?carId={Id}"));
        }

        [HttpGet]
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        public void CreateCar(string carBrand, string carModel, string VIN, string ownerPhoneNumber)
        {
            if (!string.IsNullOrEmpty(carBrand) && !string.IsNullOrEmpty(carModel)
            && !string.IsNullOrEmpty(VIN) && !string.IsNullOrEmpty(ownerPhoneNumber))
            {
                APIClient.PostRequest("api/main/addcar", new CarBindingModel
                {
                    CarBrand = carBrand,
                    CarModel = carModel,
                    VIN = VIN,
                    OwnerPhoneNumber = ownerPhoneNumber
                });
                Response.Redirect("Cars");
                return;
            }
            throw new Exception("Введите марку, модель, VIN и номер телефона владельца");
        }

        [HttpGet]
        public IActionResult Works()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<WorkViewModel>>("api/main/getworklist"));
        }

        [HttpGet]
        public IActionResult CreateWork()
        {
            return View();
        }

        [HttpPost]
        public void CreateWork(int workId, decimal price)
        {
            var work = APIClient.GetRequest<WorkTypeViewModel>($"api/main/getworktype?workId={workId}");
            if (work != null)
            {
                APIClient.PostRequest("api/main/creatework",
                new CreateWorkBindingModel
                {
                    StoreKeeperId = 1,
                    Count = 1,
                    Price = price,
                    NetPrice = price * 13,
                    WorkName = work.WorkName,
                    WorkTypeId = workId
                });
                Response.Redirect("Works");
            }

            Response.Redirect("Works");
            return;
        }

        [HttpPost]
        public void UpdateWork(int workId, decimal price)
        {
            var work = APIClient.GetRequest<WorkTypeViewModel>($"api/main/getworktype?workId={workId}");
            if (work != null)
            {
                APIClient.PostRequest("api/main/creatework",
                new CreateWorkBindingModel
                {
                    StoreKeeperId = 1,
                    Count = 1,
                    Price = price,
                    NetPrice = price * 13,
                    WorkName = work.WorkName,
                    WorkTypeId = workId
                });
                Response.Redirect("Works");
            }

            Response.Redirect("Works");
            return;
        }

        [HttpGet]
        public IActionResult WorkTypes()
        {
            return View(APIClient.GetRequest<List<WorkTypeViewModel>>("api/main/getworktypelist"));
        }

        [HttpGet]
        public IActionResult CreateWorkType()
        {
            ViewBag.Times = APIClient.GetRequest<List<TimeOfWorkViewModel>>($"api/main/gettimelist");
            return View();
        }

        [HttpPost]
        public void CreateWorkType(int timeId, string workName)
        {
            var time = APIClient.GetRequest<WorkTypeViewModel>($"api/main/gettime?timeId={timeId}");
            if (time != null)
            {
                APIClient.PostRequest("api/main/createworktype",
                new WorkTypeBindingModel
                {
                    WorkName = workName,
                    TimeOfWorkId = timeId                    
                });
                Response.Redirect("Works");
            }

            Response.Redirect("Works");
            return;
        }

        [HttpGet]
        public IActionResult UpdateWorkType(int worktypeId)
        {
            ViewBag.WorkType = APIClient.GetRequest<WorkTypeViewModel>($"api/main/getworktype?worktypeId={worktypeId}");
            return View();
        }

        [HttpPost]
        public void UpdateWorkType(int worktypeId, int timeId, string workName)
        {
            var time = APIClient.GetRequest<WorkTypeViewModel>($"api/main/gettime?timeId={timeId}");
            if (time != null)
            {
                APIClient.PostRequest("api/main/createworktype",
                new WorkTypeBindingModel
                {
                    Id = worktypeId,
                    WorkName = workName,
                    TimeOfWorkId = timeId
                });
                Response.Redirect("Works");
            }

            Response.Redirect("Works");
            return;
        }

        [HttpGet]
        public IActionResult SpareParts()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<SparePartViewModel>>($"api/main/getpartlist"));
        }

        [HttpGet]
        public IActionResult CreateSparePart()
        {
            return View();
        }

        [HttpPost]
        public void CreateSparePart(string partName, string factoryNum, decimal price, string status, string um)
        {
            if (!string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(factoryNum) && price != 0 && !string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(um))
            {
                APIClient.PostRequest("api/main/createpart",
                new SparePartBindingModel
                {
                    SparePartName = partName,
                    FactoryNumber = factoryNum,
                    Price = price,
                    Status = STOContracts.Enums.SparePartStatus.БУ,
                    UMeasurement = STOContracts.Enums.UnitMeasurement.шт
                });
                Response.Redirect("SpareParts");
            }

            Response.Redirect("SpareParts");
            return;
        }

        [HttpGet]
        public IActionResult UpdateSparePart(int partId)
        {
            ViewBag.Part = APIClient.GetRequest<WorkTypeViewModel>($"api/main/getpart?partId={partId}");
            return View();
        }

        [HttpPost]
        public void UpdateSparePart(int partId, string partName, string factoryNum, decimal price, string status, string um)
        {
            if (!string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(factoryNum) && price != 0 && !string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(um))
            {
                var part = APIClient.GetRequest<SparePartViewModel>($"api/main/getpart?partId={partId}");
                if (part == null)
                {
                    return;
                }
                APIClient.PostRequest("api/main/createpart",
                new SparePartBindingModel
                {
                    Id = partId,
                    SparePartName = partName,
                    FactoryNumber = factoryNum,
                    Price = price,
                    Status = STOContracts.Enums.SparePartStatus.БУ,
                    UMeasurement = STOContracts.Enums.UnitMeasurement.шт
                });
                Response.Redirect("SpareParts");
            }

            Response.Redirect("SpareParts");
            return;
        }

        [HttpGet]
        public IActionResult Time()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void Time(int hours)
        {
            if (hours > 0)
            {
                APIClient.PostRequest("api/main/createtime",
                new TimeOfWorkBindingModel
                {
                    Hours = hours
                });
                Response.Redirect("Time");
            }

            Response.Redirect("Time");
            return;
        }


        //[HttpGet]
        //public IActionResult Messages()
        //{
        //    if (Program.Client == null)
        //    {
        //        return Redirect("~/Home/Enter");
        //    }
        //    return View(APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getclientsmessages?clientId={Program.Client.Id}"));
        //}
    }
}