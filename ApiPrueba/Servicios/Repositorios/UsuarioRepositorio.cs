using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using ApiPrueba.Servicios.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public Usuario IniciarSesion(string correo, string clave)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            Usuario usr = null;
            string sal = string.Empty;

            try
            {
                conexion.Open();

                using (var comando1 = new NpgsqlCommand("\"Taller\".\"usrObtenerSal\"", conexion))
                {
                    comando1.CommandType = CommandType.StoredProcedure;
                    comando1.Parameters.AddWithValue("pcorreo", correo);

                    sal = (string)comando1.ExecuteScalar();

                    conexion.Close();
                }

                conexion.Open();

                using (var comando2 = new NpgsqlCommand("\"Taller\".\"usrBuscarUsuario\"", conexion))
                {
                    comando2.CommandType = CommandType.StoredProcedure;
                    var hash = Cifrado.GenerarHash(clave, sal);
                    comando2.Parameters.AddWithValue("phash", hash);

                    using (var lector = comando2.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            usr = new Usuario
                            {
                                IDUsuario = lector.GetInt32(0),
                                correo = lector.GetString(1).Trim(),
                                rol = lector.GetString(2).Trim()
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
            catch (Exception)
            {
                return null;
            }
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

        //Valida que al registrar, el correo sea de un técnico que no es usuario del sistema
        public bool EsUsuarioUnico(string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"usrValidarUsuarioUnico\"", conexion))
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
        public async Task<bool> RegistrarUsuario(string correo, string rol)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                using (NpgsqlCommand comando = new NpgsqlCommand("CALL \"Taller\".\"usrCrearUsuario\"(@pcorreo, @phash, @psal, @prol);", conexion))
                {
                    string claveGenerada = _gen.GenerarClave(8);
                    string sal = Cifrado.GenerarSal();
                    await Correos.EnviarCorreo(correo, claveGenerada);
                    string hash = Cifrado.GenerarHash(claveGenerada, sal);
                    comando.Parameters.AddWithValue(":pcorreo", correo);
                    comando.Parameters.AddWithValue(":prol", rol);
                    comando.Parameters.AddWithValue(":phash", hash);
                    comando.Parameters.AddWithValue(":psal", sal);
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

        public int EliminarUsuario(int? id)
        {
            int resultado = 0;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"usrEliminarUsuario\"", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("pid", id);
                resultado = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public async Task<bool> CambiarContrasena(string correo)
        {
            bool resultado = false;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"usrRenovarContra\"", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                string claveGenerada = _gen.GenerarClave(8);
                string sal = Cifrado.GenerarSal();
                await Correos.EnviarCorreo(correo, claveGenerada);
                string hash = Cifrado.GenerarHash(claveGenerada, sal);
                comando.Parameters.AddWithValue("pcorreo", correo);
                comando.Parameters.AddWithValue("pnuevohash", hash);
                comando.Parameters.AddWithValue("pnuevasal", sal);
                NpgsqlDataReader lector = comando.ExecuteReader();
                while (lector.Read()) resultado = lector.GetBoolean(0);
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
    }
}
