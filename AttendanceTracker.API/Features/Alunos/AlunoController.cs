using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AttendanceTracker.API.Features.Alunos;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpPost("verificar")]
    public async Task<IActionResult> RegistrarAsync([FromBody] AlunoDTO aluno)
    {
        if (!Regex.IsMatch(aluno.Matricula, @"^\d{14}$"))
            return BadRequest("Matrícula inválida. Deve conter exatamente 14 dígitos numéricos.");

        var alunos = await _alunoService.RetornarTodosAsync();
        if (alunos.Any(a => a.Matricula.Equals(aluno.Matricula, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Aluno já registrado com essa matrícula.");

        var resultado = await _alunoService.RegistrarAsync(aluno);
        return Ok(resultado);
    }

    [HttpGet("listar")]
    public async Task<IActionResult> ListarAlunos()
    {
        var alunos = await _alunoService.RetornarTodosAsync();
        return Ok(alunos);
    }

    [HttpGet("total")]
    public async Task<IActionResult> RetornarTotalAsync()
    {
        var total = await _alunoService.RetornarTotalAsync();
        return Ok(new { total });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RetornarPorIdAsync(int id)
    {
        var aluno = await _alunoService.RetornarPorIdAsync(id);
        if (aluno == null)
            return NotFound(new { message = "Aluno não encontrado." });

        return Ok(aluno);
    }
}
