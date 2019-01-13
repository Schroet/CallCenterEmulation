using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallCenterEmulation.Models;
using CallCenterEmulation.Hubs;
using CallCenterEmulation.Constants;

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
            OperatorsList list = new OperatorsList();
            return View(list.operators);
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
