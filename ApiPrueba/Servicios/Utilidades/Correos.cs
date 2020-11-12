using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Utilidades
{
    public class Correos
    {
        public static async Task EnviarCorreo(string emailDestino, string contrasenaGenerada)
        {
            const string correo = "tallerautomotrizcari@gmail.com";
            const string contra = "tallerCarrion";
            MailMessage oMailMessage = new MailMessage(correo, emailDestino, "Contraseña de su cuenta",
                "<p>¡Hola! usted ha sido registrado en el sistema y su contraseña es: " + contrasenaGenerada + "</p>");

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
