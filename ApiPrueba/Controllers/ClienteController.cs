using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [Produces("application/json", "application/xml")]
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
            _clienteServicio.VerClientesConCita();

        [HttpGet("BuscarCliente/{cedula}")]
        public Cliente VerClientePorCedula(string cedula)
        {
            Cliente objt = _clienteServicio.VerClientePorCedula(cedula);

            if(objt == null) return null;

            return objt;
        }
    }
}
