using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Register([FromBody] Usuario modelo)
        {
            bool Existe = _usrRepo.ExisteEnElSistema(modelo.correo);
            bool EsUnico = _usrRepo.EsUsuarioUnico(modelo.correo);

            if (!Existe)
            {
                return NotFound(new { message = "El correo suministrado no existe" });
            }

            if(!EsUnico)
            {
                return BadRequest(new { message = "Existe un usuario con este correo" });
            }

            bool seCreoUsuario = _usrRepo.RegistrarUsuario(modelo.correo, modelo.rol);

            if (!seCreoUsuario)
            {
                return Conflict(new { message = "Error registrando este usuario" });
            }

            return Ok();
        }
    }
}
