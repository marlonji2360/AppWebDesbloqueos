using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class CentroNegocioModel
    {
        public int IdCn {  get; set; }

        [Required(ErrorMessage = "El campo CN es obligatorio.")]
        public string NombreCn { get; set; }
    }
}
