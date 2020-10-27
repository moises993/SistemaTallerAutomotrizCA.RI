﻿using System;
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
    public class TecnicoController : Controller
    {
        string baseurl = "https://localhost:44300/";

    public async Task<IActionResult> Index()
    {
        List<Tecnico> aux = new List<Tecnico>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync("Taller/Tecnico/ListaTecnicos");

            if (res.IsSuccessStatusCode)
            {
                var auxRes = res.Content.ReadAsStringAsync().Result;

                aux = JsonConvert.DeserializeObject<List<Tecnico>>(auxRes);
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

        var Tecnico = await GetOneById(id, new Tecnico());
        if (Tecnico == null)
        {
            return NotFound();
        }

        return View(Tecnico);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([Bind("nombre,pmrApellido,sgndApellido,cedula,fechaIngreso")] Tecnico Tecnico)
    {
        if (ModelState.IsValid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(Tecnico);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.PostAsync("Taller/Tecnico/RegistrarTecnico", byteContent).Result;

                var result = postTask;

                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, "Existe un Tecnico con esta identificación");
                }

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            //ModelState.AddModelError(string.Empty, "Error del servidor");
        }
        return View(Tecnico);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == string.Empty)
        {
            return NotFound();
        }

        var Tecnico = await GetOneById(id, new Tecnico());
        if (Tecnico == null)
        {
            return NotFound();
        }
        return View(Tecnico);
        }
      
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, [Bind("nombre,pmrApellido,sgndApellido,cedula,fechaIngreso")] Tecnico Tecnico)
    {
        if (id != Tecnico.cedula)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var myContent = JsonConvert.SerializeObject(Tecnico);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var postTask = client.PatchAsync("Taller/Tecnico/ActualizarDatos/" + id, byteContent).Result;

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


        }
        return View(Tecnico);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == string.Empty)
        {
            return NotFound();
        }

        var Tecnico = await GetOneById(id, new Tecnico());
        if (Tecnico == null)
        {
            return NotFound();
        }

        return View(Tecnico);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var Tecnico = await GetOneById(id, new Tecnico());
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseurl);

            var myContent = JsonConvert.SerializeObject(Tecnico);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var postTask = client.DeleteAsync("Taller/Tecnico/BorrarTecnico/" + id).Result;

            var result = postTask;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task<Tecnico> GetOneById(string id, Tecnico aux)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync("Taller/Tecnico/BuscarTecnico/" + id);
            if (res.IsSuccessStatusCode)
            {
                var auxRes = res.Content.ReadAsStringAsync().Result;

                aux = JsonConvert.DeserializeObject<Tecnico>(auxRes);
            }
        }

        return aux;
    }
}
}
