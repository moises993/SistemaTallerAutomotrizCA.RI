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
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoServicio _vhcServicio;

        public VehiculoController(IVehiculoServicio vhcServicio)
        {
            _vhcServicio = vhcServicio;
        }

        [HttpGet("ListaVehiculos")]
        public List<Vehiculo> VerVehiculos() =>
            _vhcServicio.VerVehiculos();

        [HttpGet("BuscarVehiculo/{placa}")]
        public Vehiculo VerVehiculoPorPlaca(string placa)
        {
            Vehiculo objt = _vhcServicio.ConsultarVehiculoPorPlaca(placa);

            if (objt == null) return null;

            return objt;
        }

        [HttpPost("RegistrarVehiculo")]
        public IActionResult RegistrarVehiculo([FromBody] Vehiculo vhc)
        {
            if (vhc == null)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este vehículo");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "La información ingresada presenta errores. Corríjalos e inténtelo otra vez");
                return StatusCode(400, ModelState);
            }

            bool EsPlacaUnica = _vhcServicio.PlacaExiste(vhc.placa);
            if(!EsPlacaUnica) return BadRequest(new { message = "Existe un vehículo con esta placa" });

            if (!_vhcServicio.RegistrarVehiculo(vhc.cedclt, vhc.marca, vhc.modelo, vhc.placa))
            {
                ModelState.AddModelError("", "Ocurrió un error creando al nuevo vehículo. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Vehiculo ingresado al sistema exitosamente" });
        }

        [HttpPatch("ActualizarDatos/{id}")]
        public IActionResult ActualizarVehiculo(int id, [FromBody] Vehiculo vhc)
        {
            if (vhc == null || id != vhc.IDVehiculo)
            {
                ModelState.AddModelError("", "No se digitó toda la información requerida para este vehículo");
                return StatusCode(400, ModelState);
            }

            if (!_vhcServicio.ActualizarVehiculo(id, vhc.marca, vhc.modelo, vhc.placa))
            {
                ModelState.AddModelError("", "Ocurrió un error durante la actualización. Por favor, inténtelo en un momento");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Datos actualizados con éxito" });
        }

        [HttpDelete("BorrarVehiculo/{id}")]
        public IActionResult BorrarVehiculo(int id)
        {
            bool resultado = _vhcServicio.BorrarVehiculo(id);

            if (!resultado)
            {
                ModelState.AddModelError("", "No se eliminó este vehículo");
                return StatusCode(400, ModelState);
            }

            return Ok();
        }
    }
}
