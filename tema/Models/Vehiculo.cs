using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class Vehiculo
    {
        public string cedclt{ get; set; }
        public int IDVehiculo { get; set; }
        public int IDCliente { get; set; }
        [Display(Name = "Marca y estilo del vehículo")]
        [Required(ErrorMessage = "Debe proveer un valor para este campo")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el modelo del vehículo")]
        [Display(Name = "Modelo del vehículo")]
        [RegularExpression(@"^[0-9]{1,4}$", ErrorMessage = "Solo puede ingresar números enteros, máximo 4")] // (?:19|20)\d{2}  /^$/
        public int modelo { get; set; }
        public string placa { get; set; }
        
    }

    public class VehiculoEdit
    {
        public int IDVehiculo { get; set; }

        [Display(Name = "Marca del vehículo")]
        [Required(ErrorMessage = "Debe proveer un valor para este campo")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el modelo del vehículo")]
        [Display(Name = "Modelo del vehículo")]
        [RegularExpression(@"^(?:19|20)\d{2}$", ErrorMessage = "Se ha ingresado un modelo inválido. El rango va desde el año 1899 hasta el 2099")]
        public string modelo { get; set; }
        public string placa { get; set; }
    }
}
