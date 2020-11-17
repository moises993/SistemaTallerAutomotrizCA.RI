using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface ICorreos
    {
        Task EnviarCorreo(string emailDestino, string contrasenaGenerada);
    }
}
