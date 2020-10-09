using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface ICitaServicio
    {
        List<Cita> VerCitas();
        Cita ConsultarCitaPorId(int id);
        List<string> VerCedulasTecnico();
        bool RegistrarCita(string cedclt, int idtec, DateTime fecha, string hora, string asunto, string desc, bool conf);
        bool ActualizarCita(int pid, DateTime fecha, string hora, string asunto, string desc, bool conf);
        bool BorrarCita(int id);
    }
}
