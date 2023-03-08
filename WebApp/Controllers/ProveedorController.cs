using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Contracts;
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

        public async Task<ActionResult> Editar(int Id)
        {
            var proveedoreditar = await repositorioProveedor.ObternerPorId(Id);

            if (proveedoreditar is null)
            {
                return RedirectToAction("NoEncontrado","Home");   
            }

     
            return  View(proveedoreditar);
           
        }

        [HttpPost]
        public async Task<IActionResult>Editar(Proveedor proveedorEditar)
        {
            var proveedor = await repositorioProveedor.ObternerPorId(proveedorEditar.IdProveedor);

            if (!ModelState.IsValid)
            {
                return View(proveedorEditar);
            }

            if (proveedorEditar is null)
            {
                return RedirectToAction("NoEncontrado","Home");
            }

            await repositorioProveedor.Actualizar(proveedorEditar);

            return RedirectToAction("Index");
   

        }

        public async Task<IActionResult> Borrar(int Id)
        {

            var proveedor = await repositorioProveedor.ObternerPorId(Id);

            if (proveedor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(proveedor);

        }

        [HttpPost]
        public async Task<IActionResult> BorrarProveedor(int IdProveedor)
        {

            var proveedor = await repositorioProveedor.ObternerPorId(IdProveedor);

            if (proveedor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");

            }
            await repositorioProveedor.Borrar(IdProveedor);
            return RedirectToAction("Index");   
        }

    }
}
