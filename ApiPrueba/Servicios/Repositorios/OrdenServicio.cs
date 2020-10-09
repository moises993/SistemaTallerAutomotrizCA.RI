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
    public class OrdenServicio : IOrdenServicio
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public OrdenServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Orden> VerOrdenes()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Orden> ListaOrdens = new List<Orden>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"odnVerOrdenes\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Orden clt = new Orden
                            {
                                IDOrden = lector.GetInt32(0),
                                IDVehiculo = lector.GetInt32(1),
                                cedulaCliente = lector.GetString(2),
                                placaVehiculo = lector.GetString(3),
                                descServicio = lector.GetString(4)
                            };

                            ListaOrdens.Add(clt);
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

            return ListaOrdens;
        }

        public Orden ConsultarOrdenPorCedula(string ced)
        {
            Orden salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"odnVerOrdenPorCedula\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", ced);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            salida = new Orden
                            {
                                IDOrden = lector.GetInt32(0),
                                IDVehiculo = lector.GetInt32(1),
                                cedulaCliente = lector.GetString(2),
                                placaVehiculo = lector.GetString(3),
                                descServicio = lector.GetString(4)
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

        public bool RegistrarOrden(int idv, string pced, string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"odnCrearOrden\"(@pidv, @pcedula, @pplaca)", conexion))
                {
                    comando.Parameters.AddWithValue(":pidv", idv);
                    comando.Parameters.AddWithValue(":pcedula", pced);
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

        public bool ActualizarOrden(int id, string pced, string placa)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"odnActualizarOrden\"(@pido, @pced, @pplaca)", conexion))
                {
                    comando.Parameters.AddWithValue(":pidv", id);
                    comando.Parameters.AddWithValue(":pced", pced);
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

        public bool BorrarOrden(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"odnEliminarOrden\"(@pid)", conexion))
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
    }
}
