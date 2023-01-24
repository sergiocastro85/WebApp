using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class ArticuloCreacionViewModel:Articulo
    {
        public IEnumerable<SelectListItem> Categorias { get; set; }
    }
}
