using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class DetallesVentas:Controller
    {
        private readonly IRepositorioDetalleVentas repositorioDetalleVentas;
        private readonly IRepositorioArticulos repositorioArticulos;

        public DetallesVentas(IRepositorioDetalleVentas repositorioDetalleVentas, IRepositorioArticulos repositorioArticulos)
        {
            this.repositorioDetalleVentas = repositorioDetalleVentas;
            this.repositorioArticulos = repositorioArticulos;
        }

        public IActionResult Index()
        {
            return View();  
        }

        public async Task<IActionResult> Crear()
        {

            var modelo = new DetalleVentaViewModels();
            modelo.Articulos = await ObternerAtriculos();
            return View(modelo);    


        }

        [HttpPost]
        public async Task<IActionResult> Crear (DetalleVentaViewModels modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Articulos = await ObternerAtriculos();
                return View(modelo); 
            }


            await repositorioDetalleVentas.Crear(modelo);
            return RedirectToAction("Index");   

        }

        private async Task<IEnumerable<SelectListItem>> ObternerAtriculos()
        {
            var articulos = await repositorioArticulos.Buscar();
            return articulos.Select(X => new SelectListItem(X.Nombre, X.Idarticulo.ToString()));

        }
    }
}
