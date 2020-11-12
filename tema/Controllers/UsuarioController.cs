using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;

namespace tema.Controllers
{
    public class UsuarioController : Controller
    {
        string baseurl = "https://localhost:44300/";

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
                            "Taller/Usuario/Eliminar/" + id
                        );
                }
            }
            return RedirectToAction("Index", "Usuario");
        }
    }
}
