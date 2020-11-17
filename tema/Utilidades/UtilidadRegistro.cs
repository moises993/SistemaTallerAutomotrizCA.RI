using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using tema.Models;

namespace tema.Utilidades
{
    public class UtilidadRegistro
    {
        

        public static async void Registrar(string metodo, string usuario)
        {
            Bitacora bcr = new Bitacora { nombre = metodo, usuarioCrea = usuario };
            string baseurl = "https://localhost:44300/";
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseurl);
            string myContent = JsonConvert.SerializeObject(bcr);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await cliente.PostAsync("Taller/Usuario/InsertarRegistro", byteContent);
        }
    }
}
