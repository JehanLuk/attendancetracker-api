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
        var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
        
        if (alunos.Any(a => a.Nome.Equals(aluno.Nome, StringComparison.OrdinalIgnoreCase)) 
            && alunos.Any(a => a.Matricula.Equals(aluno.Matricula, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Aluno já registrado com esse nome.");

        var resultado = new AlunoDTO
        {
            Id = _idCounter++,
            Nome = aluno.Nome,
            Matricula = aluno.Matricula,
            Verificado = aluno.Verificado,
            DataHora = DateTime.Now
        };

        await Task.CompletedTask;

        alunos.Add(resultado);
        _cache.Set(CacheKey, alunos, TimeSpan.FromHours(1));

        return Ok(resultado);
    }

    [HttpGet("listar")]
    public IActionResult ListarAlunos()
    {
        var alunos = _cache.Get<List<AlunoDTO>>(CacheKey) ?? new List<AlunoDTO>();
        return Ok(alunos);
    }
}
