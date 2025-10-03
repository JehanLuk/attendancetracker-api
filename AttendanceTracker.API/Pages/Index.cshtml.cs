using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceTracker.Models.DTOs;
using AttendanceTracker.Models.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<AlunoDTO> Alunos { get; set; } = new List<AlunoDTO>();
    public string StatusConexao { get; set; } = "⏳ Verificando...";

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();
        try
        {
            var response = await client.GetAsync("http://localhost:5000/api/aluno/listar");
            if (response.IsSuccessStatusCode)
            {
                var alunos = await response.Content.ReadFromJsonAsync<List<AlunoDTO>>();
                if (alunos != null)
                    Alunos = alunos;
                StatusConexao = "✅ Conectado ao serviço de detecção";
            }
            else
            {
                StatusConexao = $"⚠️ Erro ({(int)response.StatusCode})";
            }
        }
        catch
        {
            StatusConexao = "❌ Falha na conexão com o serviço de detecção";
        }
    }
}
