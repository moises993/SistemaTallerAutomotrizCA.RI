using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;

namespace tema.Controllers
{
    [Authorize]
    public class OrdenController : Controller
    {
        string baseurl = "https://localhost:44300/";
        IConverter _converter;

        public OrdenController(IConverter converter)
        {
            _converter = converter;
        }

        public async Task<IActionResult> Index()
        {
            List<Orden> aux = new List<Orden>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Taller/Orden/ListaOrdenes");

                if (res.IsSuccessStatusCode)
                {
                    var auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<List<Orden>>(auxRes);
                }
            }
            return View(aux);
        }

        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == string.Empty)
        //    {
        //        return NotFound();
        //    }

        //    var Orden = await GetOneById(id, new Orden());
        //    if (Orden == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Orden);
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create([Bind("cedulaCliente,placaVehiculo,descServicio")] Orden Orden)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(baseurl);

        //            var myContent = JsonConvert.SerializeObject(Orden);
        //            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //            var byteContent = new ByteArrayContent(buffer);
        //            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //            var postTask = client.PostAsync("Taller/Orden/RegistrarOrden", byteContent).Result;

        //            var result = postTask;

        //            if (result.StatusCode == HttpStatusCode.BadRequest)
        //            {
        //                ModelState.AddModelError(string.Empty, "Existe un Orden con esta identificación");
        //            }

        //            if (result.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }
        //        //ModelState.AddModelError(string.Empty, "Error del servidor");
        //    }
        //    return View(Orden);
        //}

        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == string.Empty)
        //    {
        //        return NotFound();
        //    }

        //    var Orden = await GetOneById(id, new Orden());
        //    if (Orden == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Orden);
        //}

        //[HttpPost]


        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(string id, [Bind("cedulaCliente,placaVehiculo,descServicio")] Orden Orden)
        //{
        //    if (id != Orden.cedulaCliente)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(baseurl);

        //            var myContent = JsonConvert.SerializeObject(Orden);
        //            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //            var byteContent = new ByteArrayContent(buffer);
        //            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //            var postTask = client.PatchAsync("Taller/Orden/ActualizarDatos/" + id, byteContent).Result;

        //            var result = postTask;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");


        //    }
        //    return View(Orden);
        //}

        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == string.Empty)
        //    {
        //        return NotFound();
        //    }

        //    var Orden = await GetOneById(id, new Orden());
        //    if (Orden == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Orden);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var Orden = await GetOneById(id, new Orden());
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseurl);

        //        var myContent = JsonConvert.SerializeObject(Orden);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byteContent = new ByteArrayContent(buffer);
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        var postTask = client.DeleteAsync("Taller/Orden/BorrarOrden/" + id).Result;

        //        var result = postTask;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null) return null;
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var deleteTask = client.DeleteAsync
                        (
                            "Taller/Orden/BorrarOrden/" + id
                        );
                    deleteTask.Wait();
                }
            }
            return RedirectToAction("Index", "Orden");
        }

        //private async Task<Orden> GetOneById(int? id, Orden aux)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage res = await client.GetAsync("Taller/Orden/ListaOrdenes");
        //        if (res.IsSuccessStatusCode)
        //        {
        //            string auxRes = res.Content.ReadAsStringAsync().Result;
        //            IEnumerable<Orden> odn = JsonConvert.DeserializeObject<IEnumerable<Orden>>(auxRes);
        //            for (int k = 0; k < odn.Count(); k++)
        //            {
        //                IEnumerable<Orden> odnquery = from orden in odn where orden.IDOrden == id select orden;
        //                aux = odnquery.First();
        //            }
        //        }
        //    }
        //    return aux;
        //}

        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Ordenes_" + DateTime.Now.Date.ToString("dd/MM/yyyy")
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = ObtenerHTML(),
                //, UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "http://localhost:51301/css/", "expediente.css")
                WebSettings =
                {
                    DefaultEncoding = "utf-8",
                    LoadImages = true,
                    UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Plantilla/dist/css", "Tablas.css")
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
            return File(file, "application/pdf", "Ordenes_" + DateTime.Now.Date.ToString("dd/MM/yyyy") + ".pdf");
        }

        public string ObtenerHTML()
        {
            StringBuilder sb = new StringBuilder();
            List<Orden> odn = ObtenerOrdenes();

            sb.Append
            (
                @"
                    <html>
                    <head></head>
                    <body>
                        <br />
                        <!--<img src ='http://localhost:51301/Imagenes/imageonline-co-whitebackgroundremoved.png'/>-->
                        <img src ='http://localhost:8080/Imagenes/imageonline-co-whitebackgroundremoved.png' width='198' height='132' />
                        
                        <br />
                        <br />
                        <p >
                            Taller Automotriz CA.RI<br />
                            San José, León Cortés, San Andrés<br />
                            Teléfono: 8355-1192<br />
                            <br />
                        </p>
                        <h1 align='center'>Reporte total de órdenes</h1>
                        <hr />
                        <table class='tabla' style='width: 100%;'>
                "
            );

            sb.Append
            (
                @"
                            <thead>
                                <tr>
                                    <th style='color:white;'>Orden</th>
                                    <th style='color:white;'>Vehículo</th>
                                    <th style='color:white;'>Cédula del cliente</th>
                                    <th style='color:white;'>Placa</th>
                                    <th style='color:white;'>Servicio</th>
                                </tr>
                            </thead>
                "
            );

            foreach (Orden od in odn)
            {
                sb.AppendFormat
                (
                    @"
                            <tbody>
                                <tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                </tr>
                            </tbody>
                    ",
                    od.IDOrden,
                    od.IDVehiculo,
                    od.cedulaCliente,
                    od.placaVehiculo,
                    od.descServicio
                );
            }

            sb.Append
            (
                @"
                        <table>
                    </body>
                    </html>
                "
            );

            return sb.ToString();
        }

        private List<Orden> ObtenerOrdenes()
        {
            List<Orden> aux = new List<Orden>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.GetAsync("Taller/Orden/ListaOrdenes").Result;

                if (res.IsSuccessStatusCode)
                {
                    string auxRes = res.Content.ReadAsStringAsync().Result;

                    aux = JsonConvert.DeserializeObject<List<Orden>>(auxRes);
                }
            }

            return aux;
        }
    }
}
