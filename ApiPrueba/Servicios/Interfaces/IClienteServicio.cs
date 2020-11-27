using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<List<Cliente>> VerClientes();
        Task<Cliente> ConsultarClienteCedula(string pCedula);
        //Task<List<ClienteCita>> MostrarClientesConCita();
        bool RegistrarCliente(string nmb, string ap1, string ap2, string ced, bool frec);
        bool ActualizarCliente(string id, string nmb, string ap1, string ap2, string ced, bool frec);
        bool BorrarCliente(string ced);
        bool CedulaExiste(string ced);
        List<DetallesCliente> VerDetallesCliente(string cedula);
        DetallesCliente VerDetalleIndividual(int id);
        bool RegistrarDetalleCliente(int id, string pdireccion, string ptelefono, string pcorreo);
        bool ActualizarDetalleCliente(int idcliente, string pdireccion, string ptelefono, string pcorreo, int id);
        bool BorrarDetalleCliente(int id);
    }
}
