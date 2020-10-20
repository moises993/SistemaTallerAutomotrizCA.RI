using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tema.Models;

namespace tema.Utilidades.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        bool Error400 { get; set; }
        bool Error404 { get; set; }
        bool Error409 { get; set; }
        Task<Usuario> LoginAsync(string url, Usuario objetoLogin);
        Task<bool> RegisterAsync(string url, Usuario objetoRegistrar);
    }
}
