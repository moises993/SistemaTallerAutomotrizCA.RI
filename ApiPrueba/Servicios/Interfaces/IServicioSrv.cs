using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IServicioSrv
    {
        List<Servicio> VerServicio();
        bool RegistrarServicio(int idv, string desc);
        bool ActualizarServicio(int ids, string desc);
        bool BorrarServicio(int id);
    }
}
