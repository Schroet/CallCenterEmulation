using CallCenterEmulation.Constants;
using CallCenterEmulation.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterEmulation.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendCall(Call call)
        {
            await Clients.All.SendAsync("SendCall", call);

            if(call != null)
            {
                OperatorsList list = new OperatorsList();
                var freeOperator = list.operators.Where(x => x.Status == Constants.EmployeeStatus.Free).FirstOrDefault();
                if(freeOperator != null)
                {
                    call.ManagingByUserId = freeOperator.Id;
                    freeOperator.Status = Constants.EmployeeStatus.Busy;
                }
            }
        }

        public async Task Send(string nick, string message)
        {
            await Clients.All.SendAsync("Send", nick, message);
        }
    }
}
