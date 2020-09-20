using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Servicios
{
    public abstract class HttpClientService<T> : IDisposable where T : class
    {
        private HttpClient _httpClient;
        private readonly UrlService _urlService;
        public HttpClientService(UrlService urlService)
        {
            _urlService = urlService ??
            throw new ArgumentNullException(nameof(urlService));
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new
            Uri(_urlService.baseUrl);
        }

        private async Task<string> GetInternalAsync(string requestUri)
        {
            if (requestUri == null) throw new
            ArgumentNullException(nameof(requestUri));
            if (_objectDisposed) throw new
            ObjectDisposedException(nameof(_httpClient));
            HttpResponseMessage resp = await _httpClient.GetAsync(requestUri);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }

        public async virtual Task<T> GetAsync(string requestUri)
        {
            if (requestUri == null) throw new
            ArgumentNullException(nameof(requestUri));
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(string requestUri)
        {
            if (requestUri == null) throw new
            ArgumentNullException(nameof(requestUri));
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        private bool _objectDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_objectDisposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                _objectDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
