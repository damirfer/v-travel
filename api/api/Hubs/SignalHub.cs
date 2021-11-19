using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Persistence;

namespace api.Hubs
{
    public class SignalHub:Hub
    {
        private LastaContext db;

        public SignalHub(LastaContext _db)
        {
            db = _db;
        }

        public override Task OnConnectedAsync()
        {

            Clients.All.SendAsync("helloWorld");

            return base.OnConnectedAsync();
            
        }



    }
}
