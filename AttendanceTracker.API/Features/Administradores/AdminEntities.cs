using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Perfil { get; set; } // "Administrador", "Usuário", "Moderador"

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? UltimoAcesso { get; set; }

        // Navigation properties
        public virtual ICollection<LogAcesso> LogsAcesso { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class Relatorio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Tipo { get; set; } // "Mensal", "Performance", "Segurança", "Usuários"

        public string Descricao { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Concluído";

        // Navigation properties
        public virtual ICollection<ItemRelatorio> Itens { get; set; }
    }

    public class ItemRelatorio
    {
        [Key]
        public int Id { get; set; }

        public int RelatorioId { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public string Valor { get; set; }

        // Navigation property
        [ForeignKey("RelatorioId")]
        public virtual Relatorio Relatorio { get; set; }
    }

    public class LogAcesso
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime DataAcesso { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Acao { get; set; } // "Login", "Logout", "AcessoPagina"

        [StringLength(500)]
        public string Detalhes { get; set; }

        // Navigation property
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }

    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public StatusTicket Status { get; set; } = StatusTicket.Aberto;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataResolucao { get; set; }

        // Navigation property
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }

    public enum StatusTicket
    {
        Aberto,
        EmAndamento,
        Resolvido,
        Fechado
    }

    public class Alerta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } // "Aviso", "Erro", "Informação"

        [Required]
        public string Mensagem { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Criticidade { get; set; } // "Baixa", "Média", "Alta"

        public bool Resolvido { get; set; } = false;

        public int? ResolvidoPor { get; set; }

        public DateTime? DataResolucao { get; set; }

        // Navigation property
        [ForeignKey("ResolvidoPor")]
        public virtual Usuario UsuarioResolucao { get; set; }
    }

    public class Permissao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Perfil { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }

    public class RelatorioExportacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoRelatorio { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [StringLength(10)]
        public string Formato { get; set; } = "PDF";

        public int UsuarioId { get; set; }

        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        public DateTime? DataConclusao { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Processando"; // "Processando", "Concluído", "Erro"

        [StringLength(200)]
        public string ArquivoGerado { get; set; }

        public string MensagemErro { get; set; }

        // Navigation property
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}