// using Microsoft.EntityFrameworkCore;
// using AttendanceTracker.Models;

// namespace AttendanceTracker.Data
// {
//     public class ApplicationDbContext : DbContext
//     {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//         {
//         }

//         public DbSet<Usuario> Usuarios { get; set; }
//         public DbSet<Relatorio> Relatorios { get; set; }
//         public DbSet<ItemRelatorio> ItensRelatorio { get; set; }
//         public DbSet<LogAcesso> LogsAcesso { get; set; }
//         public DbSet<Ticket> Tickets { get; set; }
//         public DbSet<Alerta> Alertas { get; set; }
//         public DbSet<Permissao> Permissoes { get; set; }
//         public DbSet<RelatorioExportacao> RelatoriosExportacao { get; set; }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             // Configurações adicionais do modelo
//             modelBuilder.Entity<Usuario>()
//                 .HasIndex(u => u.Email)
//                 .IsUnique();

//             modelBuilder.Entity<Relatorio>()
//                 .HasMany(r => r.Itens)
//                 .WithOne(i => i.Relatorio)
//                 .HasForeignKey(i => i.RelatorioId)
//                 .OnDelete(DeleteBehavior.Cascade);

//             // Seed data para permissões
//             modelBuilder.Entity<Permissao>().HasData(
//                 new Permissao { Id = 1, Perfil = "Administrador", Nome = "GerenciarUsuarios", Descricao = "Permissão para gerenciar usuários" },
//                 new Permissao { Id = 2, Perfil = "Administrador", Nome = "VisualizarRelatorios", Descricao = "Permissão para visualizar relatórios" },
//                 new Permissao { Id = 3, Perfil = "Administrador", Nome = "ConfigurarSistema", Descricao = "Permissão para configurar o sistema" },
//                 new Permissao { Id = 4, Perfil = "Administrador", Nome = "Auditoria", Descricao = "Permissão para acessar logs de auditoria" }
//             );
//         }
//     }
// }

// // // Registro do DbContext
// // builder.Services.AddDbContext<ApplicationDbContext>(options =>
// //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // // Registro do controlador
// // builder.Services.AddScoped<AdministradoresController>();