using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        bool ExisteEnElSistema(string correo);
        Usuario IniciarSesion (string correo, string clave);
        bool RegistrarUsuario(string correo, string rol);
        void CambiarContrasena(string contraVieja, string contraNueva);
        void RecuperarContrasena(string correo);
        void IniciarRecuperacion();
        bool EsUsuarioUnico(string pcorreo);
    }
}
