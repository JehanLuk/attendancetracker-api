using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string StatusConexao { get; set; } = "⏳ Verificando...";

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync("http://localhost:5132");

            if (response.IsSuccessStatusCode)
                StatusConexao = "✅ Conectado à API de Detecção";
            else
                StatusConexao = $"⚠️ Erro ({(int)response.StatusCode})";
        }
        catch
        {
            StatusConexao = "❌ Falha na conexão com a API";
        }
    }
}