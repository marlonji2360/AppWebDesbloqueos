using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class DesbloqueoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Fecha Correo es obligatorio.")]
        public DateTime? FechaCorreo { get; set; }

        [Required(ErrorMessage = "El campo Operación es obligatorio.")]
        public string? OperacionRealizada1 { get; set; }

        [Required(ErrorMessage = "El campo Fecha Desbloqueo es obligatorio.")]
        public DateTime? FechaRespuestaDesbloqueo { get; set; }

        
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "El campo Lista es obligatorio.")]
        public string? Lista { get; set; }


        [Required(ErrorMessage = "El campo CN es obligatorio.")]  
        public string? Cn {  get; set; }

        [Required(ErrorMessage = "El campo Observacio es obligatorio.")]
        public string? Observacion {  get; set; }        
        public string? TiempoDeAtencion {  get; set; }

        [Required(ErrorMessage = "El campo Accionista es obligatorio.")]
        public string? Accionista { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        public string? Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [Required(ErrorMessage = "El campo Tipo Documento es obligatorio.")]
        public string? TipoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Número Documento es obligatorio.")]
        public string? NumeroDocumento { get; set; }

    }
}
