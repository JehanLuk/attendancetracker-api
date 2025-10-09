/* using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace AttendanceTracker.API.Features.Professores
{
    [Authorize(Roles = "Admin")]
    public class ProfesorHub : Hub
    {
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
}*/