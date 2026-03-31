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
        public string Nombre { get; set; }
        public string? Documento { get; set; }
        public string? PasswordHash { get; set; }
        public string? Mail { get; set; }
        public string? Telefono { get; set; }
        public string? Sexo { get; set; }
    }
}
