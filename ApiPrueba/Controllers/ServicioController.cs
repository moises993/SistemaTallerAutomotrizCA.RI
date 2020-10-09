using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [Route("Taller/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioSrv _srv;

        public ServicioController(IServicioSrv srv)
        {
            _srv = srv;
        }

        [HttpGet("ListaServicios")]
        public List<Servicio> VerServicios() =>
            _srv.VerServicio();

        [HttpPost("RegistrarServicio")]
        public IActionResult RegistrarServicio([FromBody] Servicio svc)
        {
            if (svc == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este servicio");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (!_srv.RegistrarServicio(svc.IDVehiculo, svc.descripcion))
            {
                ModelState.AddModelError("", "Ocurrió un error creando al nuevo servicio. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Servicio ingresado al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarServicio(int id, [FromBody] Servicio svc)
        {
            if (svc == null || id != svc.IDServicio)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este servicio");
                return StatusCode(400, ModelState);
            }

            if (!_srv.ActualizarServicio(id, svc.descripcion))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarServicio/{id}")]
        public IActionResult BorrarServicio(int id)
        {
            bool resultado = _srv.BorrarServicio(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó este servicio");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
