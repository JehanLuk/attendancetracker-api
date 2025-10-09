namespace AttendanceTracker.API.Features.Professores;

public class ProfessorDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public string Disciplina { get; set; } = default!;
}