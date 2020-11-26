using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;
using tema.Utilidades;

namespace tema.Controllers
{
    public class UsuarioController : Controller
    {
        string baseurl = "https://localhost:44300/";
        IHttpContextAccessor _httpContextAccessor;
        MethodBase mb = MethodBase.GetCurrentMethod();

        public UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Usuario> aux = new List<Usuario>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync("Taller/Usuario/ListaUsuarios");
            if (res.IsSuccessStatusCode) aux = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(res.Content.ReadAsStringAsync().Result);
            return View(aux);
        }

        [Authorize]
        public IActionResult CambiarContrasena()
        {
            return View();
        }

        public class UsuarioCambioClave
        {
            [Display(Name = "Contraseña actual")]
            [Required(ErrorMessage = "Escriba su contraseña")]
            public string contrasenaActual { get; set; }
            [Display(Name = "Contraseña nueva")]
            [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$", ErrorMessage = "La contraseña no cumple con los requisitos solicitados")]
            [Required(ErrorMessage = "Escriba una contraseña")]
            public string nuevaContrasena { get; set; }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CambiarContrasena([Bind("contrasenaActual,nuevaContrasena")] UsuarioCambioClave usr)
        {
            if(ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseurl);
                Usuario usrPost = new Usuario
                {
                    correo = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value,
                    clave = usr.contrasenaActual,
                    nuevaContrasena = usr.nuevaContrasena
                };
                string myContent = JsonConvert.SerializeObject(usrPost);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage patchTask = client.PostAsync("Taller/Usuario/CambiarContrasena", byteContent).Result;
                HttpResponseMessage result = patchTask;
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, "La contraseña actual es incorrecta");
                }
                UtilidadRegistro.Registrar(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value, "Usuario", mb.ReflectedType.Name, "CambiarContrasena");
                return RedirectToAction("Index", "Home");
            }
            return View(usr);
        }

        [Authorize]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Usuario");
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    HttpResponseMessage deleteTask = await client.DeleteAsync
                        (
                            "Taller/Usuario/EliminarUsuario/" + id
                        );
                }
            }
            return RedirectToAction("Index", "Usuario");
        }
    }
}
