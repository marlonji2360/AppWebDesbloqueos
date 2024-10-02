using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class ListaModel
    {
        public int IdLista { get; set; }
        [Required(ErrorMessage = "El campo Nombre Lista es obligatorio.")]
        public string? NombreLista { get; set; }
    }
}
