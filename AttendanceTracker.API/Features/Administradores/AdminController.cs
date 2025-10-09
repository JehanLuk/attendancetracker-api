/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTracker.API.Features.Professores
{
    [Authorize(Roles = "Professor")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : Controller
    {
        // GET: /api/profesores/dashboard
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            // Aqui você pode buscar as informações necessárias para o dashboard do professor
            var profesorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Simulação de dados que seriam exibidos na TelaProfesor.cshtml
            var dashboardInfo = new
            {
                Nome = User.Identity.Name,
                Materias = new[] { "Matemática", "Física" },
                TotalAlunos = 120,
                FaltasHoje = 5
            };

            // Retorne os dados para a view ou como JSON
            return Ok(dashboardInfo);
        }

        // GET: /api/profesores
        [HttpGet]
        public IActionResult Index()
        {
            // Exemplo de retorno de lista de professores ou informações do professor logado
            var profesorInfo = new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Nome = User.Identity.Name,
                Email = User.FindFirstValue(ClaimTypes.Email)
            };

            return Ok(profesorInfo);
        }
    }
} */
