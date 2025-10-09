using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System;
using System.Threading.Tasks;
using AttendanceTracker.API.Features.Alunos;

public class TelaProfessorModel : PageModel
{
    private readonly IAlunoService _alunoService;

    public TelaProfessorModel(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [BindProperty] public string Nome { get; set; } = string.Empty;
    [BindProperty] public string Matricula { get; set; } = string.Empty;
    public string? QrCodeBase64 { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Nome) || string.IsNullOrWhiteSpace(Matricula))
            return Page();

        await Task.CompletedTask;

        string conteudo = $"Aluno: {Nome}, Matrícula: {Matricula}";
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        QrCodeBase64 = Convert.ToBase64String(qrCode.GetGraphic(10));

        return Page();
    }
}
