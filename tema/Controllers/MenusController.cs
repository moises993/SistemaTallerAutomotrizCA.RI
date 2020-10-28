using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElectronNET.API;
using ElectronNET.API.Entities;

namespace tema.Controllers
{
    public class MenusController : Controller
    {
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
    }
}
