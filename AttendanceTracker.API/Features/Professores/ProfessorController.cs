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
        IProfessorService _professorService;

        public ProfessoresController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet("inserir")]
        public async Task<IActionResult> CriarAsync([FromBody] ProfessorDTO professor){

            var professores = await _professorService.CriarAsync(professor);
            return Ok(professores);
        }
    }
}
