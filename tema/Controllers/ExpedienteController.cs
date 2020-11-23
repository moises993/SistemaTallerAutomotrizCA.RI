using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using tema.Models;
using tema.Utilidades;

namespace tema.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        string baseurl = "https://localhost:44300/";
        IHttpContextAccessor _httpContextAccessor;
        MethodBase mb = MethodBase.GetCurrentMethod();
        private IConverter _converter;

        public ExpedienteController(IHttpContextAccessor httpContextAccessor, IConverter converter)
        {
            _httpContextAccessor = httpContextAccessor;
            _converter = converter;
        }

        public async Task<ActionResult> CreateExpediente(int? id)
        {
            UtilidadRegistro.Registrar(mb.ReflectedType.Name + ".CreateExpediente", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            if (id != null)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseurl);
                    HttpResponseMessage respuesta = await cliente.PostAsync("Taller/Expediente/RegistrarExpediente/" + id, null);
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError) ModelState.AddModelError(string.Empty, "");
                    else if (respuesta.IsSuccessStatusCode) return RedirectToAction("Index", "Vehiculo");
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
        #nullable disable

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
            UtilidadRegistro.Registrar(mb.ReflectedType?.Name + ".Edit", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
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
            UtilidadRegistro.Registrar(mb.ReflectedType?.Name + ".Delete", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
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
            UtilidadRegistro.Registrar(mb.ReflectedType?.Name + ".ObtenerExpedientePorId", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
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

        public async Task<IActionResult> CreatePDF(int? id)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Expediente" + "_" + id + "-" + ObtenerExpedientePorId(id, new Expediente()).Result.placa
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = await ObtenerHTML(id),
                //, UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "http://localhost:51301/css/", "expediente.css")
                WebSettings = 
                { 
                    DefaultEncoding = "utf-8", 
                    LoadImages = true, 
                    UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/css", "expediente.css") 
                },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Página [page] de [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Taller Automotriz CA.RI" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "Expediente" + "_" + id + "-" + ObtenerExpedientePorId(id, new Expediente()).Result.placa + ".pdf");
        }

        public async Task<string> ObtenerHTML(int? id)
        {
            StringBuilder sb = new StringBuilder();

            Expediente expmod = await ObtenerExpedientePorId(id, new Expediente());

            sb.Append(@"
            <html>
                <head>
                </head>
                <body>
                <br />
                <img src ='http://localhost:51301/Imagenes/imageonline-co-whitebackgroundremoved.png'/>
                <img src ='http://localhost:8001/Imagenes/imageonline-co-whitebackgroundremoved.png'/>
                <br />
                <br />
                <p>
                    Taller Automotriz CA.RI<br />
                    San José, León Cortés, San Andrés<br />
                    Teléfono: 8355-1192<br />
                    <br />
                </p>
                <h1 align ='center'> Expediente </ h1 >

                <div>
                <h4>Datos del vehículo</h4>
                <hr />");

            if (expmod != null)
            {
                sb.AppendFormat(@"
                     <dl>
                        <dt>
                            Código del expediente
                        </dt>
                        <dd>
                            {0}
                        </dd>
                        <dt>
                            Nombre del técnico responsable
                        </dt>
                        <dd>
                            {1}
                        </dd>
                        <dt>
                            Asunto de la primera cita
                        </dt>
                        <dd>
                            {2}
                        </dd>
                        <dt>
                            Descripción
                        </dt>
                        <dd style='white-space: pre-line;'>
                            {3}
                        </dd>
                        <dt>
                            Fecha de creación del documento
                        </dt>
                        <dd>
                            {4}
                        </dd>
                        <dt>
                            Nombre del cliente
                        </dt>
                        <dd>
                            {5}
                        </dd>
                        <dt>
                            Marca del vehículo
                        </dt>
                        <dd>
                            {6}
                        </dd>
                        <dt>
                            Modelo
                        </dt>
                        <dd>
                            {7}
                        </dd>
                        <dt>
                            Placa
                        </dt>
                        <dd>
                            {8}
                        </dd>
                    </dl>
                </div>
                ", expmod.IDExpediente,
                expmod.nombreTecnico,
                expmod.asunto,
                expmod.descripcion,
                expmod.fechaCreacionExp.Date.ToString("dd/MM/yyyy"),
                expmod.nombreCliente,
                expmod.marca,
                expmod.modelo,
                expmod.placa);
            }

            sb.Append(@"
                    </body>
                </html>
            ");
            return sb.ToString();
        }
    }
}

/*
 ViewBag.Message = "El sistema carece de la información suficiente para crear el expediente. Corrobore que exista al menos una cita para este vehículo y que dicha cita esté confirmada";
 */