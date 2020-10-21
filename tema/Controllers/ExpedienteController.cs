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
using tema.Models.ViewModels;

namespace tema.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public async Task<IActionResult> Index()
        {
            List<Expediente> aux = new List<Expediente>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Expediente/ListaExpedientes");

                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<List<Expediente>>(auxRes);
                }
            }
            return View(aux);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Expediente = await GetOneById(id, new Expediente());
            if (Expediente == null)
            {
                return NotFound();
            }

            return View(Expediente);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("nombreTecnico,asunto,descripcion,fechaCreacionExp,nombreCliente,marca,modelo,placa")] Expediente Expediente)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(Expediente);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PostAsync("Taller/Expediente/RegistrarExpediente", byteContent).Result;

                    var result = postTask;

                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Existe un Expediente con esta identificación");
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(Expediente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Expediente = await GetOneById(id, new Expediente());
            if (Expediente == null)
            {
                return NotFound();
            }
            return View(Expediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("nombreTecnico,asunto,descripcion,fechaCreacionExp,nombreCliente,marca,modelo,placa")] Expediente Expediente)
        {
            if (id != Expediente.IDVehiculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(Expediente);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Expediente/ActualizarDatos/" + Expediente.IDVehiculo, byteContent).Result;

                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


            }
            return View(Expediente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Expediente = await GetOneById(id, new Expediente());
            if (Expediente == null)
            {
                return NotFound();
            }

            return View(Expediente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Expediente = await GetOneById(id, new Expediente());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(Expediente);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.DeleteAsync("Taller/Expediente/BorrarExpediente/" + Expediente.IDVehiculo).Result;

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Expediente> GetOneById(int? id, Expediente aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Expediente/BuscarExpediente/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<Expediente>(auxRes);
                }
            }

            return aux;
        }
    }
}
