// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Security.Claims;

// namespace AttendanceTracker.API.Features.Administradores
// {
//     [Authorize(Roles = "Administrador")]
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AdministradoresController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly ILogger<AdministradoresController> _logger;

//         public AdministradoresController(ApplicationDbContext context, ILogger<AdministradoresController> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         // GET: /api/administradores/dashboard
//         [HttpGet("dashboard")]
//         public async Task<IActionResult> Dashboard()
//         {
//             try
//             {
//                 var administradorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                 // Buscar dados do banco de dados
//                 var totalUsuarios = await _context.Usuarios.CountAsync();
//                 var usuariosAtivos = await _context.Usuarios.CountAsync(u => u.Ativo);
//                 var usuariosInativos = totalUsuarios - usuariosAtivos;
                
//                 var usuariosRecentes = await _context.Usuarios
//                     .OrderByDescending(u => u.DataCadastro)
//                     .Take(5)
//                     .Select(u => new
//                     {
//                         u.Id,
//                         u.Nome,
//                         u.Email,
//                         DataDeCadastro = u.DataCadastro,
//                         u.Ativo,
//                         u.Perfil
//                     })
//                     .ToListAsync();

//                 var relatoriosRecentes = await _context.Relatorios
//                     .OrderByDescending(r => r.DataCriacao)
//                     .Take(5)
//                     .Select(r => new
//                     {
//                         r.Id,
//                         r.Titulo,
//                         Data = r.DataCriacao,
//                         r.Tipo,
//                         r.Descricao
//                     })
//                     .ToListAsync();

//                 // Métricas do sistema
//                 var loginsHoje = await _context.LogsAcesso
//                     .CountAsync(l => l.DataAcesso.Date == DateTime.Today);

//                 var novosUsuarios = await _context.Usuarios
//                     .CountAsync(u => u.DataCadastro.Date == DateTime.Today);

//                 var ticketsAbertos = await _context.Tickets
//                     .CountAsync(t => t.Status == StatusTicket.Aberto);

//                 var alertasSistema = await _context.Alertas
//                     .CountAsync(a => !a.Resolvido);

//                 var dadosUsoSistema = await ObterDadosUsoSistema();

//                 var dashboardInfo = new
//                 {
//                     Nome = User.Identity.Name,
//                     Email = User.FindFirstValue(ClaimTypes.Email),
//                     NumeroDeUsuarios = totalUsuarios,
//                     UsuariosCadastrados = totalUsuarios,
//                     UsuariosAtivos = usuariosAtivos,
//                     UsuariosInativos = usuariosInativos,
//                     MetricasSistema = new
//                     {
//                         LoginsHoje = loginsHoje,
//                         NovosUsuarios = novosUsuarios,
//                         TicketsAbertos = ticketsAbertos,
//                         AlertasSistema = alertasSistema
//                     },
//                     UsuariosRecentes = usuariosRecentes,
//                     RelatoriosDeAtividade = relatoriosRecentes,
//                     DadosUsoSistema = dadosUsoSistema
//                 };

//                 return Ok(dashboardInfo);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar dashboard do administrador");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores/estatisticas
//         [HttpGet("estatisticas")]
//         public async Task<IActionResult> GetEstatisticas()
//         {
//             try
//             {
//                 var totalUsuarios = await _context.Usuarios.CountAsync();
//                 var usuariosAtivos = await _context.Usuarios.CountAsync(u => u.Ativo);
//                 var novosUsuariosMes = await _context.Usuarios
//                     .CountAsync(u => u.DataCadastro >= DateTime.Now.AddMonths(-1));

//                 // Calcular taxa de retenção (exemplo simplificado)
//                 var usuariosMesPassado = await _context.Usuarios
//                     .CountAsync(u => u.DataCadastro >= DateTime.Now.AddMonths(-2) && 
//                                    u.DataCadastro < DateTime.Now.AddMonths(-1));
                
//                 var taxaRetencao = usuariosMesPassado > 0 ? 
//                     (await _context.Usuarios.CountAsync(u => u.DataCadastro >= DateTime.Now.AddMonths(-2) && u.Ativo)) * 100.0 / usuariosMesPassado : 100;

//                 var usoMedioDiario = await _context.LogsAcesso
//                     .Where(l => l.DataAcesso >= DateTime.Now.AddDays(-30))
//                     .GroupBy(l => l.DataAcesso.Date)
//                     .AverageAsync(g => (double?)g.Count()) ?? 0;

//                 var picoAcessos = await _context.LogsAcesso
//                     .Where(l => l.DataAcesso >= DateTime.Now.AddDays(-30))
//                     .GroupBy(l => l.DataAcesso.Date)
//                     .MaxAsync(g => (int?)g.Count()) ?? 0;

//                 var estatisticas = new
//                 {
//                     TotalUsuarios = totalUsuarios,
//                     UsuariosAtivos = usuariosAtivos,
//                     UsuariosInativos = totalUsuarios - usuariosAtivos,
//                     NovosUsuariosMes = novosUsuariosMes,
//                     TaxaRetencao = Math.Round(taxaRetencao, 2),
//                     UsoMedioDiario = Math.Round(usoMedioDiario, 2),
//                     PicoAcessos = picoAcessos
//                 };

