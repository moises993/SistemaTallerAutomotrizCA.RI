using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entidades;
using WebApplication1.Servicios;

namespace WebApplication1.Controladores
{
    public class ClienteController : Controller
    {
        private readonly UrlService _urlService;
        private readonly ClienteServicio _clienteServicio;

        public ClienteController(UrlService urlService, ClienteServicio clienteServicio)
        {
            _urlService = urlService;
            _clienteServicio = clienteServicio;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Cliente> clientes = await _clienteServicio.GetAllAsync(_urlService.ModCliente + "ListaClientes");
            return View(clientes);
        }
    }
}
