using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers

{
    public class ProveedorController: Controller
    {
        private readonly IRepositorioProveedor repositorioProveedor;
        private readonly IMapper mapper;

        //Constructor
        public ProveedorController(IRepositorioProveedor repositorioProveedor, IMapper mapper)
        {
            this.repositorioProveedor = repositorioProveedor;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var proveedor = await repositorioProveedor.ObtenerProveedor();

            return View(proveedor);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Proveedor proveedor)
        {
            if (!ModelState.IsValid) 
            {
                return View(proveedor); 
            
            }

            await repositorioProveedor.Crear(proveedor);
            return RedirectToAction("Index");

        
        }
    }
}
