using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class VehiculoController : Controller
    {
        string baseurl = "https://localhost:44300/";

        public async Task<IActionResult> Index()
        {
            List<Vehiculo> aux = new List<Vehiculo>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Vehiculo/ListaVehiculos");
                if (res.IsSuccessStatusCode) aux = JsonConvert.DeserializeObject<List<Vehiculo>>(res.Content.ReadAsStringAsync().Result);
            }
            return View(aux);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == string.Empty) return NotFound();
            Vehiculo Vehiculo = await GetOneById(id, new Vehiculo());
            if (Vehiculo == null) return NotFound();
            return View(Vehiculo);
        }

        public static async Task<List<SelectListItem>> ObtenerLista()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44300/");
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage resVehiculo = await cliente.GetAsync("Taller/Cliente/ListaClientes");
            List<Cliente> clt = JsonConvert.DeserializeObject<List<Cliente>>(await resVehiculo.Content.ReadAsStringAsync());
            foreach (Cliente temp in clt)
            {
                ls.Add(new SelectListItem() { Text = temp.nombre, Value = temp.cedula });
            }
            return ls;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("cedclt,marca,modelo,placa,placaExt")] VehiculoViewModel vhlmod)
        {
            string placa = string.Empty;
            if (string.IsNullOrEmpty(vhlmod.placa) && !string.IsNullOrEmpty(vhlmod.placaExt)) placa = vhlmod.placaExt;
            if (!string.IsNullOrEmpty(vhlmod.placa) && string.IsNullOrEmpty(vhlmod.placaExt)) placa = vhlmod.placa;
            else if (placa == string.Empty) ModelState.AddModelError("", "El campo de la placa está vacío");
            else if (string.IsNullOrEmpty(vhlmod.cedclt)) ModelState.AddModelError("", "No se ha seleccionado un cliente");
            if (ModelState.IsValid)
            {
                Vehiculo vhlPatch = new Vehiculo
                {
                    cedclt = vhlmod.cedclt,
                    marca = vhlmod.marca,
                    modelo = int.Parse(vhlmod.modelo),
                    placa = placa.ToUpper()
                };
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(vhlPatch);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = client.PostAsync("Taller/Vehiculo/RegistrarVehiculo", byteContent).Result;
                    HttpResponseMessage result = postTask;
                    if (result.StatusCode == HttpStatusCode.BadRequest) ModelState.AddModelError(string.Empty, "Existe un vehículo con esta placa");
                    else if(result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                }
            }
            return View(vhlmod);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Vehiculo vehiculo = await ObtenerVehiculoPorId(id, new Vehiculo());
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("IDVehiculo,marca,modelo,placa")] Vehiculo Vehiculo)
        {
            if (id != Vehiculo.IDVehiculo) return NotFound();
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(Vehiculo);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = await client.PatchAsync("Taller/Vehiculo/ActualizarDatos/" + id, byteContent);
                    HttpResponseMessage result = postTask;
                    if (result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                }
            }
            return View(Vehiculo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Vehiculo vehiculo = await ObtenerVehiculoPorId(id, new Vehiculo());
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = await ObtenerVehiculoPorId(id, new Vehiculo());
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                string myContent = JsonConvert.SerializeObject(vehiculo);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage postTask = client.DeleteAsync("Taller/Vehiculo/BorrarVehiculo/" + id).Result;
                HttpResponseMessage result = postTask;
                if (result.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Vehiculo> GetOneById(string placa, Vehiculo aux)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Vehiculo/BuscarVehiculo/" + placa);
                if (res.IsSuccessStatusCode) aux = JsonConvert.DeserializeObject<Vehiculo>(res.Content.ReadAsStringAsync().Result);
            }
            return aux;
        }

        private async Task<Vehiculo> ObtenerVehiculoPorId(int? id, Vehiculo aux)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Vehiculo/ListaVehiculos");
                if (res.IsSuccessStatusCode)
                {
                    IEnumerable<Vehiculo> coleccionVhl = JsonConvert.DeserializeObject<IEnumerable<Vehiculo>>(res.Content.ReadAsStringAsync().Result);
                    for (int k = 0; k < coleccionVhl.Count(); k++)
                    {
                        IEnumerable<Vehiculo> vhlquery = from vhl in coleccionVhl where vhl.IDVehiculo == id select vhl;
                        aux = vhlquery.First();
                    }
                }
            }
            return aux;
        }
    }
}
