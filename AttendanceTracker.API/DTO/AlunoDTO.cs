namespace AttedanceTracker.DTOs;

public class AlunoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Matricula { get; set; } = default!;
    public bool Verificado { get; set; } = true;
    public DateTime DataHora { get; set; }
}   
