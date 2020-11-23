using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Repositorios
{
    public class CitaServicio : ICitaServicio
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public CitaServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Cita> VerCitas()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Cita> ListaCitas = new List<Cita>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"ctaVerCitas\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Cita cta = new Cita
                            {
                                IDCita = lector.GetInt32(0),
                                IDTecnico = lector.GetInt32(1),
                                cedulaCliente = lector.GetString(2).Trim(),
                                fecha = lector.GetDateTime(3),
                                hora = lector.GetString(4).Trim(),
                                asunto = lector.GetString(5).Trim(),
                                descripcion = lector.GetString(6).Trim(),
                                citaConfirmada = lector.GetBoolean(7),
                                IDCliente = lector.GetInt32(8)
                            };

                            ListaCitas.Add(cta);
                        }
                        lector.Close();
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return ListaCitas;
        }

        public Cita ConsultarCitaPorId(int id)
        {
            Cita salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"ctaVerCitaPorId\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            salida = new Cita
                            {
                                IDCita = lector.GetInt32(0),
                                IDTecnico = lector.GetInt32(1),
                                cedulaCliente = lector.GetString(2).Trim(),
                                fecha = lector.GetDateTime(3),
                                hora = lector.GetString(4).Trim(),
                                asunto = lector.GetString(5).Trim(),
                                descripcion = lector.GetString(6).Trim(),
                                citaConfirmada = lector.GetBoolean(7),
                                IDCliente = lector.GetInt32(8)
                            };
                        }

                        lector.Close();
                    }
                }

                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return salida;
        }

        public List<string> VerCedulasTecnico()
        {
            string cedula = "";
            List<string> salida = new List<string>();
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"ctaVerCedulasTecnico\"()", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            cedula = lector.GetString(0).Trim();
                            salida.Add(cedula);
                        }

                        lector.Close();
                    }
                }

                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return salida;
        }

        #endregion consultas

        #region operaciones
        public bool RegistrarCita(string cedclt, int idtec, DateTime fecha, string hora, string asunto, string desc, bool conf)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;
            try
            {
                conexion.Open();
                NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"ctaFnCrearCita\"", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("pced", cedclt);
                comando.Parameters.AddWithValue("pidtec", idtec);
                comando.Parameters.AddWithValue("pfecha", NpgsqlDbType.Date, fecha);
                comando.Parameters.AddWithValue("phora", hora);
                comando.Parameters.AddWithValue("pasunto", asunto);
                comando.Parameters.AddWithValue("pdesc", desc);
                comando.Parameters.AddWithValue("pconf", conf);
                NpgsqlDataReader lector = comando.ExecuteReader();
                while (lector.Read()) resultado = lector.GetBoolean(0);
                conexion.Close();
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarCita(int pid, DateTime fecha, string hora, string asunto, string desc, bool conf)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"ctaFnActualizarCita\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", pid);
                    comando.Parameters.AddWithValue("pfecha", NpgsqlDbType.Date, fecha);
                    comando.Parameters.AddWithValue("phora", hora);
                    comando.Parameters.AddWithValue("pasunto", asunto);
                    comando.Parameters.AddWithValue("pdesc", desc);
                    comando.Parameters.AddWithValue("pconf", conf);

                    NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read()) resultado = lector.GetBoolean(0);

                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BorrarCita(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"ctaEliminarCita\"(@pid)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", id);

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
        #endregion operaciones
    }
}
