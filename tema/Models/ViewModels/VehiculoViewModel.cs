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
        [Display(Name = "Marca del vehículo")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Se requiere el modelo del vehículo")]
        [Display(Name = "Modelo del vehículo")]
        [RegularExpression(@"^[0-9]{1,4}$", ErrorMessage = "Solo puede ingresar números enteros, máximo 4")]
        public string modelo { get; set; }
        //[Required(ErrorMessage = "La placa es obligatoria para agregar un nuevo vehículo")]
        public string placa { get; set; }
        public string placaExt { get; set; }
        #endregion propieadesCrear
    }
}
