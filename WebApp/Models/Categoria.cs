using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Categoria
    {
        public int Idcategoria { get; set; }

        [Required (ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}
