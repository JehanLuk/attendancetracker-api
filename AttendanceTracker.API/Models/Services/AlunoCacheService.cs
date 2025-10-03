using AttendanceTracker.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace AttendanceTracker.Models.Services
{
    public class AlunoCacheService : IAlunoService
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Alunos";
        private static int _idCounter = 1;
        private static readonly object _lock = new object();

        public AlunoCacheService(IMemoryCache cache)
        {
            _cache = cache;
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
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                };

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    lock (_lock) { _idCounter = 1; }
                });

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
