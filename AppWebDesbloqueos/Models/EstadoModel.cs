using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class EstadoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string NombreEstado { get; set; }
    }
}
