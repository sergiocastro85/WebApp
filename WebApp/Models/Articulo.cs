using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Articulo
    {
        public int Idarticulo { get; set; }

        [Display(Name ="Tipo Cuenta")]
        public int Idcategoria { get; set; }

        [Required (ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:100)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public string Marca { get; set; }

        public int PrecioVenta { get; set; }

        public DateTime Dtmvigencia { get; set; }
    }
}
