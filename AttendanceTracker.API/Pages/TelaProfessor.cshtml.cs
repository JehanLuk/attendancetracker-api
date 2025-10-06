using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System;

public class TelaProfessorModel : PageModel
{
    [BindProperty] public string Nome { get; set; } = string.Empty;
    [BindProperty] public string Matricula { get; set; } = string.Empty;
    public string? QrCodeBase64 { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Matricula))
        {
            string conteudo = $"Aluno: {Nome}, Matrícula: {Matricula}";

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);
            QrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
        }

        return Page();
    }
}
