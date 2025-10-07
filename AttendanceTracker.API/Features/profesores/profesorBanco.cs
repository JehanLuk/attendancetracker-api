// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;

// namespace AttendanceTracker.API.Features.Profesores
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProfesorBancoController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;

//         public ProfesorBancoController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // Verifica se o professor está cadastrado pelo email
//         [HttpGet("verificar/{email}")]
//         public async Task<IActionResult> VerificarCadastro(string email)
//         {
//             var profesor = await _context.Profesores
//                 .FirstOrDefaultAsync(p => p.Email == email);

//             if (profesor == null)
//             {
//                 // Não cadastrado, redireciona para tela de cadastro
//                 return Redirect("/cadastro-profesor");
//             }

//             // Já cadastrado, retorna os dados do professor
//             return Ok(profesor);
//         }
//     }

//     // Exemplo de modelo de Professor
//     public class Profesor
//     {
//         public int Id { get; set; }
//         public string Nome { get; set; }
//         public string Email { get; set; }
//     }

//     // Exemplo de contexto do banco de dados
//     public class ApplicationDbContext : DbContext
//     {
//         public DbSet<Profesor> Profesores { get; set; }

//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//             : base(options)
//         {
//         }
//     }
// }