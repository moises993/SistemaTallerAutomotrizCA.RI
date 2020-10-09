using ApiPrueba.Entidades;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Repositorios
{
    public class ServicioSrv : IServicioSrv
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public ServicioSrv(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }
        public bool ActualizarServicio(int ids, string desc)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"svcEliminarServicio\"(@pid, @pdesc)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", ids);
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

        public bool BorrarServicio(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"svcEliminarServicio\"(@pid)", conexion))
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

        public bool RegistrarServicio(int idv, string desc)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"svcCrearServicio\"(@pidv, @pdesc)", conexion))
                {
                    comando.Parameters.AddWithValue(":pidv", idv);
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

        public List<Servicio> VerServicio()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Servicio> ListaServicios = new List<Servicio>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"svcVerServicios\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Servicio clt = new Servicio
                            {
                                IDServicio = lector.GetInt32(0),
                                IDVehiculo = lector.GetInt32(1),
                                descripcion = lector.GetString(2)
                            };

                            ListaServicios.Add(clt);
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

            return ListaServicios;
        }
    }
}
