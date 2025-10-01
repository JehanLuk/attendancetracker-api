using AttendanceTracker.Models.Services; // Crie esse serviço!
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceTracker.Models.Services;

public class DashboardModel : PageModel
{
    private readonly IAlunoService _alunoService;

    public DashboardModel(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }
    
    public void OnGet()
    {
        var alunos = _alunoService.ListarAlunos();
        ViewData["TotalAlunos"] = alunos.Count();
        // Adapte os valores abaixo conforme sua lógica
        ViewData["ValorPresentes"] = alunos.Count(a => a.Status == "Presente");
        ViewData["ValorDispensados"] = alunos.Count(a => a.Status == "Dispensado");
        ViewData["ValorFaltas"] = alunos.Count(a => a.Status == "Falta");
        ViewData["ValorAlunoExtra"] = alunos.Count(a => a.Tipo == "Extra");
    }
}