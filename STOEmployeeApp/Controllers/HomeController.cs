using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STOEmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            ViewBag.TOs =
            APIClient.GetRequest<List<TOViewModel>>("api/main/getcarlist");
            return View();
        }

        [HttpPost]
        public void Create(int to, int car, decimal sum, Dictionary<int, (string, int)> list)
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
                TOWorks = list.ToDictionary(rec => rec.Key, rec=> (rec.Value.Item1, rec.Value.Item2))
            });
            Response.Redirect("Index");
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