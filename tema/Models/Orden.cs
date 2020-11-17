using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Orden
    {
        [Display(Name = "Orden")]
        public int IDOrden { get; set; }
        [Display(Name = "Vehículo")]
        public int IDVehiculo { get; set; }
        [Required(ErrorMessage = "No se ingresó la Cédula")]
        [Display(Name = "Cédula")]
        public string cedulaCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó la placa")]
        [Display(Name = "Placa")]
        public string placaVehiculo { get; set; }
        [Required(ErrorMessage = "No se ingresó la descripción del servicio")]
        [Display(Name = "Descripción del servicio")]
        public string descServicio { get; set; }
    }
}
