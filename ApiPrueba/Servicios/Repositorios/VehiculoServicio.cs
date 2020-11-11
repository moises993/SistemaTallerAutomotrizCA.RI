using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Repositorios
{
    public class VehiculoServicio : IVehiculoServicio
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public VehiculoServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Vehiculo> VerVehiculos()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Vehiculo> ListaVehiculos = new List<Vehiculo>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"vhcVerVehiculos\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Vehiculo clt = new Vehiculo
                            {
                                IDVehiculo = lector.GetInt32(0),
                                IDCliente = lector.GetInt32(1),
                                marca = lector.GetString(2).Trim(),
                                modelo = lector.GetInt32(3),
                                placa = lector.GetString(4).Trim()
                            };

                            ListaVehiculos.Add(clt);
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

            return ListaVehiculos;
        }

        public Vehiculo ConsultarVehiculoPorPlaca(string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            Vehiculo v = null;
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"vhcVerVehiculoPorPlaca\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pplaca", placa);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            v = new Vehiculo
                            {
                                IDVehiculo = lector.GetInt32(0),
                                IDCliente = lector.GetInt32(1),
                                marca = lector.GetString(2).Trim(),
                                modelo = lector.GetInt32(3),
                                placa = lector.GetString(4).Trim()
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

            return v;
        }
        #endregion consultas

        #region operaciones

        public bool PlacaExiste(string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;
            try
            {
                conexion.Open();
                NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"vhlValidarPlaca\"", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("pplaca", placa);
                NpgsqlDataReader lector = comando.ExecuteReader();
                while (lector.Read()) resultado = lector.GetBoolean(0);
                lector.Close();
                conexion.Close();
                return resultado;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool RegistrarVehiculo(string pcedclt, string marca, int modelo, string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"vhcIngresarVehiculo\"(@pcedclt, @pmarca, @pmodelo, @placa)", conexion))
                {
                    comando.Parameters.AddWithValue(":pcedclt", pcedclt);
                    comando.Parameters.AddWithValue(":pmarca", marca);
                    comando.Parameters.AddWithValue(":pmodelo", modelo);
                    comando.Parameters.AddWithValue(":placa", placa);

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

        public bool ActualizarVehiculo(int id, string marca, int modelo, string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"vhcActualizarVehiculo\"(@pid, @pmarca, @pmodelo, @pplaca)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", id);
                    comando.Parameters.AddWithValue(":pmarca", marca);
                    comando.Parameters.AddWithValue(":pmodelo", modelo);
                    comando.Parameters.AddWithValue(":pplaca", placa);

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

        public bool BorrarVehiculo(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"vhcEliminarVehiculo\"(@pid)", conexion))
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
