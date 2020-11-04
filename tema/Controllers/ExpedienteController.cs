using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using tema.Models;

namespace tema.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public ActionResult CreateExpediente(int? id)
        {
            if (id != null)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    cliente.PostAsync("Taller/Expediente/RegistrarExpediente/" + id, null).Wait();
                    return RedirectToAction("Index", "Vehiculo");
                }
            }
            return RedirectToAction("Index", "Vehiculo");
        }

        #nullable enable
        public async Task<ActionResult> Details(string? id) //id es 'placa'. Se deja así por convención
        {
            if (id == null) return NotFound();
            Expediente exp = await ObtenerExpediente(id, new Expediente());
            if (exp == null) return NoExisteElExpediente();
            return View(exp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Expediente exp = await ObtenerExpedientePorId(id, new Expediente());
            if (exp == null) return NoExisteElExpediente();
            return View(exp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IDExpediente,descripcion")] Expediente exp)
        {
            if (id != exp.IDExpediente) return NotFound();
            Expediente expPatch = new Expediente { IDExpediente = id, descripcion = exp.descripcion };
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(expPatch);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage patchTask = client.PatchAsync("Taller/Expediente/ActualizarDatos/" + id, byteContent).Result;
                    HttpResponseMessage result = patchTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","Vehiculo");
                    }
                }
            }
            return View(exp);
        }

        #nullable enable
        private async Task<Expediente> ObtenerExpediente(string? placa, Expediente salida)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseurl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cliente.GetAsync("Taller/Expediente/BuscarExpediente/" + placa);
                if (res.IsSuccessStatusCode)
                {
                    string auxRes = res.Content.ReadAsStringAsync().Result;
                    salida = JsonConvert.DeserializeObject<Expediente>(auxRes);
                }
            }
            return salida;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Vehiculo");
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    client.DeleteAsync("Taller/Expediente/BorrarExpediente/" + id).Wait();
                }
            }
            return RedirectToAction("Index", "Vehiculo");
        }

        #nullable enable
        private async Task<Expediente> ObtenerExpedientePorId(int? id, Expediente salida)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseurl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cliente.GetAsync("Taller/Expediente/ListaExpedientes");
                if (res.IsSuccessStatusCode)
                {
                    string auxRes = res.Content.ReadAsStringAsync().Result;
                    IEnumerable<Expediente> expColeccion = JsonConvert.DeserializeObject<IEnumerable<Expediente>>(auxRes);
                    for (int i = 0; i < expColeccion.Count(); i++)
                    {
                        IEnumerable<Expediente> consulta = from expsel in expColeccion where expsel.IDExpediente == id select expsel;
                        salida = consulta.First();
                    }
                }
            }
            return salida;
        }

        public ActionResult NoExisteElExpediente()
        {
            return View("NoExisteElExpediente");
        }
    }
}
