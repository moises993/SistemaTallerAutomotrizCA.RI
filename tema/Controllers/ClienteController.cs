using System;
using System.Collections.Generic;
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
    public class ClienteController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public async Task<IActionResult> Index()
        {
            IEnumerable<Cliente> aux = new List<Cliente>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync("Taller/Cliente/ListaClientes");
            if (res.IsSuccessStatusCode) aux = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(res.Content.ReadAsStringAsync().Result);
            return View(aux);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == string.Empty) return NotFound();
            ClienteViewModel cltvm = new ClienteViewModel();
            cltvm.cliente = await GetOneById(id, new Cliente());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Cliente/ObtenerDetallesCliente/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;
                    cltvm.detallesCliente = JsonConvert.DeserializeObject<List<DetallesCliente>>(auxRes);
                }
                else
                {
                    cltvm.detallesCliente = new List<DetallesCliente>();
                    return RedirectToAction("Index", "Cliente");
                }
            }
            return View(cltvm);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("nombre,pmrApellido,sgndApellido,cedula,cedExt,cltFrecuente,fechaIngreso")] Cliente cliente)
        {
            Cliente cltpost = null;
            string ced = string.Empty;
            if (string.IsNullOrEmpty(cliente.cedula) && !string.IsNullOrEmpty(cliente.cedExt)) ced = cliente.cedExt;
            if (!string.IsNullOrEmpty(cliente.cedula) && string.IsNullOrEmpty(cliente.cedExt)) ced = cliente.cedula;
            else if (ced == string.Empty) ModelState.AddModelError("", "El campo de la cédula está vacío");
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    cltpost = new Cliente
                    {
                        nombre = cliente.nombre,
                        pmrApellido = cliente.pmrApellido,
                        sgndApellido = cliente.sgndApellido,
                        cedula = ced,
                        cltFrecuente = cliente.cltFrecuente
                    };
                    client.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(cltpost);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = await client.PostAsync("Taller/Cliente/RegistrarCliente", byteContent);
                    var result = postTask;

                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Existe un cliente con esta cédula");
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(cltpost);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var cliente = await GetOneById(id, new Cliente());
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("IDCliente,nombre,pmrApellido,sgndApellido,cedula,cltFrecuente,fechaIngreso")] Cliente cliente)
        {
            if (id != cliente.cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    var myContent = JsonConvert.SerializeObject(cliente);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Cliente/ActualizarDatos/" + id, byteContent).Result;

                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


            }
            return View(cliente);
        }

        [Authorize]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var cliente = await GetOneById(id, new Cliente());
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cliente = await GetOneById(id, new Cliente());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(cliente);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.DeleteAsync("Taller/Cliente/BorrarCliente/" + id).Result;

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Cliente> GetOneById(string cedula, Cliente aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Cliente/BuscarCliente/" + cedula);
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<Cliente>(auxRes);
                }
            }
            return aux;
        }
    }
}
