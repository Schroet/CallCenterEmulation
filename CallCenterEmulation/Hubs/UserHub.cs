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
using CallCenterEmulation.CallThread;

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

        ManageCallThread callManageThread = new ManageCallThread();

        public void SendCall(Call call)
        {
            Random random = new Random();
            var operators = _db.Operators.Where(x => x.Id > 0);
            var calls = GenerateCalls(call.Length);
            var threads = new List<Thread>();

            foreach (var @operator in operators)
            {
                var thread = new Thread(() =>
                {
                    @operator.Activate();
                });
                threads.Add(thread);
                thread.Start();
            }

            Task.Run(() =>
            {
                for (var idx = 0; idx < 100; ++idx)
                {
                    if (operators.All(_ => _.StatusId == 1))
                    {
                        Console.WriteLine("All operators are busy");
                    }
                    else
                    {
                        var @operator = operators.FirstOrDefault(_ => _.StatusId == 2);

                        if (@operator == null)
                        {
                            Console.WriteLine("All operators are busy");
                        }
                        else
                        {
                            @operator.StatusId = 1;
                            _db.SaveChanges();
                            var activeCall = calls.Where(x => x.IsActive == true).FirstOrDefault();
                            activeCall.ManagingByUserId = @operator.Id;                           
                            @operator.Answer(activeCall.Length);
                            activeCall.IsActive = false;                            
                            Clients.All.SendAsync("SendCall", activeCall);

                            @operator.StatusId = 2;
                            _db.SaveChanges();
                        }
                    }

                    Thread.Sleep(random.Next(1000, 5000));
                }
            });

            Console.ReadLine();
        }

        public List<Call> GenerateCalls(int callLength)
        {
            Random r = new Random();
            List<Call> calls = new List<Call>();
            int Idup = 1;

            for (int i = 0; i < 25; i++)
            {
                var id = (r.Next(1, 100));
                var callGenerated = new Call() { Id = Idup++, Length = callLength, IsActive = true };
                calls.Add(callGenerated);
                Clients.All.SendAsync("SendCall", callGenerated);        
            }

            return calls;
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

        public void EndCall(int employeeId)
        {
           var employeeEndCall = _db.Operators.Where(x => x.Id == employeeId).FirstOrDefault();
           employeeEndCall.StatusId = 2;
            _db.SaveChanges();
        }


        public async void SolveCallsInQueue(List<ManageCallThread> callManageThreads)
        {
            foreach (Call lastCall in _CallBuffer)
            {
                if (lastCall != null)
                {
                    var freeOperator = _db.Operators.Include(o => o.Status).Where(x => x.StatusId == 2).FirstOrDefault();
                    if (freeOperator != null)
                    {
                        lastCall.ManagingByUserId = freeOperator.Id;
                        freeOperator.StatusId = 1;
                        _db.SaveChanges();

                        var threadId = lastCall.Id;

                        callManageThreads.Add(new ManageCallThread { Id = threadId });
                        var threads = new List<Thread>();

                        foreach (var callManage in callManageThreads)
                        {
                            var thread = new Thread(() =>
                            {
                                callManage.Activate();
                            });
                            threads.Add(thread);
                            thread.Start();
                        }

                        var freeThread = callManageThreads.Where(x => x.IsBusy != true).FirstOrDefault();

                        EndCall(await freeThread.Answer(lastCall.Length, freeOperator.Id));
                        await Clients.All.SendAsync("SendCall", lastCall);
                    }

                    else
                    {
                        Console.WriteLine("All calls handled");
                    }
                }
            }
        }


        //public async Task<int> Answer(int duration, int employeeId, Thread thread)
        //{
        //    IsBusy = true;
        //    stop = DateTime.Now.AddSeconds(duration);
        //    Thread.Sleep(duration * 1000);
        //    Console.WriteLine($"Operator {Id} is busy, duration: {duration}");

        //    return await Task.FromResult(employeeId);
        //}


    }
}
