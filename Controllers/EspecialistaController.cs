using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Data;
using SistemaGestionTurnos.API.Models;

namespace SistemaGestionTurnos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialistasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EspecialistasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Especialista>>> Get()
        {
            return await _context.Especialistas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Especialista>> Create(Especialista especialista)
        {
            _context.Especialistas.Add(especialista);
            await _context.SaveChangesAsync();
            return Ok(especialista);
        }
    }
}