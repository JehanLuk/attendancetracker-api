using AttendanceTracker.API.Features.Alunos;
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
        ViewData["ValorPresentes"] = alunos.Count(a => a.Verificado == true);
        //ViewData["ValorDispensados"] = alunos.Count(a => a.Verificado == false);
        ViewData["ValorFaltas"] = alunos.Count(a => a.Verificado == false);
        //ViewData["ValorAlunoExtra"] = alunos.Count(a => a.Tipo == "Extra");
    }
}
