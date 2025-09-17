using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AttedanceTracker.DTOs;

namespace AttedanceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IMemoryCache _cache;

    public AlunoController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpPost("verificar")]
    public async Task<IActionResult> verificarAlunoAsync([FromBody] AlunoDTO aluno)
    {
        var resultado = new AlunoDTO
        {
            Nome = aluno.Nome,
            Matricula = aluno.Matricula,
            Verificado = true,
            DataHora = DateTime.Now
        };

        await Task.CompletedTask;

        if (aluno.Matricula != null)
            _cache.Set(aluno.Matricula, resultado, TimeSpan.FromHours(1));
        return Ok(resultado);
    }
}
