using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using tema.Models;
using tema.Models.ViewModels;
using tema.Utilidades.Interfaces;

namespace tema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepositorio _usRep;
        string baseurl = "https://localhost:44300/";

        public HomeController(ILogger<HomeController> logger, IUsuarioRepositorio usRep)
        {
            _logger = logger;
            _usRep = usRep;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (HybridSupport.IsElectronActive)
            {
                var menu = new MenuItem[]
                {
                    new MenuItem
                    {
                        Label = "Archivo", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Salir", Accelerator = "CmdOrCtrl+O",
                                Click = () =>
                                {
                                    Electron.App.Exit();
                                },
                                Role = MenuRole.quit
                            }
                        }
                    }, //Fin del menú Archivo
                    new MenuItem
                    {
                        Label = "Editar", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Deshacer", Accelerator = "CmdOrCtrl+Z", Role = MenuRole.undo },
                            new MenuItem { Label = "Rehacer", Accelerator = "Shift+CmdOrCtrl+Z", Role = MenuRole.redo },
                            new MenuItem { Type = MenuType.separator },
                            new MenuItem { Label = "Cortar", Accelerator = "CmdOrCtrl+X", Role = MenuRole.cut },
                            new MenuItem { Label = "Copiar", Accelerator = "CmdOrCtrl+C", Role = MenuRole.copy },
                            new MenuItem { Label = "Pegar", Accelerator = "CmdOrCtrl+V", Role = MenuRole.paste },
                            new MenuItem { Label = "Seleccionar todo", Accelerator = "CmdOrCtrl+A", Role = MenuRole.selectall }
                        }
                    }, //Fin del menú Editar
                    new MenuItem
                    {
                        Label = "Ver", Submenu = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Label = "Refrescar (cerrar ventanas extra)",
                                Accelerator = "CmdOrCtrl+R",
                                Click = () =>
                                {
                                    // on reload, start fresh and close any old
                                    // open secondary windows
                                    Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow => {
                                        if(browserWindow.Id != 1)
                                        {
                                            browserWindow.Close();
                                        }
                                        else
                                        {
                                            browserWindow.Reload();
                                        }
                                    });
                                }
                            },
                            new MenuItem
                            {
                                Label = "Ejecutar DevTools",
                                Accelerator = "CmdOrCtrl+I",
                                Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools()
                            }
                        }
                    }, //Fin del menú Ver
                    new MenuItem
                    {
                        Label = "Ventana", Role = MenuRole.window, Submenu = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Label = "Abrir nueva ventana",
                                Accelerator = "CmdOrCtrl+Alt+V",
                                Click = async () =>
                                {
                                    await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
                                    {
                                        Width = 1280,
                                        Height = 1024
                                    });
                                }
                            },
                            new MenuItem { Label = "Minimizar", Accelerator = "CmdOrCtrl+M", Role = MenuRole.minimize },
                            new MenuItem { Label = "Cerrar", Accelerator = "CmdOrCtrl+W", Role = MenuRole.close },
                            new MenuItem
                            {
                                Label = "Activar/desactivar pantalla completa", Accelerator = "CmdOrCtrl+F",
                                Click = async () =>
                                {
                                    bool isFullScreen = await Electron.WindowManager.BrowserWindows.First().IsFullScreenAsync();
                                    Electron.WindowManager.BrowserWindows.First().SetFullScreen(!isFullScreen);
                                }
                            }
                        }
                    }  //Fin del menú "Ventana"
                };

                //Electron.Menu.SetApplicationMenu(menu);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            Usuario objeto = new Usuario();
            if (HybridSupport.IsElectronActive)
            {
                var menu = new MenuItem[]
                {
                    new MenuItem
                    {
                        Label = "Archivo", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Salir", Accelerator = "CmdOrCtrl+O",
                                Click = () =>
                                {
                                    Electron.App.Exit();
                                },
                                Role = MenuRole.quit
                            }
                        }
                    }, //Fin del menú Archivo
                    new MenuItem
                    {
                        Label = "Editar", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Deshacer", Accelerator = "CmdOrCtrl+Z", Role = MenuRole.undo },
                            new MenuItem { Label = "Rehacer", Accelerator = "Shift+CmdOrCtrl+Z", Role = MenuRole.redo },
                            new MenuItem { Type = MenuType.separator },
                            new MenuItem { Label = "Cortar", Accelerator = "CmdOrCtrl+X", Role = MenuRole.cut },
                            new MenuItem { Label = "Copiar", Accelerator = "CmdOrCtrl+C", Role = MenuRole.copy },
                            new MenuItem { Label = "Pegar", Accelerator = "CmdOrCtrl+V", Role = MenuRole.paste },
                            new MenuItem { Label = "Seleccionar todo", Accelerator = "CmdOrCtrl+A", Role = MenuRole.selectall }
                        }
                    }, //Fin del menú Editar
                    new MenuItem
                    {
                        Label = "Ver", Submenu = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Label = "Refrescar (cerrar ventanas extra)",
                                Accelerator = "CmdOrCtrl+R",
                                Click = () =>
                                {
                                    // on reload, start fresh and close any old
                                    // open secondary windows
                                    Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow => {
                                        if(browserWindow.Id != 1)
                                        {
                                            browserWindow.Close();
                                        }
                                        else
                                        {
                                            browserWindow.Reload();
                                        }
                                    });
                                }
                            },
                            new MenuItem
                            {
                                Label = "Ejecutar DevTools",
                                Accelerator = "CmdOrCtrl+I",
                                Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools()
                            }
                        }
                    }, //Fin del menú Ver
                    new MenuItem
                    {
                        Label = "Ventana", Role = MenuRole.window, Submenu = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Label = "Abrir nueva ventana",
                                Accelerator = "CmdOrCtrl+Alt+V",
                                Click = async () =>
                                {
                                    await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
                                    {
                                        Width = 1280,
                                        Height = 1024
                                    });
                                }
                            },
                            new MenuItem { Label = "Minimizar", Accelerator = "CmdOrCtrl+M", Role = MenuRole.minimize },
                            new MenuItem { Label = "Cerrar", Accelerator = "CmdOrCtrl+W", Role = MenuRole.close },
                            new MenuItem
                            {
                                Label = "Activar/desactivar pantalla completa", Accelerator = "CmdOrCtrl+F",
                                Click = async () =>
                                {
                                    bool isFullScreen = await Electron.WindowManager.BrowserWindows.First().IsFullScreenAsync();
                                    Electron.WindowManager.BrowserWindows.First().SetFullScreen(!isFullScreen);
                                }
                            }
                        }
                    }  //Fin del menú "Ventana"
                };

                //Electron.Menu.SetApplicationMenu(menu);
            }
            return View(objeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario objeto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Usuario objLogin = new Usuario
            {
                correo = objeto.correo.Trim(),
                clave = objeto.clave.Trim()
            };

            Usuario objetoUsuario = await _usRep.LoginAsync(baseurl + "Taller/Usuario/IniciarSesion", objLogin);

            if (_usRep.Error400)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objetoUsuario.correo));
            identity.AddClaim(new Claim(ClaimTypes.Role, objetoUsuario.rol));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objetoUsuario.tokenSesion);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "manager")]
        public IActionResult Register()
        {
            return View();
        }

        public static List<SelectListItem> ObtenerRoles()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            ls.Add(new SelectListItem() { Text = "Administrador", Value = "manager" });
            ls.Add(new SelectListItem() { Text = "Técnico", Value = "tecnico" });
            return ls;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuarioViewModel objetoVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Usuario objeto = new Usuario
            {
                correo = objetoVM.correo,
                rol = objetoVM.rol
            };

            await _usRep.RegisterAsync(baseurl + "Taller/Usuario/Registrar", objeto);

            if (_usRep.Error400)
            {
                ModelState.AddModelError(string.Empty, "Este correo le pertenece a un usuario");
                return View();
            }

            if (_usRep.Error404)
            {
                ModelState.AddModelError(string.Empty, "Este correo no existe en el sistema");
                return View();
            }

            if (_usRep.Error409)
            {
                ModelState.AddModelError(string.Empty, "Error interno");
                return View();
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> VerUsuarios()
        {
            IEnumerable<Usuario> aux = new List<Usuario>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync("Taller/Usuario/ListaUsuarios");
            if (res.IsSuccessStatusCode) aux = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(res.Content.ReadAsStringAsync().Result);
            return View(aux);
        }

        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> RecuperarAcceso([Bind("correo")] UsuarioRecAccess usr)
        {
            if(ModelState.IsValid)
            {
                Usuario usrpost = new Usuario { correo = usr.correo };
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseurl);
                string myContent = JsonConvert.SerializeObject(usrpost);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage postTask = await client.PostAsync("Taller/Usuario/RecuperarAcceso", byteContent);
                HttpResponseMessage result = postTask;
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("", "No se halló el correo ingresado");
                }
                if (result.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "No se pudo cambiar la contraseña");
                }
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return View(usr);
        }

        public class UsuarioRecAccess
        {
            [DataType(DataType.EmailAddress, ErrorMessage = "El correo tiene un formato inválido")]
            [Required(ErrorMessage = "No se ingresó el correo")]
            public string correo { get; set; }
        }
    }
}
