using System.Collections.Generic;

namespace tema.Models.ViewModels
{
    public class ClienteViewModel
    {
        public Cliente cliente { get; set; }
        public List<DetallesCliente> detallesCliente { get; set; }
    }
}
