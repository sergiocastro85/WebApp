using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private readonly IMapper mapper;

        //Constructor
        public ArticulosController(IRepositoriosCategorias repositoriosCategorias, IRepositorioArticulos repositorioArticulos,IMapper mapper)
        {
            this.repositoriosCategorias = repositoriosCategorias;
            this.repositorioArticulos = repositorioArticulos;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var articulosConCategoria = await repositorioArticulos.Buscar();
            var modelo = articulosConCategoria
                 .GroupBy(x => x.Categoria)
                 .Select(grupo => new IndiceArticulosViewModel
                 {
                     Categoria = grupo.Key,
                     Articulos = grupo.AsEnumerable()
                 }).ToList(); 
            
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var categorias = await repositoriosCategorias.Obtener();
            var modelo = new ArticuloCreacionViewModel();

            modelo.Categorias = categorias.Select(x => new SelectListItem(x.Nombre, x.Idcategoria.ToString()));

       

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

        public async Task<IActionResult> Editar(int Id)
        {
            var articulo= await repositorioArticulos.ObtenerPorId(Id); 

            if (articulo is null)
            {
                return RedirectToAction("NoEncontrado","Home");   
            }

            var modelo = mapper.Map<ArticuloCreacionViewModel>(articulo);    

            modelo.Categorias=await ObtenerCategorias();
            return View(modelo);    


        }

        [HttpPost]
        public async Task<IActionResult>Editar(ArticuloCreacionViewModel articuloEditar)
        {
            var articulo = await repositorioArticulos.ObtenerPorId(articuloEditar.Idarticulo);
            
            if (articulo is null)
            {
                return RedirectToAction("NoEncontrado","Home");   
            }

            
            var categoria = await repositoriosCategorias.ObtenerId(articuloEditar.Idcategoria);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioArticulos.Actualizar(articuloEditar);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult>Borrar(int Id)
        {
            var articulo = await repositorioArticulos.ObtenerPorId(Id);

            if (articulo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(articulo);
        }


        [HttpPost] 

        public async Task<IActionResult> BorrarArticulo(int IdArticulo)
        {

            var articulo = await repositorioArticulos.ObtenerPorId(IdArticulo);

            if (articulo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioArticulos.Borrar(IdArticulo);

            return RedirectToAction("Index");   

        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias()
        {
            var categorias = await repositoriosCategorias.Obtener();
            return categorias.Select(x => new SelectListItem(x.Nombre, x.Idcategoria.ToString()));


        }



    }
}
