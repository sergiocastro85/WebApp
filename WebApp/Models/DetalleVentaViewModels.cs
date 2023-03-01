using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class DetalleVentaViewModels:DetalleVenta
    {
        public IEnumerable<SelectListItem> Articulos { get; set; }

        
    }
}
