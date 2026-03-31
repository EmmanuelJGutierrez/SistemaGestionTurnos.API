using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Data;
using SistemaGestionTurnos.API.Models;

namespace SistemaGestionTurnos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // .NET inyecta el AppDbContext automáticamente acá
        public TurnosController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/turnos - Trae todos los turnos
        [HttpGet]
        public async Task<ActionResult<List<Turno>>> Get()
        {
            return await _context.Turnos
                .Include(t => t.Especialista)
                .ToListAsync();
        }

        // GET api/turnos/1 - Trae un turno por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetById(int id)
        {
            var turno = await _context.Turnos
                .Include(t => t.Especialista)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turno == null)
                return NotFound();

            return turno;
        }

        // POST api/turnos - Crea un turno nuevo
        [HttpPost]
        public async Task<ActionResult<Turno>> Create(Turno turno)
        {
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = turno.Id }, turno);
        }

        // PUT api/turnos/1 - Modifica un turno existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Turno turno)
        {
            if (id != turno.Id)
                return BadRequest();

            _context.Entry(turno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/turnos/1 - Elimina un turno
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);

            if (turno == null)
                return NotFound();

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}