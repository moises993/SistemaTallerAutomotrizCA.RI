using Npgsql;
using System;
using System.Data;
using System.Threading;

namespace TareaProgramada
{
    class Program
    {
        private static void ConsultaEstadoClave()
        {
            const string cadena = "Server=192.168.0.130;Port=5432;Database=BD_TallerAutomotrizCA.RI;Username=postgres;Password=_1234567Aa;";
            NpgsqlConnection conexion = new NpgsqlConnection(cadena);

            try
            {
                conexion.Open();

                using (NpgsqlCommand comando = new NpgsqlCommand("\"Taller\".\"usrControlContrasena\"", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                }

                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        static void Main()
        {
            for(; ; )
            {
                ConsultaEstadoClave();
                Thread.Sleep(TimeSpan.FromHours(1));
            }
        }
    }
}
