using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class CitaViewModel
    {
        private DateTime? _returnDate = DateTime.MinValue;
        #region propiedadesServicio
        [Required(ErrorMessage = "Debe seleccionar un vehículo")]
        public int? IDVehiculo { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar un servicio")]
        public string? descripcion { get; set; }
        #nullable disable
        #endregion propiedadesServicio
        #region propiedadesCrearCita
        [Required(ErrorMessage = "Debe seleccionar un técnico")]
        public int? IDTecnico { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        public string? cedulaCliente { get; set; }
        #nullable disable
        [Display(Name = "Fecha de la cita")]
        [Required(ErrorMessage = "Debe indicar la fecha de la cita")]
        public DateTime? fecha 
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
        [Display(Name = "Hora de la cita")]
        [Required(ErrorMessage = "No se indicó la hora de la cita")]
        public string hora { get; set; }
        [Display(Name = "Asunto de la cita")]
        [Required(ErrorMessage = "No se indicó el asunto de la cita")]
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
