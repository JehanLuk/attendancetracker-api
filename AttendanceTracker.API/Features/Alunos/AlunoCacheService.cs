using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace AttendanceTracker.API.Features.Alunos
{
    public class AlunoCacheService : IAlunoService
    {
        private readonly IMemoryCache _cache;
        private readonly IHubContext<AlunoHub> _hubContext;
        private const string CacheKey = "Alunos";
        private static int _idCounter = 1;
        private static readonly object _lock = new object();

        public AlunoCacheService(IMemoryCache cache, IHubContext<AlunoHub> hubContext)
        {
            _cache = cache;
            _hubContext = hubContext;
        }

        public Task<AlunoDTO?> RegistrarAsync(AlunoDTO aluno)
        {
            var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
            AlunoDTO resultado;

            lock (_lock)
            {
                resultado = new AlunoDTO
                {
                    Id = _idCounter++,
                    Nome = aluno.Nome,
                    Matricula = aluno.Matricula,
                    Verificado = aluno.Verificado,
                    DataHoraEntrada = DateTime.Now,
                    DataHoraSaida = DateTime.Now
                };

                alunos.Add(resultado);

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
                    PostEvictionCallbacks =
                    {
                        new PostEvictionCallbackRegistration
                        {
                            EvictionCallback = async (key, value, reason, state) =>
                            {
                                lock (_lock)
                                {
                                    _idCounter = 1;
                                    Console.WriteLine("Cache expirou ou foi removido!");
                                }

                                try {
                                    await _hubContext.Clients.All.SendAsync("CacheLimpo", "Cache de alunos foi limpo!");
                                } catch (Exception ex) {
                                    Console.WriteLine("Erro ao enviar notificação SignalR: " + ex.Message);
                                }
                            }
                        }
                    }
                };

                _cache.Set(CacheKey, alunos, options);
            }

            return Task.FromResult<AlunoDTO?>(resultado);
        }

        public Task<IEnumerable<AlunoDTO>> RetornarTodosAsync()
        {
            var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
            return Task.FromResult<IEnumerable<AlunoDTO>>(alunos);
        }

        public Task<int> RetornarTotalAsync()
        {
            var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
            return Task.FromResult(alunos.Count);
        }

        public Task<AlunoDTO?> RetornarPorIdAsync(int id)
        {
            var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
            var aluno = alunos.FirstOrDefault(a => a.Id == id);
            return Task.FromResult<AlunoDTO?>(aluno);
        }
    }
}
