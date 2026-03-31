namespace SistemaGestionTurnos.API.Models
{
    public class Especialista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Documento { get; set; }
        public string? Mail { get; set; }
        public string? Telefono { get; set; }
        public string? Sexo { get; set; }
        public string Especialidad { get; set; }
        public bool Disponible { get; set; } = true;
    }
}
