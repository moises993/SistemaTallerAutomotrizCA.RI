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
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteServicio _exp;

        public ExpedienteController(IExpedienteServicio exp)
        {
            _exp = exp;
        }

        [HttpGet("ListaExpedientes")]
        public List<Expediente> VerExpedientes() =>
            _exp.VerExpedientes();

        [HttpGet("BuscarExpediente/{placa}")]
        public Expediente VerExpedientePorPlaca(string placa)
        {
            Expediente objt = _exp.ConsultarExpedientePorPlaca(placa);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarExpediente/{id}")]
        public IActionResult RegistrarExpediente(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este expediente");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (_exp.RegistrarExpediente(id) < 1)
            {
                ModelState.AddModelError("", "No se creó el expediente. Verifique que el vehículo tenga al menos una cita confirmada.");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Expediente ingresado al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarExpediente(int id, [FromBody] Expediente exp)
        {
            if (exp == null || id != exp.IDExpediente)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este expediente");
                return StatusCode(400, ModelState);
            }

            if (!_exp.ActualizarExpediente(id, exp.descripcion))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarExpediente/{id}")]
        public IActionResult BorrarExpediente(int id)
        {
            bool resultado = _exp.BorrarExpediente(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó este expediente");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
