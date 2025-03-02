using Microsoft.AspNetCore.SignalR;

namespace MonitoringApp.API.Hubs
{
    public class HealthStatusHub : Hub
    {
        // Bu metot, tüm bağlı istemcilere mesaj gönderecek
        public async Task SendHealthStatus(string message)
        {
            await Clients.All.SendAsync("ReceiveHealthStatus", message);
        }
    }
}
