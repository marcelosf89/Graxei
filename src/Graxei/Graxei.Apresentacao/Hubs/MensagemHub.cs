using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Graxei.Apresentacao.Hubs
{
    public class MensagemHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}