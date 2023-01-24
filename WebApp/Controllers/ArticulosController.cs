using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class ArticulosController:Controller
    {
        private readonly IRepositoriosCategorias repositoriosCategorias;
        private readonly IRepositorioArticulos repositorioArticulos;

        //Constructor
        public ArticulosController(IRepositoriosCategorias repositoriosCategorias, IRepositorioArticulos repositorioArticulos)
        {
            this.repositoriosCategorias = repositoriosCategorias;
            this.repositorioArticulos = repositorioArticulos;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            
            var modelo = new ArticuloCreacionViewModel();

            modelo.Categorias = await ObtenerCategorias();

            return View(modelo);  
        }

        [HttpPost] 

        public async Task<IActionResult> Crear (ArticuloCreacionViewModel articulo)
        {
            var categoria = await repositoriosCategorias.ObtenerId(articulo.Idcategoria);

            if (categoria == null) { 
            
            return  RedirectToAction("NoEncontrado","Home"); 
            
            }

            if (!ModelState.IsValid)
            {
                articulo.Categorias = await ObtenerCategorias();

                return View(articulo);
            }

            await repositorioArticulos.Crear(articulo);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias()
        {
            var categorias = await repositoriosCategorias.Obtener();
            return categorias.Select(x => new SelectListItem(x.Nombre, x.Idcategoria.ToString()));

        }



    }
}
