using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tema.Models;
using tema.Utilidades.Interfaces;

namespace tema.Utilidades
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public bool Error400 { get; set; }
        public bool Error404 { get; set; }
        public bool Error409 { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        public UsuarioRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Usuario> LoginAsync(string url, Usuario objetoLogin)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (objetoLogin != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objetoLogin), Encoding.UTF8, "application/json");
            }
            else
            {
                return new Usuario();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            Error400 = trigger400(responseMessage.StatusCode);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario>(jsonString);
            }

            return new Usuario();
        }

        public async Task<bool> RegisterAsync(string url, Usuario objetoRegistrar)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (objetoRegistrar != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objetoRegistrar), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            Error400 = trigger400(responseMessage.StatusCode);
            Error404 = trigger404(responseMessage.StatusCode);
            Error409 = trigger409(responseMessage.StatusCode);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        Func<HttpStatusCode, bool> trigger400 = hsc => hsc == HttpStatusCode.BadRequest ? true : false;
        Func<HttpStatusCode, bool> trigger404 = hsc => hsc == HttpStatusCode.NotFound ? true : false;
        Func<HttpStatusCode, bool> trigger409 = hsc => hsc == HttpStatusCode.Conflict ? true : false;
    }
}
