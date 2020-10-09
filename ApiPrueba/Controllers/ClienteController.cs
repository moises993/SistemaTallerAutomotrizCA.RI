using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ApiPrueba.Controllers
{
    //[Authorize]
    [Route("Taller/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        [HttpGet("ListaClientes")]
        public List<Cliente> VerClientes() =>
            _clienteServicio.VerClientes();

        [HttpGet("ClientesConCita")]
        public List<ClienteCita> VerClientesConCita() =>
            _clienteServicio.MostrarClientesConCita();

        [HttpGet("BuscarCliente/{cedula}")] 
        public Cliente VerClientePorCedula(string cedula)
        {
            Cliente objt = _clienteServicio.ConsultarClienteCedula(cedula);

            if(objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarCliente")]
        public IActionResult RegistrarCliente([FromBody] Cliente clt)
        {
            if (clt == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este cliente");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            bool EsCedulaUnica = _clienteServicio.CedulaExiste(clt.cedula);

            if(!EsCedulaUnica)
            {
                return BadRequest(new { message = "Existe un cliente con esta cédula" });
            }

            if (!_clienteServicio.RegistrarCliente(clt.nombre, clt.pmrApellido, clt.sgndApellido, clt.cedula, clt.cltFrecuente))
            {
                ModelState.AddModelError("", "Ocurrió un error creando al nuevo cliente. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Cliente ingresado al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarCliente(string id, [FromBody] Cliente clt)
        {
            if (clt == null || id != clt.cedula)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este cliente");
                return StatusCode(400, ModelState);
            }

            if (!_clienteServicio.ActualizarCliente(id, clt.nombre, clt.pmrApellido, clt.sgndApellido, clt.cedula, clt.cltFrecuente))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarCliente/{cedula}")]
        public IActionResult BorrarCliente(string cedula)
        {
            bool resultado = _clienteServicio.BorrarCliente(cedula);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó este cliente");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }

        /*-----------------------------------------------------------------------------------------------------------------
         ------------------------------------------DETALLES DEL CLIENTE----------------------------------------------------
         ------------------------------------------------------------------------------------------------------------------*/

        [HttpGet("ObtenerDetallesCliente/{id}")]
        public DetallesCliente VerDetallesCliente(int id)
        {
            DetallesCliente objt = _clienteServicio.VerDetallesCliente(id);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarDetallesCliente")]
        public IActionResult RegistrarDetallesCliente([FromBody] DetallesCliente clt)
        {
            if (clt == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este cliente");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            if (!_clienteServicio.RegistrarDetalleCliente(clt.IDCliente, clt.direccion, clt.telefono, clt.correo))
            {
                ModelState.AddModelError("", "Ocurrió un error insertando los detalles. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Información registrada exitosamente" });
        }

        [HttpPatch("ActualizarDetalles/{id}")]
        public IActionResult ActualizarDetallesCliente(int id, [FromBody] DetallesCliente clt)
        {
            if (clt == null || id != clt.IDCliente)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este cliente");
                return StatusCode(400, ModelState);
            }

            if (!_clienteServicio.ActualizarDetalleCliente(clt.IDCliente, clt.direccion, clt.telefono, clt.correo))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarDetallesCliente/{id}")]
        public IActionResult BorrarDetallesCliente(int id)
        {
            bool resultado = _clienteServicio.BorrarDetalleCliente(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó la información consultada para este cliente");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
