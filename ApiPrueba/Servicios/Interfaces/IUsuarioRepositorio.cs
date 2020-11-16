using ApiPrueba.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        bool ExisteEnElSistema(string correo);
        Usuario IniciarSesion(string correo, string clave);
        Task<bool> RegistrarUsuario(string correo, string rol);
        Task<bool> CambiarContrasena(string correo);
        bool EsUsuarioUnico(string pcorreo);
        int EliminarUsuario(int? id);
        Task<List<Usuario>> VerUsuarios();
        void InsertarRegistro(string nombreAccion, string usuario);
    }
}
