using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using ApiPrueba.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public ClienteServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        #region consultas
        public async Task<List<Cliente>> VerClientes()
        {
            await using NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            List<Cliente> ListaClientes = new List<Cliente>();
            try
            {
                await conexion.OpenAsync();
                await using (NpgsqlCommand comando = new NpgsqlCommand("SELECT * FROM \"Taller\".\"cltVerClientes\"();", conexion)) //
                {
                    using var lector = await comando.ExecuteReaderAsync();
                    while (await lector.ReadAsync())
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
                    await lector.CloseAsync();
                }
                await conexion.CloseAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return ListaClientes;
        }

        public async Task<Cliente> ConsultarClienteCedula(string pCedula)
        {
            await using NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            Cliente salida = null;
            try
            {
                await conexion.OpenAsync();
                using (var comando = new NpgsqlCommand("\"Taller\".\"cltVerClientePorCedula\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", pCedula);
                    await comando.PrepareAsync();
                    using var lector = await comando.ExecuteReaderAsync();
                    while (await lector.ReadAsync())
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
                    await lector.CloseAsync();
                }
                await conexion.CloseAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return salida;
        }

        //public async Task<List<ClienteCita>> MostrarClientesConCita()
        //{
        //    await using NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
        //    List<ClienteCita> cc = new List<ClienteCita>();
        //    try
        //    {
        //        await conexion.OpenAsync();
        //        using (var comando = new NpgsqlCommand("\"Taller\".\"cltVerClientesConCita\"", conexion))
        //        {
        //            comando.CommandType = CommandType.StoredProcedure;
        //            using (var lector = await comando.ExecuteReaderAsync())
        //            {
        //                ClienteCita objt = null;
        //                while (await lector.ReadAsync())
        //                {
        //                    objt = new ClienteCita
        //                    {
        //                        nombre = lector.GetString(0),
        //                        pmrApellido = lector.GetString(1),
        //                        sgndApellido = lector.GetString(2),
        //                        cedula = lector.GetString(3),
        //                        fecha = lector.GetDateTime(4),
        //                        hora = lector.GetString(5),
        //                        asunto = lector.GetString(6)
        //                    };
        //                    cc.Add(objt);
        //                }
        //                await lector.CloseAsync();
        //            }
        //        }
        //        await conexion.CloseAsync();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return cc;
        //}
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
        public List<DetallesCliente> VerDetallesCliente(string cedula)
        {
            List<DetallesCliente> salida = new List<DetallesCliente>();
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dtcVerDetalleCliente\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", cedula);
                    using var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        DetallesCliente dtc = new DetallesCliente
                        {
                            IDCliente = lector.GetInt32(0),
                            direccion = lector.GetString(1).Trim(),
                            telefono = lector.GetString(2).Trim(),
                            correo = lector.GetString(3).Trim(),
                            codDet = lector.GetInt32(4)
                        };
                        salida.Add(dtc);
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

        public DetallesCliente VerDetalleIndividual(int id)
        {
            DetallesCliente dtc = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dtcVerDetallePorId\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);
                    using var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        dtc = new DetallesCliente
                        {
                            IDCliente = lector.GetInt32(0),
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
            return dtc;
        }

        public bool RegistrarDetalleCliente(int id, string pdireccion, string ptelefono, string pcorreo)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dtcFnIngresarDetalle\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pid", id);
                    comando.Parameters.AddWithValue("pdireccion", pdireccion);
                    comando.Parameters.AddWithValue("ptelefono", ptelefono);
                    comando.Parameters.AddWithValue("pcorreo", pcorreo);

                    NpgsqlDataReader lector = comando.ExecuteReader();
                    while(lector.Read())
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

        public bool ActualizarDetalleCliente(int idcliente, string pdireccion, string ptelefono, string pcorreo, int id)
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            bool resultado = false;

            try
            {
                conexion.Open();
                using (var comando = new NpgsqlCommand("\"Taller\".\"dtcFnActualizarDetalles\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pidcliente", idcliente);
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
        #endregion areaDetalles
    }
}
