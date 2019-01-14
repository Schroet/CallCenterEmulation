using CallCenterEmulation.Data;
using CallCenterEmulation.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CallCenterEmulation.CallThread
{
    public class ManageCallThread
    {
        //private readonly CallCenterContext _db;

        //public ManageCallThread(CallCenterContext db)
        //{
        //    _db = db;
        //}


        public int Id { get; set; }
        public bool IsBusy { get; private set; }
        private DateTime stop;

        public void Activate()
        {
            Console.WriteLine($"Operator {Id} is activated");
            for (; ; )
            {
                if (IsBusy && stop < DateTime.Now)
                {
                    IsBusy = false;
                    Console.WriteLine($"Operator {Id} is free");
                }
                Thread.Sleep(100);
            }
        }
        public async Task<int> Answer(int duration, int employeeId)
        {
            IsBusy = true;
            stop = DateTime.Now.AddSeconds(duration);
            Thread.Sleep(duration * 1000);
            Console.WriteLine($"Operator {Id} is busy, duration: {duration}");

            return await Task.FromResult(employeeId);
        }

        //public void AnswerToCall(int busyOperatorId, int callLength)
        //{
        //    IsBusy = true;
        //    stop = DateTime.Now.AddSeconds(callLength);
        //    var freeingOperator = _db.Operators.FirstOrDefault(x => x.Id == busyOperatorId);
        //    freeingOperator.StatusId = 2;
        //    _db.SaveChanges();
        //}

    }
}
