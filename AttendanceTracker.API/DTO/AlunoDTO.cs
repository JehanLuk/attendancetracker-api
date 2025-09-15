namespace AttedanceTracker.DTOs;

public class AlunoDTO 
{
    public string Nome { get; set; }
    public string Matricula { get; set; }
    public bool Verificado { get; set; } = true;
    public DateTime DataHora { get; set; }
}
