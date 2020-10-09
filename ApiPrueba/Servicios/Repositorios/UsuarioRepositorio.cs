using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using ApiPrueba.Servicios.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace ApiPrueba.Servicios.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GeneradorClaves _gen;
        private IConfiguration _configuration;
        private readonly AppSettings _appsettings;
        private readonly string connectionString;

        public UsuarioRepositorio(IConfiguration configuration, IOptions<AppSettings> appsettings, GeneradorClaves gen)
        {
            _configuration = configuration;
            _appsettings = appsettings.Value;
            _gen = gen;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        public void CambiarContrasena(string contraVieja, string contraNueva)
        {
            throw new NotImplementedException();
        }

        public void IniciarRecuperacion()
        {
            throw new NotImplementedException();
        }

        public Usuario IniciarSesion(string correo, string clave)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            Usuario usr = null;

            try
            {
                conexion.Open();

                using(var comando = new NpgsqlCommand("\"Taller\".\"usrBuscarUsuario\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcorreo", correo);
                    const string llave = "iohgisfg8709254ytgsfjlvhsd89";
                    comando.Parameters.AddWithValue("pclave", Cifrado.Cifrar(clave, llave));

                    using(var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            usr = new Usuario
                            {
                                IDUsuario = lector.GetInt32(0),
                                correo = lector.GetString(1).Trim(),
                                clave = lector.GetString(2).Trim(),
                                rol = lector.GetString(3).Trim()
                            };
                        }
                        lector.Close();
                    }
                    conexion.Close();
                }

                if (usr == null) return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, usr.correo),
                    new Claim(ClaimTypes.Role, usr.rol)
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                usr.tokenSesion = tokenHandler.WriteToken(token);

                return usr;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void RecuperarContrasena(string correo)
        {
            throw new NotImplementedException();
        }

        #region bloqueValidacionesRegistro

        //Valida que, al registrar, el correo exista en el sistema
        public bool ExisteEnElSistema(string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"usrValidarCorreo\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcorreo", pcorreo);

                    using(var lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            resultado = lector.GetBoolean(0); //el contenido de la respuesta, sea true o false
                        }

                        lector.Close();
                    }
                    conexion.Close();
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Valida que al registrar, el correo sea de un técnico que no es usuario del sistema
        public bool EsUsuarioUnico(string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();

                using(var comando = new NpgsqlCommand("\"Taller\".\"usrValidarUsuarioUnico\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcorreo", pcorreo);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            resultado = lector.GetBoolean(0); //el contenido de la respuesta, sea true o false
                        }

                        lector.Close();
                    }
                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //efectúa el registro con clave autogenerada y encriptado SHA256
        public bool RegistrarUsuario(string correo, string rol)
        {
            const string llave = "iohgisfg8709254ytgsfjlvhsd89";
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            string claveGenerada = _gen.GenerarClave(8);
            string claveEncriptada = Cifrado.Cifrar(claveGenerada, llave);

            try
            {
                conexion.Open();

                using(var comando = new NpgsqlCommand("CALL \"Taller\".\"usrCrearUsuario\"(@pcorreo, @pclave, @prol);", conexion))
                {
                    comando.Parameters.AddWithValue(":pcorreo", correo);
                    comando.Parameters.AddWithValue(":pclave", claveEncriptada);
                    comando.Parameters.AddWithValue(":prol", rol);

                    comando.ExecuteNonQuery();

                    conexion.Close();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion bloqueValidacionesRegistro
    }
}
