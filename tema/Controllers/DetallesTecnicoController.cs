using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using tema.Models;

namespace tema.Controllers
{
    [Authorize]
    public class DetallesTecnicoController : Controller
    {
        string baseurl = "https://localhost:44300/";

        [Authorize]
        [Authorize(Roles = "manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, [Bind("IDTecnico,direccion,telefono,correo")] DetallesTecnico dtt)
        {
            DetallesTecnico dttPost;
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    dttPost = new DetallesTecnico
                    {
                        IDTecnico = id,
                        direccion = dtt.direccion,
                        telefono = dtt.telefono,
                        correo = dtt.correo
                    };
                    cliente.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(dttPost);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = cliente.PostAsync("Taller/Tecnico/RegistrarDetallesTecnico", byteContent).Result;
                    HttpResponseMessage result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Tecnico");
                    }
                }
            }
            return View(dtt);
        }

        [Authorize]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            DetallesTecnico dtt = await GetDetalle(id, new DetallesTecnico());
            if (dtt == null) return NotFound();
            return View(dtt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind("direccion,telefono,correo,codDet")] DetallesTecnico dtt)
        {
            if (id != dtt.codDet) return NotFound();
            if (ModelState.IsValid)
            {
                DetallesTecnico dttPatch = new DetallesTecnico
                {
                    direccion = dtt.direccion,
                    telefono = dtt.telefono,
                    correo = dtt.correo,
                    codDet = dtt.codDet
                };
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(dttPatch);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = cliente.PatchAsync("Taller/Tecnico/ActualizarDetalles/" + id, byteContent).Result;
                    HttpResponseMessage result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Tecnico");
                    }
                }
            }
            return View(dtt);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return null;
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    HttpResponseMessage deleteTask = await cliente.DeleteAsync
                    (
                        "Taller/Tecnico/BorrarDetallesTecnico/" + id
                    );
                }
            }
            return RedirectToAction("Index", "Tecnico");
        }

        private async Task<DetallesTecnico> GetDetalle(int? id, DetallesTecnico aux)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync
                    (
                        "Taller/Tecnico/ObtenerDetalleIndividual/" + id
                    );
                if (res.IsSuccessStatusCode)
                {
                    string auxRes = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<DetallesTecnico>(auxRes);
                }
            }
            return aux;
        }
    }
}
