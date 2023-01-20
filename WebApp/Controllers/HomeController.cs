using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {



        //acciones son las funciones que se ejecutan cuando hacemos una petición http a una ruta especifica
        public IActionResult Index()
        {
    
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NoEncontrado() 
        { 
            
            return View(); 
        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}