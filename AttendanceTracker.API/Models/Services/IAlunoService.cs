using AttendanceTracker.Models.DTOs;

namespace AttendanceTracker.Models.Services;

public interface IAlunoService
{
    Task<AlunoDTO?> RegistrarAsync(AlunoDTO aluno);
    Task<IEnumerable<AlunoDTO>> RetornarTodosAsync();
    Task<int> RetornarTotalAsync();
    Task<AlunoDTO?> RetornarPorIdAsync(int id);
}

