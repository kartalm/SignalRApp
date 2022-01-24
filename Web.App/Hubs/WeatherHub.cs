using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Web.App.Hubs
{
    public class WeatherHub : Hub
    {
        public async Task BroadcastFromClient(string message)
        {
            await Clients.All.SendAsync("Broadcast", message);
        }
    }
}
