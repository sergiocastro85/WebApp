using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }    

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Identificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        public string Direccion { get; set; }


        [StringLength(maximumLength: 20)]
        public string Telefono { get; set; }
    }
}
