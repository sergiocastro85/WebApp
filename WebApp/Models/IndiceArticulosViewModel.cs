using System.Collections;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class IndiceArticulosViewModel
    {
        public string Categoria { get; set; }

        public IEnumerable<Articulo> Articulos { get; set; }    
    }
}
