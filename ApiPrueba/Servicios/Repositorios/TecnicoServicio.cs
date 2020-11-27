using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiPrueba.Servicios.Repositorios
{
    public class TecnicoServicio : ITecnicoServicio
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public TecnicoServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Tecnico> VerTecnicos()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Tecnico> ListaTecnicos = new List<Tecnico>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"tncVerTecnicos\"();", conexion))
                {

                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        Tecnico tnc = new Tecnico
                        {
                            IDTecnico = lector.GetInt32(0),
                            nombre = lector.GetString(1).Trim(),
                            pmrApellido = lector.GetString(2).Trim(),
                            sgndApellido = lector.GetString(3).Trim(),
                            cedula = lector.GetString(4).Trim(),
                            fechaIngreso = lector.GetDateTime(5)
                        };
                        ListaTecnicos.Add(tnc);
                    }
                    lector.Close();
                }
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return ListaTecnicos;
        }

        public Tecnico ConsultarTecnicoCedula(string pCedula)
        {
            Tecnico salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"tncVerTecnicoPorCedula\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pced", pCedula);

                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        salida = new Tecnico
                        {
                            IDTecnico = lector.GetInt32(0),
                            nombre = lector.GetString(1).Trim(),
                            pmrApellido = lector.GetString(2).Trim(),
                            sgndApellido = lector.GetString(3).Trim(),
                            cedula = lector.GetString(4).Trim(),
                            fechaIngreso = lector.GetDateTime(5)
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

        //public List<TecnicoCita> MostrarTecnicosConCita()
        //{
        //    List<TecnicoCita> tc = new List<TecnicoCita>();
        //    NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

        //    try
        //    {
        //        conexion.Open();

        //        using (var comando = new NpgsqlCommand("\"Taller\".\"tncVerTecnicosConCita\"", conexion))
        //        {
        //            comando.CommandType = CommandType.StoredProcedure;

        //            using NpgsqlDataReader lector = comando.ExecuteReader();
        //            TecnicoCita objt = null;
        //            while (lector.Read())
        //            {
        //                objt = new TecnicoCita
        //                {
        //                    nombre = lector.GetString(0),
        //                    pmrApellido = lector.GetString(1).Trim(),
        //                    sgndApellido = lector.GetString(2).Trim(),
        //                    cedula = lector.GetString(3).Trim(),
        //                    fecha = lector.GetDateTime(4),
        //                    hora = lector.GetString(5).Trim(),
        //                    asunto = lector.GetString(6).Trim()
        //                };
        //                tc.Add(objt);
        //            }
        //            lector.Close();
        //        }
        //        conexion.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return tc;
        //}

        #endregion consultas

        #region operaciones
        public bool RegistrarTecnico(string nmb, string ap1, string ap2, string ced)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"tncCrearTecnico\"(@nmb, @ap1, @ap2, @ced)", conexion))
                {
                    comando.Parameters.AddWithValue(":nmb", nmb);
                    comando.Parameters.AddWithValue(":ap1", ap1);
                    comando.Parameters.AddWithValue(":ap2", ap2);
                    comando.Parameters.AddWithValue(":ced", ced);

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

        public bool ActualizarTecnico(string nmb, string ap1, string ap2, string ced)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"tncActualizarTecnico\"(@pid, @nmb, @ap1, @ap2)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", ced);
                    comando.Parameters.AddWithValue(":nmb", nmb);
                    comando.Parameters.AddWithValue(":ap1", ap1);
                    comando.Parameters.AddWithValue(":ap2", ap2);

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

        public bool BorrarTecnico(string id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"tncEliminarTecnico\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);

                    using(var lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            resultado = lector.GetBoolean(0);
                        }
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

        #endregion operaciones

        #region areaDetallesTec
        public List<DetallesTecnico> VerDetallesTecnico(string cedula)
        {
            List<DetallesTecnico> salida = new List<DetallesTecnico>();
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"dttVerDetalleTecnico\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", cedula);
                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        DetallesTecnico dtt = new DetallesTecnico
                        {
                            IDTecnico = lector.GetInt32(0),
                            direccion = lector.GetString(1).Trim(),
                            telefono = lector.GetString(2).Trim(),
                            correo = lector.GetString(3).Trim(),
                            codDet = lector.GetInt32(4)
                        };
                        salida.Add(dtt);
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

        public DetallesTecnico VerDetalleIndividual(int id)
        {
            DetallesTecnico salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"dttVerDetallePorId\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);
                    using NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        salida = new DetallesTecnico
                        {
                            IDTecnico = lector.GetInt32(0),
                            direccion = lector.GetString(1).Trim(),
                            telefono = lector.GetString(2).Trim(),
                            correo = lector.GetString(3).Trim(),
                            codDet = lector.GetInt32(4)
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

        public bool RegistrarDetalleTecnico(int id, string pdireccion, string ptelefono, string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dttFnIngresarDetalle\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);
                    comando.Parameters.AddWithValue("pdireccion", pdireccion);
                    comando.Parameters.AddWithValue("ptelefono", ptelefono);
                    comando.Parameters.AddWithValue("pcorreo", pcorreo);

                    NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        resultado = lector.GetBoolean(0);
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

        public bool ActualizarDetalleTecnico(int idtecnico, string pdireccion, string ptelefono, string pcorreo, int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dttFnActualizarDetalles\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pidtecnico", idtecnico);
                    comando.Parameters.AddWithValue("pdireccion", pdireccion);
                    comando.Parameters.AddWithValue("ptelefono", ptelefono);
                    comando.Parameters.AddWithValue("pcorreo", pcorreo);
                    comando.Parameters.AddWithValue("pid", id);

                    NpgsqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        resultado = lector.GetBoolean(0);
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

        public bool BorrarDetalleTecnico(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"dttEliminarDetalle\"(@pid)", conexion))
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
        #endregion areaDetallesTec
    }
}
