using ApiPrueba.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        bool ExisteEnElSistema(string correo);
        Usuario IniciarSesion(string correo, string clave);
        Task<bool> RegistrarUsuario(string correoForm, string contra, string correo, string rol);
        void RecuperarContrasena(string correo);
        void IniciarRecuperacion();
        bool EsUsuarioUnico(string pcorreo);
    }
}
