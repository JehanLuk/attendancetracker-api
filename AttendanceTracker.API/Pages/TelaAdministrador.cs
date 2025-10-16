using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TelaAdministrador : PageModel
{
    // Propriedades para estatísticas
    public int NumeroDeUsuarios { get; set; }
    public int UsuariosCadastrados { get; set; }
    public int UsuariosAtivos { get; set; }
    public int UsuariosInativos { get; set; }
    
    // Listas
    public List<Usuario> UsuariosRecentes { get; set; }
    public List<Relatorio> RelatoriosDeAtividade { get; set; }
    public List<Usuario> ResultadosDaBusca { get; set; }
    
    // Propriedades para gráficos e métricas
    public Dictionary<string, int> MetricasSistema { get; set; }
    public List<UsoSistema> DadosUsoSistema { get; set; }
    
    // Propriedade para busca
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        await CarregarDadosAsync();
        
        if (!string.IsNullOrEmpty(SearchTerm))
        {
            await ProcessarBuscaAsync();
        }
    }

    private async Task CarregarDadosAsync()
    {
        // Simular carga assíncrona de dados
        await Task.Run(() =>
        {
            // Estatísticas básicas
            NumeroDeUsuarios = 324;
            UsuariosCadastrados = 318;
            UsuariosAtivos = 295;
            UsuariosInativos = NumeroDeUsuarios - UsuariosAtivos;

            // Usuários recentes com dados mais realistas
            UsuariosRecentes = new List<Usuario>
            {
                new Usuario { 
                    Nome = "João Silva", 
                    Email = "joao.silva@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-1), 
                    Ativo = true,
                    Perfil = "Usuário"
                },
                new Usuario { 
                    Nome = "Maria Santos", 
                    Email = "maria.santos@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-2), 
                    Ativo = true,
                    Perfil = "Administrador"
                },
                new Usuario { 
                    Nome = "Pedro Oliveira", 
                    Email = "pedro.oliveira@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-3), 
                    Ativo = true,
                    Perfil = "Usuário"
                },
                new Usuario { 
                    Nome = "Ana Costa", 
                    Email = "ana.costa@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-4), 
                    Ativo = false,
                    Perfil = "Usuário"
                },
                new Usuario { 
                    Nome = "Carlos Lima", 
                    Email = "carlos.lima@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-5), 
                    Ativo = true,
                    Perfil = "Moderador"
                },
                new Usuario { 
                    Nome = "Fernanda Rocha", 
                    Email = "fernanda.rocha@email.com",
                    DataDeCadastro = DateTime.Now.AddDays(-6), 
                    Ativo = true,
                    Perfil = "Usuário"
                }
            };

            // Relatórios de atividade
            RelatoriosDeAtividade = new List<Relatorio>
            {
                new Relatorio { 
                    Titulo = "Relatório Mensal - Janeiro 2024", 
                    Data = DateTime.Now.AddDays(-10),
                    Tipo = "Mensal",
                    Descricao = "Relatório completo das atividades do mês"
                },
                new Relatorio { 
                    Titulo = "Análise de Performance do Sistema", 
                    Data = DateTime.Now.AddDays(-15),
                    Tipo = "Performance",
                    Descricao = "Análise detalhada da performance do sistema"
                },
                new Relatorio { 
                    Titulo = "Auditoria de Segurança", 
                    Data = DateTime.Now.AddDays(-20),
                    Tipo = "Segurança",
                    Descricao = "Relatório de auditoria de segurança"
                },
                new Relatorio { 
                    Titulo = "Relatório de Usuários Ativos", 
                    Data = DateTime.Now.AddDays(-25),
                    Tipo = "Usuários",
                    Descricao = "Análise do comportamento dos usuários ativos"
                },
                new Relatorio { 
                    Titulo = "Backup e Recuperação", 
                    Data = DateTime.Now.AddDays(-30),
                    Tipo = "Manutenção",
                    Descricao = "Relatório de procedimentos de backup"
                }
            };

            // Métricas do sistema para gráficos
            MetricasSistema = new Dictionary<string, int>
            {
                { "Logins Hoje", 142 },
                { "Novos Usuários", 8 },
                { "Tickets Abertos", 23 },
                { "Alertas Sistema", 2 }
            };

            // Dados para gráfico de uso
            DadosUsoSistema = new List<UsoSistema>
            {
                new UsoSistema { Dia = "Seg", Acessos = 245, UsuariosUnicos = 189 },
                new UsoSistema { Dia = "Ter", Acessos = 312, UsuariosUnicos = 234 },
                new UsoSistema { Dia = "Qua", Acessos = 278, UsuariosUnicos = 210 },
                new UsoSistema { Dia = "Qui", Acessos = 345, UsuariosUnicos = 267 },
                new UsoSistema { Dia = "Sex", Acessos = 298, UsuariosUnicos = 245 },
                new UsoSistema { Dia = "Sáb", Acessos = 187, UsuariosUnicos = 156 },
                new UsoSistema { Dia = "Dom", Acessos = 165, UsuariosUnicos = 142 }
            };
        });
    }

    private async Task ProcessarBuscaAsync()
    {
        await Task.Run(() =>
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Simular busca em uma lista maior de usuários
                var todosUsuarios = new List<Usuario>
                {
                    new Usuario { Nome = "João Silva", DataDeCadastro = DateTime.Now.AddDays(-1) },
                    new Usuario { Nome = "Maria Santos", DataDeCadastro = DateTime.Now.AddDays(-2) },
                    new Usuario { Nome = "Pedro Oliveira", DataDeCadastro = DateTime.Now.AddDays(-3) },
                    new Usuario { Nome = "Ana Costa", DataDeCadastro = DateTime.Now.AddDays(-4) },
                    new Usuario { Nome = "Carlos Lima", DataDeCadastro = DateTime.Now.AddDays(-5) },
                    new Usuario { Nome = "Fernanda Rocha", DataDeCadastro = DateTime.Now.AddDays(-6) },
                    new Usuario { Nome = "Roberto Alves", DataDeCadastro = DateTime.Now.AddDays(-7) },
                    new Usuario { Nome = "Juliana Mendes", DataDeCadastro = DateTime.Now.AddDays(-8) }
                };

                ResultadosDaBusca = todosUsuarios
                    .Where(u => u.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                               u.Email?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
            }
        });
    }

    // Método para ações administrativas
    public IActionResult OnPostExportarRelatorio()
    {
        // Lógica para exportar relatório
        // Retornar arquivo ou redirecionamento
        return Page();
    }

    public async Task<IActionResult> OnPostAtualizarDadosAsync()
    {
        await CarregarDadosAsync();
        return Page();
    }
}

// Classes auxiliares melhoradas
public class Usuario
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataDeCadastro { get; set; }
    public bool Ativo { get; set; }
    public string Perfil { get; set; }
    public string Telefone { get; set; }
    public DateTime? UltimoAcesso { get; set; }
}

public class Relatorio
{
    public string Titulo { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; }
    public string Tipo { get; set; }
    public string Status { get; set; } = "Concluído";
    public int QuantidadeItens { get; set; }
}

public class UsoSistema
{
    public string Dia { get; set; }
    public int Acessos { get; set; }
    public int UsuariosUnicos { get; set; }
}