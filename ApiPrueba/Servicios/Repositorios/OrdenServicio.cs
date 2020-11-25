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
        private readonly IConfiguration _configuration;
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

                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        Orden clt = new Orden
                        {
                            IDOrden = lector.GetInt32(0),
                            IDVehiculo = lector.GetInt32(1),
                            cedulaCliente = lector.GetString(2).Trim(),
                            placaVehiculo = lector.GetString(3).Trim(),
                            descServicio = lector.GetString(4).Trim()
                        };

                        ListaOrdens.Add(clt);
                    }
                    lector.Close();
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

                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        salida = new Orden
                        {
                            IDOrden = lector.GetInt32(0),
                            IDVehiculo = lector.GetInt32(1),
                            cedulaCliente = lector.GetString(2).Trim(),
                            placaVehiculo = lector.GetString(3).Trim(),
                            descServicio = lector.GetString(4).Trim()
                        };
                    }

                    lector.Close();
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
        public bool? RegistrarOrden(int pidcita, int pidcliente, string pdesc)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;
            try
            {
                conexion.Open();
                using (NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"odnGenerarOrden\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pidcita", pidcita);
                    comando.Parameters.AddWithValue("pidcliente", pidcliente);
                    comando.Parameters.AddWithValue("pdesc", pdesc);
                    using(NpgsqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read()) resultado = lector.GetBoolean(0);
                    }
                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
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
        #endregion operaciones
    }
}
