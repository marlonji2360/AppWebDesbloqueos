using System.ComponentModel.DataAnnotations;

namespace AppWebDesbloqueos.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo Usuario Sistema es obligatorio.")]
        public string? UsuarioSistema { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }
    }
}
