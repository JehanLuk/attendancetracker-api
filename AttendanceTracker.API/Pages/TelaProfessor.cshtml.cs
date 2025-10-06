using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceTracker.API.Features.Alunos;
using QRCoder;
using System;

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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Matricula))
        {
            var aluno = new AlunoDTO { Nome = Nome, Matricula = Matricula };
            var resultado = await _alunoService.RegistrarAsync(aluno);

            string conteudo = $"Aluno: {resultado!.Nome}, Matrícula: {resultado.Matricula}";
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            QrCodeBase64 = Convert.ToBase64String(qrCode.GetGraphic(20));
        }

        return Page();
    }
}
