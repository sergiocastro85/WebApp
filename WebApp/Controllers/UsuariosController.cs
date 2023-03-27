using AutoMapper;
using Microsoft.AspNetCore.Authentication;
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


        [HttpGet]

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(LoguinViewModel modelo)
        {
            if (!ModelState.IsValid) 
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.Email, modelo.pasword, 
                                                                    modelo.Recuerdame, lockoutOnFailure: false);
            if (resultado.Succeeded) 
            {
               return RedirectToAction("Index", "Articulos");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de Usuario o password incorrecto");
                return View(modelo);    
            }

        }


        [HttpPost]
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Articulos");
        }
    }
}
