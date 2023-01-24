using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositoriosCategorias repositoriosCategorias;
        private readonly IServicioUsuarios servicioUsuarios;


        //constructor
        public CategoriasController(IRepositoriosCategorias repositoriosCategorias,IServicioUsuarios servicioUsuarios)
        {
            this.repositoriosCategorias = repositoriosCategorias;
            this.servicioUsuarios = servicioUsuarios;
        } 

        public async Task<IActionResult> Index()
        {
            var categorias = await repositoriosCategorias.Obtener();

            return View(categorias);
        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult>Editar(int id)
        {

            var categoria = await repositoriosCategorias.ObtenerId(id);
            
            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(categoria);

        }

      
        public async Task<IActionResult>Borrar(int id)
        {
            var categoria = await repositoriosCategorias.ObtenerId(id);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");

            }

            return View(categoria); 
        }


        [HttpPost]
        public async Task<IActionResult> BorrarCategorias(Categoria categoria)
        {
            var existeCategoria = await repositoriosCategorias.ObtenerId(categoria.Idcategoria);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");

            }

            await repositoriosCategorias.Borrar(categoria.Idcategoria);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Editar(Categoria categoria)
        {
            var categoriaExiste = await repositoriosCategorias.ObtenerId(categoria.Idcategoria);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositoriosCategorias.Actualizar(categoria);
            return RedirectToAction("Index");
        }


        [HttpPost]
        //async asincrono
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (!ModelState.IsValid) 
            { 
                return View(categoria); 
            
            }

            var yaExisteCategoria = await repositoriosCategorias.Existe(categoria.Nombre);

            if (yaExisteCategoria)
            {
                ModelState.AddModelError(nameof(categoria.Nombre),$"el nombre {categoria.Nombre} ya existe.");

                return View(categoria);
            }


           await repositoriosCategorias.Crear(categoria);
            return RedirectToAction("Index"); 
        }



    }
}
