using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebApp.Models
{
    public class Articulo
    {
        public int Idarticulo { get; set; }

        [Display(Name ="Categoría")]
        public int Idcategoria { get; set; }

        [Required (ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:100)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public string Marca { get; set; }

        public int PrecioVenta { get; set; }

        
        [Display(Name ="Fecha de Vigencia")]
        public DateTime Dtmvigencia { get; set; }


        public string Categoria { get; set; }   
    }
}
