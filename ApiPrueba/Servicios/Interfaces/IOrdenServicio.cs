using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba.Entidades;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IOrdenServicio
    {
        List<Orden> VerOrdenes();
        Orden ConsultarOrdenPorCedula(string ced);
        bool? RegistrarOrden(int pidcita, int pidcliente, string pdesc);
        bool ActualizarOrden(int id, string pced, string placa);
        bool BorrarOrden(int id);
    }
}
