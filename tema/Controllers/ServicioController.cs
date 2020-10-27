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
    public class ServicioController : Controller
    {
        string baseurl = "https://localhost:44300/";

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IDVehiculo,descripcion")] CitaViewModel ctavm)
        {
            Servicio srv = new Servicio { IDVehiculo = ctavm.IDVehiculo, descripcion = ctavm.descripcion };
            if (ModelState.IsValid)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    var myContent = JsonConvert.SerializeObject(srv);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = await cliente.PostAsync("Taller/Servicio/RegistrarServicio", byteContent);
                    var result = postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Cita");
                    }
                }
            }
            return null;
        }
    }
}
