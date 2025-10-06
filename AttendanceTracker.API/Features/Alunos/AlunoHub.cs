using Microsoft.AspNetCore.SignalR;

namespace AttendanceTracker.API.Features.Alunos
{
    public class AlunoHub : Hub
    {
        public async Task EnviarNotificacao(string mensagem)
        {
            await Clients.All.SendAsync("CacheLimpo", mensagem);
        }
    }
}
