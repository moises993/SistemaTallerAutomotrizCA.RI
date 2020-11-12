using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Expediente
    {
        [Display(Name = "Código del expediente")]
        public int IDExpediente { get; set; }
        public int IDVehiculo { get; set; }
        //[Required(ErrorMessage = "No se ingresó el nombre del técnico.")]
        [Display(Name = "Nombre del técnico")]
        public string nombreTecnico { get; set; }
        //[Required(ErrorMessage = "No se ingresó el asunto.")]
        [Display(Name = "Motivo de registro")]
        public string asunto { get; set; }
        //[Required(ErrorMessage = "No se ingresó la descripción.")]
        [Display(Name = "Descripción de los servicios aplicados")]
        public string descripcion { get; set; }
        //[Required(ErrorMessage = "No se ingresó la fecha de creación del expediente.")]
        [Display(Name = "Fecha de creación del expediente")]
        public DateTime fechaCreacionExp { get; set; }
        //[Required(ErrorMessage = "No se ingresó el nombre del Cliente.")]
        [Display(Name = "Nombre del cliente")]
        public string nombreCliente { get; set; }
        //[Required(ErrorMessage = "No se ingresó la marca.")]
        [Display(Name = "Marca")]
        public string marca { get; set; }
        //[Required(ErrorMessage = "No se ingresó el modelo.")]
        [Display(Name = "Modelo")]
        public int modelo { get; set; }
        //[Required(ErrorMessage = "No se ingresó la placa.")]
        [Display(Name = "Placa")]
        public string placa { get; set; }
    }
}

