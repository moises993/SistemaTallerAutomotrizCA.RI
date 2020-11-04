using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [Route("Taller/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenServicio _odn;

        public OrdenController(IOrdenServicio odn)
        {
            _odn = odn;
        }

        [HttpGet("ListaOrdenes")]
        public List<Orden> VerOrdenes() =>
            _odn.VerOrdenes();

        [HttpGet("BuscarOrden/{cedula}")]
        public Orden VerOrdenPorCedula(string cedula)
        {
            Orden objt = _odn.ConsultarOrdenPorCedula(cedula);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarOrden")]
        public IActionResult RegistrarOrden([FromBody] ParametrosOrden odn)
        {
            if (odn == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para esta orden");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if ((bool)_odn.RegistrarOrden(odn.IDCita, odn.IDCliente, odn.descripcion) == false)
            {
                HttpResponseMessage mensaje = new HttpResponseMessage();
                mensaje.Content = new StringContent("ya_existe");
                return StatusCode(400, mensaje);
            }

            return Ok(new { message = "Orden ingresada al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarOrden(int id, [FromBody] Orden odn)
        {
            if (odn == null || id != odn.IDOrden)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para esta orden");
                return StatusCode(400, ModelState);
            }

            if (!_odn.ActualizarOrden(id, odn.cedulaCliente, odn.placaVehiculo))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarOrden/{id}")]
        public IActionResult BorrarOrden(int id)
        {
            bool resultado = _odn.BorrarOrden(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó esta orden");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
