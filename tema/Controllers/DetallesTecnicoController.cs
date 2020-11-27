using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using tema.Models;
using tema.Models.ViewModels;

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
        public ActionResult Create(int id, [Bind("IDTecnico,direccion,telefono,telefonoExt,correo")] DetallesTecnico dtt)
        {
            DetallesTecnico dttPost;
            string tel = string.Empty;
            if (string.IsNullOrEmpty(dtt.telefono) && !string.IsNullOrEmpty(dtt.telefonoExt)) tel = dtt.telefonoExt;
            if (!string.IsNullOrEmpty(dtt.telefono) && string.IsNullOrEmpty(dtt.telefonoExt)) tel = dtt.telefono;
            else if (tel == string.Empty) ModelState.AddModelError("", "El campo de teléfono está vacío");
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    dttPost = new DetallesTecnico
                    {
                        IDTecnico = id,
                        direccion = dtt.direccion,
                        telefono = tel,
                        correo = dtt.correo
                    };
                    cliente.BaseAddress = new Uri(baseurl);
                    string myContent = JsonConvert.SerializeObject(dttPost);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage postTask = cliente.PostAsync("Taller/Tecnico/RegistrarDetallesTecnico", byteContent).Result;
                    HttpResponseMessage result = postTask;

                    if(result.StatusCode == HttpStatusCode.Conflict)
                    {
                        ModelState.AddModelError(string.Empty, "Este correo ya le pertenece a un técnico");
                    }

                    if (result.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "El correo o el teléfono ya existen para otro técnico");
                    }

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
            DttEditViewModel dtt = await GetDetalleEdit(id, new DttEditViewModel());
            if (dtt == null) return NotFound();
            return View(dtt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind("direccion,telefono,correo,codDet")] DttEditViewModel dtt)
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

                    if (result.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "El correo o el teléfono ya existen para otro técnico");
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

        #region bloqueConsultas
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

        private async Task<DttEditViewModel> GetDetalleEdit(int? id, DttEditViewModel aux)
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
                    aux = JsonConvert.DeserializeObject<DttEditViewModel>(auxRes);
                }
            }
            return aux;
        }
        #endregion bloqueConsultas
    }
}
