using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IExpedienteServicio
    {
        List<Expediente> VerExpedientes();
        Expediente ConsultarExpedientePorPlaca(string placa);
        bool RegistrarExpediente(int pidv);
        bool ActualizarExpediente(int pid, string desc);
        bool BorrarExpediente(int pid);
    }
}
