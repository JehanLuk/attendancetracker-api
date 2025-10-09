namespace AttendanceTracker.API.Features.Professores;

public interface IProfessorService
{
    Task<IEnumerable<ProfessorDTO?>> RetornarTodosAsync();
    Task<ProfessorDTO?> RetornarPorIdAsync(int id);
    Task<ProfessorDTO?> CriarAsync(ProfessorDTO professor);
    Task<bool> AtualizarAsync(int id, ProfessorDTO professor);
    Task<bool> DeletarAsync(int id);
}
