using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallCenterEmulation.Models;
using CallCenterEmulation.Hubs;

namespace CallCenterEmulation.Controllers
{
    public class HomeController : Controller
    {
        //private readonly UserHub userHub;

        //public HomeController(UserHub userHub)
        //{
        //    this.userHub = userHub;
        //}

        public IActionResult Index()
        {
            List<Operator> operators = new List<Operator> {
            new Operator{ Id = 1, Name = "John", Type = Constants.EmployeeType.Operator, Status = Constants.EmployeeStatus.Free },
            new Operator{ Id = 2, Name = "Mark", Type = Constants.EmployeeType.Operator, Status = Constants.EmployeeStatus.Free }};

            return View(operators);
        }

        public async Task<ActionResult> StartEmulation()
        {
            Random r = new Random();
            UserHub userHub = new UserHub();

            //string message = "new";
            //string nick = "nick";
            //await userHub.Send(message, nick);

            var id = (r.Next(1, 100));

            for(int i = 0; i < 5; i++)
            {
                var call = new Call() { Id = id, Length = 5 };
                await userHub.SendCall(call);
            }

            return null;
        }

        public ActionResult Operator()
        {
            return View();
        }

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult SeniorManager()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
