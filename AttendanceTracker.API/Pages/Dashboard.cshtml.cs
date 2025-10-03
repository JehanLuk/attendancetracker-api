using AttendanceTracker.Models.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

public class DashboardModel : PageModel
{
    private readonly IAlunoService _alunoService;

    public DashboardModel(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    public async Task OnGetAsync()
    {
        var alunos = await _alunoService.RetornarTodosAsync();
        ViewData["TotalAlunos"] = alunos.Count();
        //ViewData["ValorPresentes"] = alunos.Count(a => a.Tipo == "Presente");
        //ViewData["ValorDispensados"] = alunos.Count(a => a.Tipo == "Dispensado");
        //ViewData["ValorFaltas"] = alunos.Count(a => a.Tipo == "Falta");
        //ViewData["ValorAlunoExtra"] = alunos.Count(a => a.Tipo == "Extra");
    }
}
