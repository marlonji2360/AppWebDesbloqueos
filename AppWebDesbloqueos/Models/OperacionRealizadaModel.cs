using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class OperacionRealizadaModel
    {
        public int IdOperacionRealizada { get; set; }
        [Required(ErrorMessage = "El campo Operación es obligatorio.")]
        public string? OperacionRealizada {  get; set; }
    }
}
