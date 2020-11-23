using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class Servicio
    {
        public int IDServicio { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un vehículo")]
        public int? IDVehiculo { get; set; }
        #nullable enable
        [Required(ErrorMessage = "La descripción no puede estar vacía")]
        public string? descripcion { get; set; }
        #nullable disable
    }

    public class ServicioEdit
    {
        public int IDServicio { get; set; }
        #nullable enable
        [Required(ErrorMessage = "La descripción no puede estar vacía")]
        public string? descripcion { get; set; }
        #nullable disable
    }
}
