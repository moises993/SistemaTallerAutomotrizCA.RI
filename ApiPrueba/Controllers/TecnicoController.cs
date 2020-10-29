using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [Route("Taller/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoServicio _tecServicio;

        public TecnicoController(ITecnicoServicio tecServ)
        {
            _tecServicio = tecServ;
        }

        [HttpGet("ListaTecnicos")]
        public List<Tecnico> VerTecnicos() =>
            _tecServicio.VerTecnicos();

        [HttpGet("TecnicosConCita")]
        public List<TecnicoCita> VerTecnicosConCita() =>
            _tecServicio.MostrarTecnicosConCita();

        [HttpGet("BuscarTecnico/{cedula}")]
        public Tecnico VerTecnicoPorCedula(string cedula)
        {
            Tecnico objt = _tecServicio.ConsultarTecnicoCedula(cedula);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarTecnico")]
        public IActionResult RegistrarTecnico([FromBody] Tecnico tec)
        {
            if (tec == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este técnico");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (!_tecServicio.RegistrarTecnico(tec.nombre, tec.pmrApellido, tec.sgndApellido, tec.cedula))
            {
                ModelState.AddModelError("", "Ocurrió un error creando al nuevo Tecnico. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Técnico ingresado al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarTecnico(string id, [FromBody] Tecnico tec)
        {
            if (tec == null || id != tec.cedula)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este técnico");
                return StatusCode(400, ModelState);
            }

            if (!_tecServicio.ActualizarTecnico(tec.nombre, tec.pmrApellido, tec.sgndApellido, tec.cedula))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarTecnico/{id}")]
        public IActionResult BorrarTecnico(string id)
        {
            bool resultado = _tecServicio.BorrarTecnico(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó este técnico");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }

        /*-----------------------------------------------------------------------------------------------------------------
         ------------------------------------------DETALLES DEL TÉCNICO----------------------------------------------------
         ------------------------------------------------------------------------------------------------------------------*/

        [HttpGet("ObtenerDetallesTecnico/{cedula}")]
        public List<DetallesTecnico> VerDetallesTecnico(string cedula) =>
            _tecServicio.VerDetallesTecnico(cedula);

        [HttpGet("ObtenerDetalleIndividual/{id}")]
        public DetallesTecnico VerTecnicoPorCedula(int id)
        {
            DetallesTecnico objt = _tecServicio.VerDetalleIndividual(id);
            if (objt == null) return null;
            return objt;
        }

        [HttpPost("RegistrarDetallesTecnico")]
        public IActionResult RegistrarDetallesTecnico([FromBody] DetallesTecnico tec)
        {
            if (tec == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este técnico");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (!_tecServicio.RegistrarDetalleTecnico(tec.IDTecnico, tec.direccion, tec.telefono, tec.correo))
            {
                ModelState.AddModelError("", "Ocurrió un error insertando los detalles. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Información registrada exitosamente" });
        }

        [HttpPatch("ActualizarDetalles/{id}")]
        public IActionResult ActualizarDetallesTecnico(int id, [FromBody] DetallesTecnico tec)
        {
            if (tec == null || id != tec.codDet)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este técnico");
                return StatusCode(400, ModelState);
            }

            if (!_tecServicio.ActualizarDetalleTecnico(tec.codDet, tec.direccion, tec.telefono, tec.correo))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarDetallesTecnico/{id}")]
        public IActionResult BorrarDetallesTecnico(int id)
        {
            bool resultado = _tecServicio.BorrarDetalleTecnico(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó la información consultada para este técnico");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
