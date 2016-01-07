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