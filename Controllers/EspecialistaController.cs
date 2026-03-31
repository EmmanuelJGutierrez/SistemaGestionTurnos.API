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

        //inyeccion de AppDbContext
        public EspecialistasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/especialistas. Devuelve la lista de especialistas.
        [HttpGet]
        public async Task<ActionResult<List<Especialista>>> Get()
        {
            return await _context.Especialistas.ToListAsync();
        }

        // POST: api/especialistas. Agrega un nuevo especialista.
        [HttpPost]
        public async Task<ActionResult<Especialista>> Create(Especialista especialista)
        {
            _context.Especialistas.Add(especialista);
            await _context.SaveChangesAsync();
            return Ok(especialista);
        }

        // GET: api/especialistas/{id}. Devuelve un especialista por su ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialista>> GetById(int id) // explicame como funciona esto
        {
            var especialista = await _context.Especialistas.FindAsync(id);
            if (especialista == null)
            {
                return NotFound();
            }
            return Ok(especialista);
        }


        // PUT: api/especialistas/{id}. Actualiza un especialista existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Especialista especialista)
        {
            if (id != especialista.Id)
                return BadRequest();
            _context.Entry(especialista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/especialistas/{id}. Elimina un especialista por su ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var especialista = await _context.Especialistas.FindAsync(id);
            if (especialista == null)
                return NotFound();
            _context.Especialistas.Remove(especialista);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}