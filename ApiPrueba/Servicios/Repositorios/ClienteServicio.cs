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
        string connectionString;

        public ClienteServicio(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("pginstConexion");
        }

        public List<Cliente> VerClientes()
        {
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);
            string funcion = "SELECT * FROM administracion.cltverclientes();";
            List<Cliente> ListaClientes = new List<Cliente>(); 

            try
            {
                conexion.Open();
                NpgsqlCommand comando = new NpgsqlCommand(funcion, conexion);
                NpgsqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Cliente clt = new Cliente
                    {
                        IdCliente = lector.GetInt32(0),
                        Nombre = lector.GetString(1),
                        Apellido1 = lector.GetString(2),
                        Apellido2 = lector.GetString(3),
                        FechaIngreso = lector.GetDateTime(4),
                        CltFrecuente = lector.GetBoolean(5),
                        FactPendiente = lector.GetBoolean(6),
                        Cedula = lector.GetString(7)
                    };

                    ListaClientes.Add(clt);
                }

                lector.Close();
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return ListaClientes;
        }

        public Cliente VerClientePorCedula(string pCedula)
        {
            Cliente salida = null;
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using (var comando = new NpgsqlCommand("administracion.cltverclienteporcedula", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("pcedula", pCedula);

                    using (var lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            salida = new Cliente
                            {
                                IdCliente = lector.GetInt32(0),
                                Nombre = lector.GetString(1),
                                Apellido1 = lector.GetString(2),
                                Apellido2 = lector.GetString(3),
                                FechaIngreso = lector.GetDateTime(4),
                                CltFrecuente = lector.GetBoolean(5),
                                FactPendiente = lector.GetBoolean(6),
                                Cedula = lector.GetString(7)
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

        public List<ClienteCita> VerClientesConCita()
        {
            List<ClienteCita> cc = new List<ClienteCita>();
            NpgsqlConnection conexion = new NpgsqlConnection(connectionString);

            try
            {
                conexion.Open();

                using(var comando = new NpgsqlCommand("administracion.cltverclientesconcita", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using(var lector = comando.ExecuteReader())
                    {
                        ClienteCita objt = null;
                        while(lector.Read())
                        {
                            objt = new ClienteCita
                            {
                                Nombre = lector.GetString(0),
                                Apellido1 = lector.GetString(1),
                                Apellido2 = lector.GetString(2),
                                Cedula = lector.GetString(3),
                                Fecha = lector.GetDateTime(4),
                                Hora = lector.GetString(5),
                                Asunto = lector.GetString(6)
                            };
                            cc.Add(objt);
                        }
                        lector.Close();
                    }
                }
                conexion.Close();
            }
            catch(Exception)
            {
                throw;
            }

            return cc;
        }
    }
}
