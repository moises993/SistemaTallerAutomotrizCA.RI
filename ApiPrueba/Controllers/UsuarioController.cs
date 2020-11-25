using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPrueba.Controllers
{
    [Authorize]
    [Route("Taller/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usrRepo;

        public UsuarioController(IUsuarioRepositorio usrRepo)
        {
            _usrRepo = usrRepo;
        }

        [AllowAnonymous]
        [HttpPost("IniciarSesion")]
        public IActionResult IniciarSesion([FromBody] Usuario modelo)
        {
            var usuario = _usrRepo.IniciarSesion(modelo.correo, modelo.clave);

            if (usuario == null)
            {
                return BadRequest(new { message = "Usuario o contraseña incorrectos" });
            }

            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> Register([FromBody] Usuario modelo)
        {
            bool Existe = _usrRepo.ExisteEnElSistema(modelo.correo);
            bool EsUnico = _usrRepo.EsUsuarioUnico(modelo.correo);

            if (!Existe)
            {
                return NotFound(new { message = "El correo suministrado no existe" });
            }

            if (!EsUnico)
            {
                return BadRequest(new { message = "Existe un usuario con este correo" });
            }

            bool seCreoUsuario = await _usrRepo.RegistrarUsuario(modelo.correo, modelo.rol);

            if (!seCreoUsuario)
            {
                return Conflict(new { message = "Error registrando este usuario" });
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("EliminarUsuario/{id}")]
        public IActionResult EliminarUsuario(int? id)
        {
            _usrRepo.EliminarUsuario(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("RecuperarAcceso")]
        public async Task<IActionResult> RecuperarAcceso([FromBody] Usuario modelo)
        {
            bool resultado;
            bool Existe = _usrRepo.ExisteEnElSistema(modelo.correo);
            if (!Existe) return NotFound(new { message = "El correo suministrado no existe" });
            else resultado = await _usrRepo.CambiarContrasena(modelo.correo);
            if (resultado) return Ok();
            else return StatusCode(500, "Error cambiando la clave");
        }

        [AllowAnonymous]
        [HttpGet("ListaUsuarios")]
        public async Task<List<Usuario>> VerUsuarios() => await _usrRepo.VerUsuarios();

        [AllowAnonymous]
        [HttpPost("InsertarRegistro")]
        public void InsertarRegistro([FromBody] Bitacora bcr) => _usrRepo.InsertarRegistro(bcr.usuario, bcr.tabla, bcr.controlador, bcr.accion); 
    }
}
