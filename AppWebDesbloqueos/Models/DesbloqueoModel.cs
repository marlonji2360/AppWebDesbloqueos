namespace AppWebDesbloqueos.Models
{
    public class DesbloqueoModel
    {
        public int Id { get; set; }        
        public string? Nombre { get; set; }        
        public DateTime? FechaCorreo { get; set; }
        public string? OperacionRealizada1 { get; set; }
        public DateTime? FechaRespuestaDesbloqueo { get; set; }        
        public string? Observaciones {  get; set; }
        public string? Lista {  get; set; }
        public string? Cn {  get; set; }
        public string? Observacion {  get; set; }        
        public string? TiempoDeAtencion {  get; set; }
        public string? Accionista { get; set; }        
        public string? Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }

    }
}
