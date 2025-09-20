using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AttedanceTracker.DTOs;

namespace AttedanceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "Alunos";
    private static int _idCounter = 1;

    public AlunoController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpPost("verificar")]
    public async Task<IActionResult> verificarAlunoAsync([FromBody] AlunoDTO aluno)
    {
        var resultado = new AlunoDTO
        {
            Id = _idCounter++,
            Nome = aluno.Nome,
            Matricula = aluno.Matricula,
            Verificado = true,
            DataHora = DateTime.Now
        };

        await Task.CompletedTask;

        // Recupera a lista do cache ou cria uma nova
        var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
        alunos.Add(resultado);
        _cache.Set(CacheKey, alunos, TimeSpan.FromHours(1));

        return Ok(resultado);
    }

    // Endpoint para listar todos os alunos do cache
    [HttpGet("listar")]
    public IActionResult ListarAlunos()
    {
        var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
        return Ok(alunos);
    }
}
