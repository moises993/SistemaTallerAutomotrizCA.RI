using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class Vehiculo
    {
        public string cedclt{ get; set; }
        public int IDVehiculo { get; set; }
        public int IDCliente { get; set; }
        [Display(Name = "Marca del vehículo")]
        [Required(ErrorMessage = "Debe proveer un valor para este campo")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el modelo del vehículo")]
        [Display(Name = "Modelo del vehículo")]
        [RegularExpression(@"^[0-9]{1,4}$", ErrorMessage = "Solo puede ingresar números enteros, máximo 4")]
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
        [RegularExpression(@"^[0-9]{1,4}$", ErrorMessage = "Solo puede ingresar números enteros, máximo 4")]
        public string modelo { get; set; }
        public string placa { get; set; }
    }
}
