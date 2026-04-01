using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTurnos.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Documento { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Sexo { get; set; }
        public string Rol { get; set; } = "paciente"; // Por defecto, el rol es "paciente"
    }
}