//                 return Ok(estatisticas);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar estatísticas");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores/usuarios-recentes
//         [HttpGet("usuarios-recentes")]
//         public async Task<IActionResult> GetUsuariosRecentes([FromQuery] int limite = 10)
//         {
//             try
//             {
//                 var usuariosRecentes = await _context.Usuarios
//                     .OrderByDescending(u => u.DataCadastro)
//                     .Take(limite)
//                     .Select(u => new
//                     {
//                         u.Id,
//                         u.Nome,
//                         u.Email,
//                         DataDeCadastro = u.DataCadastro,
//                         u.Ativo,
//                         u.Perfil,
//                         UltimoAcesso = u.UltimoAcesso
//                     })
//                     .ToListAsync();

//                 return Ok(usuariosRecentes);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar usuários recentes");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores/relatorios
//         [HttpGet("relatorios")]
//         public async Task<IActionResult> GetRelatorios([FromQuery] string tipo = null)
//         {
//             try
//             {
//                 var query = _context.Relatorios.AsQueryable();

//                 if (!string.IsNullOrEmpty(tipo))
//                 {
//                     query = query.Where(r => r.Tipo == tipo);
//                 }

//                 var relatorios = await query
//                     .OrderByDescending(r => r.DataCriacao)
//                     .Take(20)
//                     .Select(r => new
//                     {
//                         r.Id,
//                         r.Titulo,
//                         Data = r.DataCriacao,
//                         r.Tipo,
//                         r.Descricao,
//                         r.Status,
//                         QuantidadeItens = r.Itens.Count
//                     })
//                     .ToListAsync();

//                 return Ok(relatorios);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar relatórios");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores/buscar-usuarios
//         [HttpGet("buscar-usuarios")]
//         public async Task<IActionResult> BuscarUsuarios(
//             [FromQuery] string termo,
//             [FromQuery] int pagina = 1,
//             [FromQuery] int tamanhoPagina = 10)
//         {
//             try
//             {
//                 if (string.IsNullOrEmpty(termo) || termo.Length < 2)
//                 {
//                     return BadRequest("Termo de busca deve ter pelo menos 2 caracteres");
//                 }

//                 var query = _context.Usuarios
//                     .Where(u => u.Nome.Contains(termo) || 
//                                u.Email.Contains(termo) ||
//                                u.Perfil.Contains(termo));

//                 var total = await query.CountAsync();

//                 var resultados = await query
//                     .OrderBy(u => u.Nome)
//                     .Skip((pagina - 1) * tamanhoPagina)
//                     .Take(tamanhoPagina)
//                     .Select(u => new
//                     {
//                         u.Id,
//                         u.Nome,
//                         u.Email,
//                         u.Perfil,
//                         u.Ativo,
//                         DataCadastro = u.DataCadastro,
//                         UltimoAcesso = u.UltimoAcesso
//                     })
//                     .ToListAsync();

//                 var resposta = new
//                 {
//                     Total = total,
//                     Pagina = pagina,
//                     TotalPaginas = (int)Math.Ceiling(total / (double)tamanhoPagina),
//                     Resultados = resultados
//                 };

//                 return Ok(resposta);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao buscar usuários");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores
//         [HttpGet]
//         public async Task<IActionResult> GetInformacoesAdministrador()
//         {
//             try
//             {
//                 var administradorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                 var administrador = await _context.Usuarios
//                     .Where(u => u.Id == int.Parse(administradorId) && u.Perfil == "Administrador")
//                     .Select(u => new
//                     {
//                         u.Id,
//                         u.Nome,
//                         u.Email,
//                         Role = u.Perfil,
//                         DataCadastro = u.DataCadastro,
//                         UltimoAcesso = u.UltimoAcesso,
//                         u.Ativo
//                     })
//                     .FirstOrDefaultAsync();

//                 if (administrador == null)
//                 {
//                     return NotFound("Administrador não encontrado");
//                 }

//                 var permissoes = await _context.Permissoes
//                     .Where(p => p.Perfil == "Administrador")
//                     .Select(p => p.Nome)
//                     .ToListAsync();

//                 var adminInfo = new
//                 {
//                     administrador.Id,
//                     administrador.Nome,
//                     administrador.Email,
//                     administrador.Role,
//                     DataCadastro = administrador.DataCadastro.ToString("dd/MM/yyyy"),
//                     UltimoAcesso = administrador.UltimoAcesso?.ToString("dd/MM/yyyy HH:mm"),
//                     administrador.Ativo,
//                     Permissoes = permissoes
//                 };

//                 return Ok(adminInfo);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar informações do administrador");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // POST: /api/administradores/exportar-relatorio
//         [HttpPost("exportar-relatorio")]
//         public async Task<IActionResult> ExportarRelatorio([FromBody] ExportarRelatorioRequest request)
//         {
//             try
//             {
//                 if (request == null || string.IsNullOrEmpty(request.TipoRelatorio))
//                 {
//                     return BadRequest("Tipo de relatório não especificado");
//                 }

