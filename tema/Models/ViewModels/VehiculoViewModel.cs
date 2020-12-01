using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class VehiculoViewModel
    {
        public List<Cliente> cliente { get; set; }

        #region propieadesCrear
        public string cedclt { get; set; }
        [Required(ErrorMessage = "Se requiere de un nombre descriptivo para este vehículo")]
        [Display(Name = "Estilo")]
        [StringLength(70, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el modelo del vehículo")]
        [Display(Name = "Modelo del vehículo")]
        [RegularExpression(@"^(?:19|20)\d{2}$", ErrorMessage = "Se ha ingresado un modelo inválido. El rango va desde el año 1899 hasta el 2099")]
        public string modelo { get; set; }
        //[Required(ErrorMessage = "La placa es obligatoria para agregar un nuevo vehículo")]
        [StringLength(10, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string placa { get; set; }
        [StringLength(10, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string placaExt { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar una marca")]
        public string? fabricante { get; set; }
        #nullable disable
        #endregion propieadesCrear
    }
}
