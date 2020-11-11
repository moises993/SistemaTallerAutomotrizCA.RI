using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using tema.Models;
using tema.Models.ViewModels;

namespace tema.Controllers
{
    [Authorize]
    public class ServicioController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public static async Task<List<SelectListItem>> ObtenerLista()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44300/");
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage resVehiculo = await cliente.GetAsync("Taller/Vehiculo/ListaVehiculos");
            List<Vehiculo> vhl = JsonConvert.DeserializeObject<List<Vehiculo>>(await resVehiculo.Content.ReadAsStringAsync());
            foreach (Vehiculo temp in vhl)
            {
                ls.Add(new SelectListItem() { Text = temp.marca, Value = Convert.ToString(temp.IDVehiculo) });
            }
            return ls;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IDVehiculo,descripcion")] Servicio svcm)
        {
            Servicio srv = new Servicio { IDVehiculo = svcm.IDVehiculo, descripcion = svcm.descripcion };
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
            return View(svcm);
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
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Servicio/ListaServicios");
                if (res.IsSuccessStatusCode)
                {
                    IEnumerable<Servicio> coleccionSvc = JsonConvert.DeserializeObject<IEnumerable<Servicio>>(res.Content.ReadAsStringAsync().Result);
                    for(int k = 0;k < coleccionSvc.Count();k++)
                    {
                        IEnumerable<Servicio> svcquery = from svc in coleccionSvc where svc.IDServicio == id select svc;
                        aux = svcquery.First();
                    }
                }
            }
            return aux;
        }
    }
}
