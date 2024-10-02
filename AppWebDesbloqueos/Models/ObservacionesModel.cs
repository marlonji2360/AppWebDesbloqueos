using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class ObservacionesModel
    {
        public int IdObservacion { get; set; }
        [Required(ErrorMessage = "El campo Observaciones es obligatorio.")]
        public string? ObservacionesDetalle { get; set; }
    }
}
