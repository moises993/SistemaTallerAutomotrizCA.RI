using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Repositorios
{
    public class ClienteServicio : IClienteServicio
    {
        private IConfiguration _configuration;
        private readonly string connectionString;

        public ClienteServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public List<Cliente> VerClientes()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Cliente> ListaClientes = new List<Cliente>();

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"cltVerClientes\"();", conexion))
                {

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Cliente clt = new Cliente
                            {
                                IDCliente = lector.GetInt32(0),
                                nombre = lector.GetString(1).Trim(),
                                pmrApellido = lector.GetString(2).Trim(),
                                sgndApellido = lector.GetString(3).Trim(),
                                cedula = lector.GetString(4).Trim(),
                                cltFrecuente = lector.GetBoolean(5),
                                fechaIngreso = lector.GetDateTime(6)
                            };

                            ListaClientes.Add(clt);
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

            return ListaClientes;
        }

        public Cliente ConsultarClienteCedula(string pCedula)
        {
            Cliente salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"cltVerClientePorCedula\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", pCedula);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            salida = new Cliente
                            {
                                IDCliente = lector.GetInt32(0),
                                nombre = lector.GetString(1).Trim(),
                                pmrApellido = lector.GetString(2).Trim(),
                                sgndApellido = lector.GetString(3).Trim(),
                                cedula = lector.GetString(4).Trim(),
                                cltFrecuente = lector.GetBoolean(5),
                                fechaIngreso = lector.GetDateTime(6)
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

        public List<ClienteCita> MostrarClientesConCita()
        {
            List<ClienteCita> cc = new List<ClienteCita>();
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"cltVerClientesConCita\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        ClienteCita objt = null;
                        while (lector.Read())
                        {
                            objt = new ClienteCita
                            {
                                nombre = lector.GetString(0),
                                pmrApellido = lector.GetString(1),
                                sgndApellido = lector.GetString(2),
                                cedula = lector.GetString(3),
                                fecha = lector.GetDateTime(4),
                                hora = lector.GetString(5),
                                asunto = lector.GetString(6)
                            };
                            cc.Add(objt);
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

            return cc;
        }
        #endregion consultas

        //Métodos que devuelven true siempre y cuando se cumplan las validaciones para cada parámetro
        #region operaciones

        public bool CedulaExiste(string ced)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"cltValidarCedula\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", ced);

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
            catch(Exception)
            {
                throw;
            }
        }

        public bool RegistrarCliente(string nmb, string ap1, string ap2, string ced, bool frec)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using(var comando = new NpgsqlCommand("CALL \"Taller\".\"cltCrearCliente\"(@nmb, @ap1, @ap2, @ced, @frec)", conexion))
                {
                    comando.Parameters.AddWithValue(":nmb", nmb);
                    comando.Parameters.AddWithValue(":ap1", ap1);
                    comando.Parameters.AddWithValue(":ap2", ap2);
                    comando.Parameters.AddWithValue(":ced", ced);
                    comando.Parameters.AddWithValue(":frec", frec);

                    comando.ExecuteNonQuery();

                    conexion.Close();
                }
                return true;
            }
            catch(Exception)
            {
                throw;
            }

        }

        public bool ActualizarCliente(string id, string nmb, string ap1, string ap2, string ced, bool frec)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"cltEditarCliente\"(@id, @nmb, @ap1, @ap2, @ced, @frec)", conexion))
                {
                    comando.Parameters.AddWithValue(":id", id);
                    comando.Parameters.AddWithValue(":nmb", nmb);
                    comando.Parameters.AddWithValue(":ap1", ap1);
                    comando.Parameters.AddWithValue(":ap2", ap2);
                    comando.Parameters.AddWithValue(":ced", ced);
                    comando.Parameters.AddWithValue(":frec", frec);

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

        public bool BorrarCliente(string ced)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"cltEliminarCliente\"(@identificacion)", conexion))
                {
                    comando.Parameters.AddWithValue(":identificacion", ced.Trim());

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

        //Métodos para ingreso y consulta de los detalles de un cliente
        #region areaDetalles
        public DetallesCliente VerDetallesCliente(int id)
        {
            DetallesCliente salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("\"Taller\".\"dtcVerDetalleClt\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            salida = new DetallesCliente
                            {
                                IDCliente = lector.GetInt32(0),
                                direccion = lector.GetString(1),
                                telefono = lector.GetString(2),
                                correo = lector.GetString(3)
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

        public bool ActualizarDetalleCliente(int id, string pdireccion, string ptelefono, string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"dtcActualizarDetalles\"(@pid, @pdireccion, @ptelefono, @pcorreo)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", id);
                    comando.Parameters.AddWithValue(":pdireccion", pdireccion);
                    comando.Parameters.AddWithValue(":ptelefono", ptelefono);
                    comando.Parameters.AddWithValue(":pcorreo", pcorreo);

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

        public bool BorrarDetalleCliente(int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"dtcEliminarDetalle\"(@pid)", conexion))
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

        public bool RegistrarDetalleCliente(int id, string pdireccion, string ptelefono, string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("CALL \"Taller\".\"dtcIngresarDetalles\"(@pid, @pdireccion, @ptelefono, @pcorreo)", conexion))
                {
                    comando.Parameters.AddWithValue(":pid", id);
                    comando.Parameters.AddWithValue(":pdireccion", pdireccion);
                    comando.Parameters.AddWithValue(":ptelefono", ptelefono);
                    comando.Parameters.AddWithValue(":pcorreo", pcorreo);

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

        #endregion areaDetalles
    }
}
