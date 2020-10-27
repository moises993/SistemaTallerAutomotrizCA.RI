using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;

namespace tema.Controllers
{
    [Authorize]
    public class OrdenController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public async Task<IActionResult> Index()
        {
            List<Orden> aux = new List<Orden>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Orden/ListaOrdenes");

                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<List<Orden>>(auxRes);
                }
            }
            return View(aux);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var Orden = await GetOneById(id, new Orden());
            if (Orden == null)
            {
                return NotFound();
            }

            return View(Orden);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("cedulaCliente,placaVehiculo,descServicio")] Orden Orden)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(Orden);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PostAsync("Taller/Orden/RegistrarOrden", byteContent).Result;

                    var result = postTask;

                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Existe un Orden con esta identificación");
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                //ModelState.AddModelError(string.Empty, "Error del servidor");
            }
            return View(Orden);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var Orden = await GetOneById(id, new Orden());
            if (Orden == null)
            {
                return NotFound();
            }
            return View(Orden);
        }

        [HttpPost]

      
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("cedulaCliente,placaVehiculo,descServicio")] Orden Orden)
        {
            if (id != Orden.cedulaCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(Orden);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Orden/ActualizarDatos/" + id, byteContent).Result;

                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


            }
            return View(Orden);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var Orden = await GetOneById(id, new Orden());
            if (Orden == null)
            {
                return NotFound();
            }

            return View(Orden);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var Orden = await GetOneById(id, new Orden());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(Orden);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.DeleteAsync("Taller/Orden/BorrarOrden/" + id).Result;

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Orden> GetOneById(string cedula, Orden aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Orden/BuscarOrden/" + cedula);
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<Orden>(auxRes);
                }
            }

            return aux;
        }
    }
}
