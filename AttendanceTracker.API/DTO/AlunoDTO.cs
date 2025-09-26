namespace AttendanceTracker.DTOs;

public class AlunoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Matricula { get; set; } = default!;
    public bool Verificado { get; set; } = true;
    public DateTime DataHoraEntrada { get; set; }
    public DateTime DataHoraSaida { get; set; }
}
