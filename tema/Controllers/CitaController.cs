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
    public class CitaController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public async Task<IActionResult> Index()
        {
            CitaViewModel ctavm = new CitaViewModel();
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseurl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resCita = await cliente.GetAsync("Taller/Cita/ListaCitas");
                HttpResponseMessage resServicio = await cliente.GetAsync("Taller/Servicio/ListaServicios");
                HttpResponseMessage resVhl = await cliente.GetAsync("Taller/Vehiculo/ListaVehiculos");
                if (resCita.IsSuccessStatusCode && resServicio.IsSuccessStatusCode && resVhl.IsSuccessStatusCode)
                {
                    var auxResCita = resCita.Content.ReadAsStringAsync().Result;
                    var auxResServicio = resServicio.Content.ReadAsStringAsync().Result;
                    var auxResVhl = resVhl.Content.ReadAsStringAsync().Result;
                    ctavm.cita = JsonConvert.DeserializeObject<List<Cita>>(auxResCita);
                    ctavm.servicio = JsonConvert.DeserializeObject<List<Servicio>>(auxResServicio);
                    ctavm.vehiculo = JsonConvert.DeserializeObject<List<Vehiculo>>(auxResVhl);
                }
            }
            return View(ctavm);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cita = await GetOneById(id, new Cita());
            if (Cita == null)
            {
                return NotFound();
            }

            return View(Cita);
        }
        public async Task<ActionResult> Create()
        {
            CitaViewModel ctavm = new CitaViewModel();
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseurl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resTecnico = await cliente.GetAsync("Taller/Tecnico/ListaTecnicos");
                HttpResponseMessage resServicio = await cliente.GetAsync("Taller/Servicio/ListaServicios");
                HttpResponseMessage resClt = await cliente.GetAsync("Taller/Cliente/ListaClientes");
                if (resTecnico.IsSuccessStatusCode && resClt.IsSuccessStatusCode)
                {
                    var auxResTecnico = resTecnico.Content.ReadAsStringAsync().Result;
                    var auxResClt = resClt.Content.ReadAsStringAsync().Result;
                    var auxResServicio = resServicio.Content.ReadAsStringAsync().Result;
                    ctavm.servicio = JsonConvert.DeserializeObject<List<Servicio>>(auxResServicio);
                    ctavm.tecnico = JsonConvert.DeserializeObject<List<Tecnico>>(auxResTecnico);
                    ctavm.cliente = JsonConvert.DeserializeObject<List<Cliente>>(auxResClt);
                }
            }
            return View(ctavm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IDTecnico,cedulaCliente,fecha,hora,asunto,descripcion,citaConfirmada")] CitaViewModel cta)
        {
            Cita ctapost = new Cita
            {
                IDTecnico = cta.IDTecnico,
                cedulaCliente = cta.cedulaCliente,
                fecha = cta.fecha,
                hora = cta.hora,
                asunto = cta.asunto,
                descripcion = cta.descripcion,
                citaConfirmada = cta.citaConfirmada
            };
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(ctapost);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = await cliente.PostAsync("Taller/Cita/RegistrarCita", byteContent);
                    var result = postTask;
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Existen errores en la información suministrada");
                    }
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(ctapost);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cita = await GetOneById(id, new Cita());
            if (Cita == null)
            {
                return NotFound();
            }
            return View(Cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("IDCita,IDTecnico,cedulaCliente,fecha,hora,asunto,descripcion,citaConfirmada")] Cita Cita)
        {
            if (id != Cita.IDCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(Cita);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Cita/ActualizarDatos/" + Cita.IDCita, byteContent).Result;

                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


            }
            return View(Cita);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cita = await GetOneById(id, new Cita());
            if (Cita == null)
            {
                return NotFound();
            }

            return View(Cita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Cita = await GetOneById(id, new Cita());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(Cita);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.DeleteAsync("Taller/Cita/BorrarCita/" + Cita.IDCita).Result;

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Cita> GetOneById(int? id, Cita aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Cita/BuscarCita/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<Cita>(auxRes);
                }
            }

            return aux;
        }
        
        public async Task<IActionResult> CreateOrden(int? idCita, int idCliente, string desc)
        {
            if (idCita == null)
            {
                ModelState.AddModelError(string.Empty, "No se brindó un id para generar la orden");
            }
            if (ModelState.IsValid)
            {
                ParametrosOrden odnPost = new ParametrosOrden
                {
                    IDCita = idCita,
                    IDCliente = idCliente,
                    descripcion = desc
                };
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(odnPost);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = await cliente.PostAsync("Taller/Orden/RegistrarOrden", byteContent);
                    HttpResponseMessage result = postTask;
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Error", "Ya existe una orden correspondiente a esta cita.");
                        return RedirectToAction("Index", "Cita");
                    }
                    else if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Cita");
                    }
                }
            }
            return RedirectToAction("Index", "Cita");
        }

        /*
         El siguiente código contiene una clase para mandarla como objeto a la API
         */
        private class ParametrosOrden
        {
            public int? IDCita { get; set; }
            public int IDCliente { get; set; }
            public string descripcion { get; set; }
        }
    }
}
