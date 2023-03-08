using AutoMapper;
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
    public class DetallesVentasController:Controller
    {
        private readonly IRepositorioDetalleVentas repositorioDetalleVentas;
        private readonly IRepositorioArticulos repositorioArticulos;
        private readonly IMapper mapper;

        public DetallesVentasController(IRepositorioDetalleVentas repositorioDetalleVentas, IRepositorioArticulos repositorioArticulos, IMapper mapper)
        {
            this.repositorioDetalleVentas = repositorioDetalleVentas;
            this.repositorioArticulos = repositorioArticulos;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var detalle = await repositorioDetalleVentas.Buscar();

            return View(detalle);
        }


        //public IActionResult Index()
        //{
        //    return View();  
        //}

        public async Task<IActionResult> Crear()
        {

            var modelo = new DetalleVentaViewModels();
            modelo.Articulos = await ObternerAtriculos();

            modelo.rTotal = modelo.Cantidad * modelo.Precio;
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

        //[HttpGet]
        //public async Task<IActionResult> Editar(int IdDetalleVenta)
        //{


        //    var detalleventa = await repositorioDetalleVentas.ObtenerPorId(IdDetalleVenta);
        //    var modelo = new DetalleVentaViewModels();

        //    modelo.Articulos = await ObternerAtriculos();

        //    if (detalleventa is null)
        //    {
        //        return RedirectToAction("NoEncontrado", "Home");
        //    }
        //    return View(detalleventa);
        //}

        public async Task<IActionResult> Editar(int Id)
        {
            var venta = await repositorioDetalleVentas.ObtenerPorId(Id);

            if (venta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = mapper.Map<DetalleVentaViewModels>(venta);

            modelo.Articulos = await ObternerAtriculos();

            modelo.rTotal = modelo.Cantidad * modelo.Precio;
            return View(modelo);


        }


        [HttpPost]
        public async Task<IActionResult> Editar( DetalleVenta detalleVentaEditar)
        {

            var detalle = await repositorioDetalleVentas.ObtenerPorId(detalleVentaEditar.IdDetalleVenta);

            if (!ModelState.IsValid)
            {
                return View(detalleVentaEditar);
            }

            if (detalleVentaEditar is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioDetalleVentas.Actualizar(detalleVentaEditar);

            return RedirectToAction("Index");

        }



        private async Task<IEnumerable<SelectListItem>> ObternerAtriculos()
        {
            var articulos = await repositorioArticulos.Buscar();
            return articulos.Select(X => new SelectListItem(X.Nombre, X.Idarticulo.ToString()));

        }
    }
}
