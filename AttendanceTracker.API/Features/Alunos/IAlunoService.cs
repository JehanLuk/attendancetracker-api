namespace AttendanceTracker.API.Features.Alunos;

public interface IAlunoService
{
    Task<AlunoDTO?> RegistrarAsync(AlunoDTO aluno);
    Task<IEnumerable<AlunoDTO>> RetornarTodosAsync();
    Task<int> RetornarTotalAsync();
    Task<AlunoDTO?> RetornarPorIdAsync(int id);
}

