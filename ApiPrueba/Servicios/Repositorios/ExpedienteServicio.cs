using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiPrueba.Servicios.Repositorios
{
    public class ExpedienteServicio : IExpedienteServicio
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public ExpedienteServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Expediente> VerExpedientes()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Expediente> ListaExpedientes = new List<Expediente>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"expVerExpedientes\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Expediente exp = new Expediente
                            {
                                IDExpediente = lector.GetInt32(0),
                                IDVehiculo = lector.GetInt32(1),
                                nombreTecnico = lector.GetString(2).Trim(),
                                asunto = lector.GetString(3).Trim(),
                                descripcion = lector.GetString(4).Trim(),
                                fechaCreacionExp = lector.GetDateTime(5),
                                nombreCliente = lector.GetString(6).Trim(),
                                marca = lector.GetString(7).Trim(),
                                modelo = lector.GetInt32(8),
                                placa = lector.GetString(9).Trim()
                            };

                            ListaExpedientes.Add(exp);
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

            return ListaExpedientes;
        }

        public Expediente ConsultarExpedientePorPlaca(string placa)
        {
            Expediente salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"expVerExpPorPlaca\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pplaca", placa);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            salida = new Expediente
                            {
                                IDExpediente = lector.GetInt32(0),
                                IDVehiculo = lector.GetInt32(1),
                                nombreTecnico = lector.GetString(2).Trim(),
                                asunto = lector.GetString(3).Trim(),
                                descripcion = lector.GetString(4).Trim(),
                                fechaCreacionExp = lector.GetDateTime(5),
                                nombreCliente = lector.GetString(6).Trim(),
                                marca = lector.GetString(7).Trim(),
                                modelo = lector.GetInt32(8),
                                placa = lector.GetString(9).Trim()
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
        #endregion consultas

        #region operaciones
        public bool RegistrarExpediente(int? pidv)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"expCrearExpediente\"(@pidv)", conexion))
                {
                    comando.Parameters.AddWithValue(":pidv", pidv);

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

        public bool ActualizarExpediente(int pid, string desc)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"expActualizarExp\"(@pid, @pdesc)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", pid);
                    comando.Parameters.AddWithValue(":pdesc", desc);

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

        public bool BorrarExpediente(int pid)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"expEliminarExp\"(@pid)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", pid);

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
