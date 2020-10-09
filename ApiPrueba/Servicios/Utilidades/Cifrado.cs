using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Utilidades
{
    public class Cifrado
    {
        public static string Cifrar(string clave, string llave)
        {
            var bytesClave = Encoding.UTF8.GetBytes(clave);
            var bytesLlave = Encoding.UTF8.GetBytes(llave);
            bytesLlave = SHA256.Create().ComputeHash(bytesLlave);
            var bytesEncriptados = Cifrar(bytesClave, bytesLlave);
            return Convert.ToBase64String(bytesEncriptados);
        }

        public static string Desencriptar(string claveCifrada, string llave)
        {
            var bytesCifrados = Convert.FromBase64String(claveCifrada);
            var bytesLlave = Encoding.UTF8.GetBytes(llave);

            bytesLlave = SHA256.Create().ComputeHash(bytesLlave);

            var bytesDescifrados = Desencriptar(bytesCifrados, bytesLlave);

            return Encoding.UTF8.GetString(bytesDescifrados);
        }

        private static byte[] Cifrar(byte[] bytesClave, byte[] bytesLlave)
        {
            byte[] bytesCifrados = null;
            var bytesSal = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(bytesLlave, bytesSal, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesClave, 0, bytesClave.Length);
                        cs.Close();
                    }

                    bytesCifrados = ms.ToArray();
                }
            }

            return bytesCifrados;
        }

        private static byte[] Desencriptar(byte[] bytesCifrados, byte[] bytesLlave)
        {
            byte[] claveDescifrada = null;

            var bytesSal = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32  };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(bytesCifrados, bytesSal, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesCifrados, 0, bytesCifrados.Length);
                        cs.Close();
                    }
                    claveDescifrada = ms.ToArray();
                }
            }
            return claveDescifrada;
        }
    }
}
