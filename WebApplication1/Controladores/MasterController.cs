using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entidades;
using WebApplication1.Servicios;

namespace WebApplication1.Controladores
{
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
