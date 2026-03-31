using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Models;

namespace SistemaGestionTurnos.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Cada DbSet
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Especialista> Especialistas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}