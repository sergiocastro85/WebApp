using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DetalleVenta
    {

        public int IdDetalleVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdArticulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Precio  { get; set; }


        [DisplayName("Fecha Venta")]
        [DataType(DataType.Date)]
        public DateTime DtmFecha { get; set; }=DateTime.Now;    

        public string Nombre { get; set; }


        public int rTotal { get; set; } 

        

    }
}
