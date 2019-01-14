using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenterEmulation.Models
{
    public class Operator : BaseEmployeeModel
    {





        public void Activate()
        {
            Console.WriteLine($"Operator {Id} is activated");
            for (; ; )
            {
                if (StatusId == 1 && stop < DateTime.Now)
                {
                    StatusId = 2;
                    Console.WriteLine($"Operator {Id} is free");
                }
                Thread.Sleep(100);
            }
        }
        public void Answer(int duration)
        {
            StatusId = 1;
            stop = DateTime.Now.AddSeconds(duration);
            Console.WriteLine($"Operator {Id} is busy, duration: {duration}");
        }

    }

}
