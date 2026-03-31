namespace SistemaGestionTurnos.API.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Disponible { get; set; } = true;
        // relacion con Especialista
        public int EspecialistaId { get; set; } 
        public Especialista? Especialista { get; set; }
        public string? Sector { get; set; }
    }
}
