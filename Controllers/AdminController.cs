using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Data;
using SistemaGestionTurnos.API.Models;

namespace SistemaGestionTurnos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]  // ← todo el controller protegido
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/admin/usuarios - Ver todos los usuarios
        [HttpGet("usuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // PUT api/admin/usuarios/{id}/rol - Cambiar rol de un usuario
        [HttpPut("usuarios/{id}/rol")]
        public async Task<IActionResult> CambiarRol(int id, string nuevoRol)
        {
            if (nuevoRol != "Admin" && nuevoRol != "Paciente")
                return BadRequest("Rol inválido. Los roles válidos son 'Admin' y 'Paciente'.");

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            usuario.Rol = nuevoRol;
            await _context.SaveChangesAsync();
            return Ok($"Rol de {usuario.Nombre} actualizado a {nuevoRol}.");
        }

        // DELETE api/admin/usuarios/{id} - Eliminar usuario
        [HttpDelete("usuarios/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario eliminado.");
        }

        // DELETE api/admin/turnos/{id} - Eliminar turno
        [HttpDelete("turnos/{id}")]
        public async Task<IActionResult> EliminarTurno(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
                return NotFound("Turno no encontrado.");

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();
            return Ok("Turno eliminado.");
        }

        // DELETE api/admin/especialistas/{id} - Eliminar especialista
        [HttpDelete("especialistas/{id}")]
        public async Task<IActionResult> EliminarEspecialista(int id)
        {
            var especialista = await _context.Especialistas.FindAsync(id);
            if (especialista == null)
                return NotFound("Especialista no encontrado.");

            _context.Especialistas.Remove(especialista);
            await _context.SaveChangesAsync();
            return Ok("Especialista eliminado.");
        }
    }
}