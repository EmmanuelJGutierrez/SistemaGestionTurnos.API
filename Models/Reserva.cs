using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTurnos.API.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int TurnoId { get; set; }
        public Turno? Turno { get; set; }
        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}