using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class StockProductoController:Controller
    {
        private readonly IRepositorioStock repositorioStock;
        private readonly IMapper mapper;

        public StockProductoController(IRepositorioStock repositorioStock, IMapper mapper)
        {
            this.repositorioStock = repositorioStock;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var stockproducto = await repositorioStock.ObtenerStock();

            return View(stockproducto);
        }
    }
}
