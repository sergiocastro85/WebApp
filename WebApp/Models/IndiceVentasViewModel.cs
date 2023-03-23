using System.Collections.Generic;

namespace WebApp.Models
{
    public class IndiceVentasViewModel:ProductoMasVendido
    {
        public IEnumerable<ProductoMasVendido>MasVendidos  { get; set; }
    }
}
