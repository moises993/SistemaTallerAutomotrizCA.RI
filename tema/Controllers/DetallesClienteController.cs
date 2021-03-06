﻿using System;
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
    public class DetallesClienteController : Controller
    {
        string baseurl = "https://localhost:44300/";
        public ActionResult Create()
        {
            return View();
        }

        [TempData]
        public string Message { get; set; }

        [HttpPost]
        public ActionResult Create(int id, [Bind("IDCliente,direccion,telefono,telefonoExt,correo")] DetallesCliente dtc)
        {
            DetallesCliente dtcPost;
            string tel = string.Empty;
            if (string.IsNullOrEmpty(dtc.telefono) && !string.IsNullOrEmpty(dtc.telefonoExt)) tel = dtc.telefonoExt; 
            if (!string.IsNullOrEmpty(dtc.telefono) && string.IsNullOrEmpty(dtc.telefonoExt)) tel = dtc.telefono; 
            else if(tel == string.Empty) ModelState.AddModelError("", "El campo de teléfono está vacío"); 
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    dtcPost = new DetallesCliente
                    {
                        IDCliente = id,
                        direccion = dtc.direccion,
                        telefono = tel,
                        correo = dtc.correo
                    };
                    client.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(dtcPost);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PostAsync("Taller/Cliente/RegistrarDetallesCliente", byteContent).Result;
                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", "Cliente");
                    }

                    if(result.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "El correo o el teléfono ya existen para otro cliente");
                    }
                }
            }
            return View(dtc);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var dtc = await GetDetalleEdit(id, new DtcEditViewModel());
            if (dtc == null) return NotFound();
            return View(dtc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind("IDCliente,direccion,telefono,correo,codDet")] DtcEditViewModel dtc)
        {
            if (id != dtc.codDet)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                DetallesCliente dtcPatch = new DetallesCliente
                {
                    IDCliente = dtc.IDCliente,
                    direccion = dtc.direccion,
                    telefono = dtc.telefono,
                    correo = dtc.correo,
                    codDet = dtc.codDet
                };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(dtcPatch);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PatchAsync("Taller/Cliente/ActualizarDetalles/" + id, byteContent).Result;
                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Cliente");
                    }

                    if (result.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "El correo o el teléfono ya existen para otro cliente");
                    }
                }
            }
            return View(dtc);
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
                            "Taller/Cliente/BorrarDetallesCliente/" + id
                        );
                    deleteTask.Wait();
                }
            }
            return RedirectToAction("Index", "Cliente");
        }

        #region consultas
        private async Task<DetallesCliente> GetDetalle(int? id, DetallesCliente aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync
                    (
                        "Taller/Cliente/ObtenerDetalleIndividual/" + id
                    );
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<DetallesCliente>(auxRes);
                }
            }
            return aux;
        }

        private async Task<DtcEditViewModel> GetDetalleEdit(int? id, DtcEditViewModel aux)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync
                    (
                        "Taller/Cliente/ObtenerDetalleIndividual/" + id
                    );
                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<DtcEditViewModel>(auxRes);
                }
            }
            return aux;
        }
        #endregion consultas
    }
}
