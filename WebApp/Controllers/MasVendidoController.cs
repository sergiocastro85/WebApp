using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class MasVendidoController:Controller
    {
        private readonly IRepositorioDetalleVentas repositoriomasvendido;
        private readonly IMapper mapper;

        public MasVendidoController(IRepositorioDetalleVentas repositoriomasvendido, IMapper mapper)
        {
            this.repositoriomasvendido = repositoriomasvendido;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            var ventas = await repositoriomasvendido.BuscarProductoMasvendido();

            var pruebas= ventas.Select(v=>new IndiceVentasViewModel
            {
                IdArticulo=v.IdArticulo,
                Nombre=v.Nombre,
                TotalVendido=v.TotalVendido,
            }).ToList();

            return View(pruebas);

      
            //return View(modelo);

            //hasta aca funciona.......

            //var masvendido = await repositoriomasvendido.BuscarProductoMasvendido();

   


          
        }


    }
}
