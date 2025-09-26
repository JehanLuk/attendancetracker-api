using Microsoft.AspNetCore.Mvc.RazorPages;
using AttedanceTracker.DTOs;
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
            // Ajuste a URL para o endpoint correto que retorna todos os alunos
            var response = await client.GetAsync("http://0.0.0.0:5000");
            if (response.IsSuccessStatusCode)
            {
                var alunos = await response.Content.ReadFromJsonAsync<List<AlunoDTO>>();
                if (alunos != null)
                    Alunos = alunos;
                StatusConexao = "✅ Conectado à API de Detecção";
            }
            else
            {
                StatusConexao = $"⚠️ Erro ({(int)response.StatusCode})";
            }
        }
        catch
        {
            StatusConexao = "❌ Falha na conexão com a API";
        }
    }
}