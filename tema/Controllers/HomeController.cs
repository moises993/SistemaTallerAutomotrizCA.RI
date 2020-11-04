using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

                Electron.Menu.SetApplicationMenu(menu);
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

                Electron.Menu.SetApplicationMenu(menu);
            }
            return View(objeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario objeto)
        {
            if(!ModelState.IsValid)
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
                correoForm = objetoVM.correoForm,
                contra = objetoVM.contra,
                correo = objetoVM.correo,
                rol = objetoVM.rol
            };

            bool resultado = await _usRep.RegisterAsync(baseurl + "Taller/Usuario/Registrar", objeto);

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
    }
}
