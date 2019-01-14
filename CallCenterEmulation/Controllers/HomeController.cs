using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallCenterEmulation.Models;
using CallCenterEmulation.Hubs;
using CallCenterEmulation.Constants;
using CallCenterEmulation.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace CallCenterEmulation.Controllers
{
    public class HomeController : Controller
    {
        private readonly CallCenterContext _db;

        public HomeController(CallCenterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var operators = _db.Operators
                .Include(o =>o.Status)
                .Include(o => o.Type)
                .Where(x => x.Id > 0)
                .ToList();

            return View(operators);
        }

        public async Task<ActionResult> StartEmulation()
        {
            Random r = new Random();
            var operators = _db.Operators.Where(x => x.Id > 0);
            var id = (r.Next(1, 100));

            for(int i = 0; i < 5; i++)
            {
                var call = new Call() { Id = id, Length = 5 };
            }

            return null;
        }

        public ActionResult StopEmulation()
        {
            var ops = _db.Operators;
            foreach(var o in ops)
            {
                o.StatusId = 2;
            }
            _db.SaveChanges();

           return RedirectToAction("Index");
        }

        public ActionResult Operator()
        {
            var operatorModel = _db.Operators.Where(x=>x.Id == 1).Include(o=>o.Status).FirstOrDefault();
            return View(operatorModel);
        }

        public ActionResult Manager()
        {
            var managerModel = _db.Operators.Where(x => x.Id == 2).Include(o => o.Status).FirstOrDefault();
            return View(managerModel);
        }

        public ActionResult SeniorManager()
        {
            var seniorModel = _db.Operators.Where(x => x.Id == 3).Include(o => o.Status).FirstOrDefault();
            return View(seniorModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
