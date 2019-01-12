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
            await Clients.All.SendAsync("Send", call);
        }

        public async Task Send(string nick, string message)
        {
            await Clients.All.SendAsync("Send", nick, message);
        }
    }
}
