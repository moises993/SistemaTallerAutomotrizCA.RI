using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tema.Models;

namespace tema.Models.ViewModels
{
    public class CitaViewModel
    {
        private DateTime _returnDate = DateTime.MinValue;
        #region propiedadesServicio
        public int IDVehiculo { get; set; }
        public string descripcion { get; set; }
        #endregion propiedadesServicio
        #region propiedadesCrearCita
        public int IDTecnico { get; set; }
        public string cedulaCliente { get; set; }
        public DateTime fecha 
        { 
            get
            {
                return (_returnDate == DateTime.MinValue) ? DateTime.Now : _returnDate;
            }
            set
            {
                _returnDate = value;
            }   
        }
        public string hora { get; set; }
        public string asunto { get; set; }
        public bool citaConfirmada { get; set; }
        #endregion propiedadesCrearCita
        public List<Cita> cita { get; set; }
        public List<Servicio> servicio { get; set; }
        public List<Vehiculo> vehiculo { get; set; }
        public List<Cliente> cliente { get; set; }
        public List<Tecnico> tecnico { get; set; }
    }
}
