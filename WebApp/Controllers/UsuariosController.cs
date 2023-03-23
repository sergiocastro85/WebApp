using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UsuariosController:Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;
        private readonly SignInManager<Usuario> signInManager;

        //clas UserManager para crear el usuario

        public UsuariosController(UserManager<Usuario>userManager, IMapper mapper, SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

       public IActionResult Registro()
        {
            return View();  
        }


        [HttpPost]
        public async Task<IActionResult>Registro(RegistroViewModel modelo)
        {

            if (!ModelState.IsValid)
            {

                return View(modelo);
            }

            var usuario = new Usuario() { Email = modelo.Email, Tipo=modelo.Tipo };

            var resultado = await userManager.CreateAsync(usuario, password: modelo.pasword);

            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Articulos");
            }

            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }

        }
    }
}
