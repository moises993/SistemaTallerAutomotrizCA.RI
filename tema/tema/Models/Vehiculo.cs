using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Vehiculo
    {
        public int IDVehiculo { get; set; }
        public int IDCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó la Marca")]
        [Display(Name = "Marca")]
        public string marca { get; set; }

        [Required(ErrorMessage = "No se ingresó el Modelo")]
        [Display(Name = "Modelo")]
        public int modelo { get; set; }

        [Required(ErrorMessage = "No se ingresó la Placa")]
        [Display(Name = "Placa")]
        public string placa { get; set; }
    }
}
