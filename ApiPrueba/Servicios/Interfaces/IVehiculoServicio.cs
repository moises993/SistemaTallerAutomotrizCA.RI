using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IVehiculoServicio
    {
        List<Vehiculo> VerVehiculos();
        Vehiculo ConsultarVehiculoPorPlaca(string placa);
        bool PlacaExiste(string placa);
        bool RegistrarVehiculo(string pcedclt, string marca, int modelo, string placa);
        bool ActualizarVehiculo(int id, string marca, int modelo, string placa);
        bool BorrarVehiculo(int id);
    }
}
