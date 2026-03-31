using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Data;
using SistemaGestionTurnos.API.Models;

namespace SistemaGestionTurnos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/reservas
        [HttpGet]
        public async Task<ActionResult<List<Reserva>>> Get()
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Turno)
                    .ThenInclude(t => t.Especialista)
                .ToListAsync();
        }

        // POST api/reservas - Reservar un turno
        [HttpPost]
        public async Task<ActionResult<Reserva>> Reservar(int usuarioId, int turnoId)
        {
            // 1. Verificar que el turno existe
            var turno = await _context.Turnos.FindAsync(turnoId);
            if (turno == null)
                return NotFound("El turno no existe.");

            // 2. Verificar que el turno está disponible
            if (!turno.Disponible)
                return BadRequest("El turno ya está reservado.");

            // 3. Verificar que el turno no es en el pasado
            if (turno.FechaHora < DateTime.Now)
                return BadRequest("No se pueden reservar turnos pasados.");

            // 4. Verificar que el usuario existe
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                return NotFound("El usuario no existe.");

            // 5. Crear la reserva
            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                TurnoId = turnoId,
                FechaReserva = DateTime.Now,
                Estado = "Activa"
            };

            _context.Reservas.Add(reserva);

            // 6. Marcar el turno como no disponible
            turno.Disponible = false;

            await _context.SaveChangesAsync();
            return Ok(reserva);
        }

        // DELETE api/reservas/1 - Cancelar una reserva
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancelar(int id)
        {
            // 1. Verificar que la reserva existe
            var reserva = await _context.Reservas
                .Include(r => r.Turno)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
                return NotFound("La reserva no existe.");

            // 2. Verificar que faltan más de 24hs
            if (reserva.Turno.FechaHora - DateTime.Now < TimeSpan.FromHours(24))
                return BadRequest("No se puede cancelar un turno con menos de 24hs de anticipación.");

            // 3. Marcar el turno como disponible de nuevo
            reserva.Turno.Disponible = true;

            // 4. Eliminar la reserva
            _context.Reservas.Remove(reserva);

            await _context.SaveChangesAsync();
            return Ok("Reserva cancelada exitosamente.");
        }
    }
}