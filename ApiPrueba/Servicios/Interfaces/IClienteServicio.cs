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
        List<Cliente> VerClientes();
        Cliente VerClientePorCedula(string pCedula);

        List<ClienteCita> VerClientesConCita();
    }
}
