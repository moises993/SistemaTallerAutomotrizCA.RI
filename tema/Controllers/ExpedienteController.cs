using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tema.Models;
using tema.Models.ViewModels;

namespace tema.Controllers
{
    [Authorize]
    public class ExpedienteController : Controller
    {
        string baseurl = "https://localhost:44300/";
    }
}
