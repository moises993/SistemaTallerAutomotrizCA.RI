using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using tema.Models;
using tema.Utilidades;
using tema.Utilidades.Interfaces;

namespace tema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepositorio _usRep;
        string baseurl = "https://localhost:44300/";

        public HomeController(ILogger<HomeController> logger, IUsuarioRepositorio usRep)
        {
            _logger = logger;
            _usRep = usRep;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            Usuario objeto = new Usuario();
            return View(objeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario objeto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            Usuario objetoUsuario = await _usRep.LoginAsync(baseurl + "Taller/Usuario/IniciarSesion", objeto);

            if (_usRep.Error400)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objetoUsuario.correo));
            identity.AddClaim(new Claim(ClaimTypes.Role, objetoUsuario.rol));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objetoUsuario.tokenSesion);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "manager")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuarioViewModel objetoVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Usuario objeto = new Usuario
            {
                correo = objetoVM.correo,
                rol = objetoVM.rol
            };

            bool resultado = await _usRep.RegisterAsync(baseurl + "Taller/Usuario/Registrar", objeto);

            if (_usRep.Error400)
            {
                ModelState.AddModelError(string.Empty, "Este correo le pertenece a un usuario");
                return View();
            }

            if (_usRep.Error404)
            {
                ModelState.AddModelError(string.Empty, "Este correo no existe en el sistema");
                return View();
            }

            if (_usRep.Error409)
            {
                ModelState.AddModelError(string.Empty, "Error interno");
                return View();
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
