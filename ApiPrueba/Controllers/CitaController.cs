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
    public class CitaController : ControllerBase
    {
        private readonly ICitaServicio _citaServicio;

        public CitaController(ICitaServicio citaServicio)
        {
            _citaServicio = citaServicio;
        }

        [HttpGet("ListaCitas")]
        public List<Cita> VerCitas() =>
            _citaServicio.VerCitas();

        [HttpGet("ListaCedulas")]
        public List<string> VerCedulas() =>
            _citaServicio.VerCedulasTecnico();

        [HttpGet("BuscarCita/{id}")]
        public Cita VerCitaPorCedula(int id)
        {
            Cita objt = _citaServicio.ConsultarCitaPorId(id);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarCita")]
        public IActionResult RegistrarCita([FromBody] Cita cta)
        {
            if (cta == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para esta cita");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (!_citaServicio.RegistrarCita(cta.cedulaCliente, cta.IDTecnico, cta.fecha, cta.hora, cta.asunto, cta.descripcion, cta.citaConfirmada))
            {
                ModelState.AddModelError("", "Ocurrió un error creando la cita. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Cita ingresada al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarCita(int id, [FromBody] Cita cta)
        {
            if (cta == null || id != cta.IDCita)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para esta cita");
                return StatusCode(400, ModelState);
            }

            if (!_citaServicio.ActualizarCita(id, cta.fecha, cta.hora, cta.asunto, cta.descripcion, cta.citaConfirmada))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarCita/{id}")]
        public IActionResult BorrarCita(int id)
        {
            bool resultado = _citaServicio.BorrarCita(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó esta cita");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
