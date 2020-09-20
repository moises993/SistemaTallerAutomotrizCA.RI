using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entidades;

namespace WebApplication1.Servicios
{
    public class ClienteServicio : HttpClientService<Cliente>
    {
        public ClienteServicio(UrlService urlService) : base(urlService) { }

        public override async Task<IEnumerable<Cliente>> GetAllAsync(string requestUri)
        {
            IEnumerable<Cliente> clientes = await base.GetAllAsync(requestUri);
            return clientes.OrderBy(c => c.Apellido1);
        } 
    }
}
