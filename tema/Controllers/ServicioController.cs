using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;
using tema.Models.ViewModels;

namespace tema.Controllers
{
    [Authorize]
    public class ServicioController : Controller
    {
        string baseurl = "https://localhost:44300/";

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IDVehiculo,descripcion")] CitaViewModel ctavm)
        {
            Servicio srv = new Servicio { IDVehiculo = ctavm.IDVehiculo, descripcion = ctavm.descripcion };
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(srv);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = await cliente.PostAsync("Taller/Servicio/RegistrarServicio", byteContent);
                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Cita");
                    }
                }
            }
            return null;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cita = await GetOneById(id, new Servicio());
            if (Cita == null)
            {
                return NotFound();
            }
            return View(Cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("IDServicio,descripcion")] Servicio servicio)
        {
            if (id != servicio.IDServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(servicio);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Servicio/ActualizarDatos/" + servicio.IDServicio, byteContent).Result;

                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Cita");
                    }
                }
            }
            return View(servicio);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return null;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var deleteTask = client.DeleteAsync
                        (
                            "Taller/Servicio/BorrarServicio/" + id
                        );
                    deleteTask.Wait();
                }
            }
            return RedirectToAction("Index", "Cita");
        }

        private async Task<Servicio> GetOneById(int? id, Servicio aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Servicio/ListaServicios");
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;
                    var coleccionSvc = JsonConvert.DeserializeObject<IEnumerable<Servicio>>(auxRes);
                    for(int k = 0;k < coleccionSvc.Count();k++)
                    {
                        var svcquery = from svc in coleccionSvc where svc.IDServicio == id select svc;
                        aux = svcquery.First();
                    }
                }
            }
            return aux;
        }
    }
}
