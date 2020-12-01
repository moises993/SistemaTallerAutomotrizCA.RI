using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Utilidades
{
    public class Correos : ICorreos
    {
        private IWebHostEnvironment _env;

        public Correos(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        public async Task EnviarCorreo(string emailDestino, string contrasenaGenerada)
        {
            const string correo = "tallerautomotrizcari@gmail.com";
            const string contra = "eLtALlerAut0CA.RI";

            string ruta = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "imageonline-co-whitebackgroundremoved.jpeg");
            LinkedResource imagen = new LinkedResource(ruta, MediaTypeNames.Image.Jpeg);
            imagen.ContentId = "logo_taller";

            string mensaje = "<br /><img src='cid:logo_taller' width='99' height='66' />" + //width='99' height='66'
                @"<p>
                      Taller Automotriz CA.RI<br/>
                      San José, León Cortés, San Andrés<br/>
                      Teléfono: 8355 - 1192<br/>
                  </p> " +
                "<p>Reciba un cordial saludo de parte del taller.<br />Su contraseña para ingresar al sistema es: " + contrasenaGenerada + "</p>"
                + "<p>Esta contraseña tiene validez durante 1 hora. Debe cambiarla en cuanto acceda al sistema.</p>";

            AlternateView altView = AlternateView.CreateAlternateViewFromString(mensaje, null, MediaTypeNames.Text.Html);
            altView.LinkedResources.Add(imagen);
            MailMessage oMailMessage = new MailMessage(correo, emailDestino, "Contraseña de su cuenta", mensaje);
            oMailMessage.AlternateViews.Add(altView);
            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.Port = 587;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(correo, contra);
            oSmtpClient.EnableSsl = true;
            await oSmtpClient.SendMailAsync(oMailMessage);
            oSmtpClient.Dispose();
        }
    }
}
