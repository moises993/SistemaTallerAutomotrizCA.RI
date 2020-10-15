using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Utilidades
{
    public class Correos
    {
        public static async Task EnviarCorreo(
            string emailFormulario, string contrasena, string emailDestino, string contrasenaGenerada, string rol)
        {
            MailMessage oMailMessage = new MailMessage(emailFormulario, emailDestino, "Contraseña de su cuenta",
                "<p>¡Hola! usted ha sido registrado en el sistema y su contraseña es: " + contrasenaGenerada + "</p>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
           
            oSmtpClient.Port = 587;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailFormulario, contrasena);
            oSmtpClient.EnableSsl = true;

            await oSmtpClient.SendMailAsync(oMailMessage);

            oSmtpClient.Dispose();
        }
    }
}