//                 // Registrar a solicitação de exportação
//                 var relatorioExportacao = new RelatorioExportacao
//                 {
//                     TipoRelatorio = request.TipoRelatorio,
//                     DataInicio = request.DataInicio,
//                     DataFim = request.DataFim,
//                     Formato = request.Formato,
//                     UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
//                     DataSolicitacao = DateTime.Now,
//                     Status = "Processando"
//                 };

//                 _context.RelatoriosExportacao.Add(relatorioExportacao);
//                 await _context.SaveChangesAsync();

//                 // Simular processamento assíncrono
//                 _ = ProcessarExportacaoEmBackground(relatorioExportacao.Id);

//                 var resultado = new
//                 {
//                     Mensagem = "Solicitação de exportação recebida. O relatório será processado em background.",
//                     SolicitacaoId = relatorioExportacao.Id,
//                     DataSolicitacao = relatorioExportacao.DataSolicitacao.ToString("dd/MM/yyyy HH:mm")
//                 };

//                 return Ok(resultado);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao solicitar exportação de relatório");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // GET: /api/administradores/alertas
//         [HttpGet("alertas")]
//         public async Task<IActionResult> GetAlertas([FromQuery] bool apenasNaoResolvidos = true)
//         {
//             try
//             {
//                 var query = _context.Alertas.AsQueryable();

//                 if (apenasNaoResolvidos)
//                 {
//                     query = query.Where(a => !a.Resolvido);
//                 }

//                 var alertas = await query
//                     .OrderByDescending(a => a.Criticidade)
//                     .ThenByDescending(a => a.DataCriacao)
//                     .Take(50)
//                     .Select(a => new
//                     {
//                         a.Id,
//                         a.Tipo,
//                         a.Mensagem,
//                         Data = a.DataCriacao,
//                         a.Criticidade,
//                         a.Resolvido,
//                         a.ResolvidoPor,
//                         DataResolucao = a.DataResolucao
//                     })
//                     .ToListAsync();

//                 return Ok(alertas);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao carregar alertas");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         // PUT: /api/administradores/alertas/{id}/resolver
//         [HttpPut("alertas/{id}/resolver")]
//         public async Task<IActionResult> ResolverAlerta(int id)
//         {
//             try
//             {
//                 var alerta = await _context.Alertas.FindAsync(id);
//                 if (alerta == null)
//                 {
//                     return NotFound("Alerta não encontrado");
//                 }

//                 var administradorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

//                 alerta.Resolvido = true;
//                 alerta.ResolvidoPor = administradorId;
//                 alerta.DataResolucao = DateTime.Now;

//                 await _context.SaveChangesAsync();

//                 return Ok(new { Mensagem = "Alerta resolvido com sucesso" });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao resolver alerta");
//                 return StatusCode(500, "Erro interno do servidor");
//             }
//         }

//         private async Task<List<object>> ObterDadosUsoSistema()
//         {
//             var dataInicio = DateTime.Today.AddDays(-6); // Últimos 7 dias
//             var dados = new List<object>();

//             for (int i = 0; i < 7; i++)
//             {
//                 var data = dataInicio.AddDays(i);
//                 var acessos = await _context.LogsAcesso
//                     .CountAsync(l => l.DataAcesso.Date == data.Date);
                
//                 var usuariosUnicos = await _context.LogsAcesso
//                     .Where(l => l.DataAcesso.Date == data.Date)
//                     .Select(l => l.UsuarioId)
//                     .Distinct()
//                     .CountAsync();

//                 dados.Add(new
//                 {
//                     Dia = data.ToString("ddd"),
//                     Acessos = acessos,
//                     UsuariosUnicos = usuariosUnicos
//                 });
//             }

//             return dados;
//         }

//         private async Task ProcessarExportacaoEmBackground(int solicitacaoId)
//         {
//             try
//             {
//                 // Simular processamento demorado
//                 await Task.Delay(5000);

//                 var solicitacao = await _context.RelatoriosExportacao.FindAsync(solicitacaoId);
//                 if (solicitacao != null)
//                 {
//                     solicitacao.Status = "Concluído";
//                     solicitacao.ArquivoGerado = $"{solicitacao.TipoRelatorio}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
//                     solicitacao.DataConclusao = DateTime.Now;

//                     await _context.SaveChangesAsync();
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Erro ao processar exportação em background");

//                 var solicitacao = await _context.RelatoriosExportacao.FindAsync(solicitacaoId);
//                 if (solicitacao != null)
//                 {
//                     solicitacao.Status = "Erro";
//                     solicitacao.MensagemErro = ex.Message;
//                     await _context.SaveChangesAsync();
//                 }
//             }
//         }
//     }

//     // Classes de request
//     public class ExportarRelatorioRequest
//     {
//         public string TipoRelatorio { get; set; }
//         public DateTime? DataInicio { get; set; }
//         public DateTime? DataFim { get; set; }
//         public string Formato { get; set; } = "PDF";
//     }
// }