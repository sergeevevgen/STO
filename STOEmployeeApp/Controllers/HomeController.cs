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
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<TOViewModel>>($"api/main/gettos?employeeId={Program.Employee.Id}"));
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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cars = APIClient.GetRequest<List<CarViewModel>>("api/main/getcarlist");
            ViewBag.Works = new MultiSelectList(APIClient.GetRequest<List<WorkTypeViewModel>>("api/main/getworktypelist"), "Id", "WorkName", "Hours");
            return View();
        }

        [HttpPost]
        public void Create(int car, int work, decimal sum)
        {
            if (sum == 0)
            {
                return;
            }
            APIClient.PostRequest("api/main/createto",
            new CreateTOBindingModel
            {
                EmployeeId = Program.Employee.Id,
                CarId = car,
                Sum = sum,
                TOWorks = new Dictionary<int, (string, int)> { }
            });
            Response.Redirect("Index");
        }

        public IActionResult Cars()
        {
            if (Program.Employee == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<CarViewModel>>("api/main/getcarlist"));
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
        //[HttpPost]
        //public decimal Calc(List<(int, int)> list)
        //{
        //    var works =
        //    APIClient.GetRequest<WorkViewModel>($"api/main/getworks?dishId={dish}");
        //    return count * dis.Price;
        //}

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