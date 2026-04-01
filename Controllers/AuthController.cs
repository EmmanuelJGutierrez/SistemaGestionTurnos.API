using SistemaGestionTurnos.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaGestionTurnos.API.Data;
using SistemaGestionTurnos.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaGestionTurnos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST api/auth/registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            // Verificar que el mail no esté en uso
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Mail == usuario.Mail);

            if (existe)
                return BadRequest("Ya existe un usuario con ese mail.");
            // Sin importar lo que mande el usuario, siempre es Paciente
            usuario.Rol = "Paciente";

            // Hashear la contraseña antes de guardarla
            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordHash);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario registrado exitosamente.");
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            // Buscar el usuario por mail
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Mail == login.Mail);

            if (usuario == null)
                return Unauthorized("Mail o contraseña incorrectos.");

            // Verificar la contraseña
            if (!BCrypt.Net.BCrypt.Verify(login.Password, usuario.PasswordHash))
                return Unauthorized("Mail o contraseña incorrectos.");

            // Generar el token JWT
            var token = GenerarToken(usuario);
            return Ok(new { token });
        }

        private string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var credenciales = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // Claims — información que va dentro del token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Mail),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
