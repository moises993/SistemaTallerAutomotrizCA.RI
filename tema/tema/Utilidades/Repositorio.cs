using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using tema.Utilidades.Interfaces;

namespace tema.Utilidades
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repositorio(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
