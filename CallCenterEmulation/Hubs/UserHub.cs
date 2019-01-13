using CallCenterEmulation.Constants;
using CallCenterEmulation.Data;
using CallCenterEmulation.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenterEmulation.Hubs
{
    public class UserHub : Hub
    {
        private readonly CallCenterContext _db;
        
        public UserHub(CallCenterContext db)
        {
            _db = db;
        }

        Queue _CallBuffer = new Queue();

        public async Task SendCall(Call call)
        {
            Random r = new Random();
            var operators = _db.Operators.Where(x => x.Id > 0);

            //Thread thread1 = new Thread();
            
            for (int i = 0; i < 25; i++)
            {
                var id = (r.Next(1, 100));
                var callGenerated = new Call() { Id = id, Length = call.Length, IsActive = true };
                await Clients.All.SendAsync("SendCall", callGenerated);
                if (call != null)
                {
                    var freeOperator = _db.Operators.Include(o => o.Status).Where(x => x.StatusId == 2).FirstOrDefault();
                    if (freeOperator != null)
                    {
                        callGenerated.ManagingByUserId = freeOperator.Id;
                        freeOperator.StatusId = 1;
                        _db.SaveChanges();

                        //Thread t = new Thread(new ThreadStart(OperatorManageCall(freeOperator.Id, callGenerated.Length)));
                        //thread1.Start(OperatorManageCall(freeOperator.Id, callGenerated.Length));

                        OperatorManageCall(freeOperator.Id, callGenerated.Length);
                        await Clients.All.SendAsync("SendCall", callGenerated);
                    }

                    else
                    {
                        QueueLogCall(callGenerated);
                    }
                }
            }         
        }

        public void QueueLogCall(Call callGenerated)
        {
           
            try
            {
                if (callGenerated == null) return;

                _CallBuffer.Enqueue(callGenerated);
            }
            catch { }
        }


        public void OperatorManageCall(int busyOperatorId, int callLength)
        {
            Thread.Sleep(callLength * 1000);
            var freeingOperator = _db.Operators.FirstOrDefault(x => x.Id == busyOperatorId);
            freeingOperator.StatusId = 2;
            _db.SaveChanges();

             //return await Task.FromResult<bool>(true);
        }

        //public async Task Send(string nick, string message)
        //{
        //    await Clients.All.SendAsync("Send", nick, message);
        //}

    }
}
