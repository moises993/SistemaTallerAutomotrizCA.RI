using System.Collections.Generic;

namespace tema.Models.ViewModels
{
    public class TecnicoViewModel
    {
        public Tecnico tecnico { get; set; }
        public List<DetallesTecnico> detallesTecnico { get; set; }
    }
}
