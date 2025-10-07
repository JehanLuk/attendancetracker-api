namespace AttendanceTracker.API.Features.Alunos;

public class profesoresDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string email { get; set; } = default!;
    public string senha { get; set; } = default!;
    public string disciplina { get; set; } = default!;
}