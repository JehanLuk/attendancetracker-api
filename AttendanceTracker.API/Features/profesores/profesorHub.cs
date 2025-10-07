using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AttendanceTracker.API.Features.Profesores
{
    public class ProfesorHub : Hub
    {
        // Envia uma mensagem para todos os clientes conectados
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Notifica quando um professor entra no hub
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ProfesorConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        // Notifica quando um professor sai do hub
        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.All.SendAsync("ProfesorDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}