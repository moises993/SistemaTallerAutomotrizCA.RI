using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Utilidades
{
    public class GeneradorClaves
    {
        public string GenerarClave(int tamañoDeClave)
        {
            string clave = string.Empty;
            string[] combinaciones = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o",
                                 "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O",
                                 "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z","#","&",
                                 "/","$","!","?","¿","¡",".",",",";","-","*","+"};
            Random eleccionAleatoria = new Random();

            for (int i = 0; i < tamañoDeClave; i++)
            {
                int letraOSimboloAlAzar = eleccionAleatoria.Next(0, 100);
                int numerosAlAzar = eleccionAleatoria.Next(0, 9);

                if (letraOSimboloAlAzar < combinaciones.Length)
                {
                    clave += combinaciones[letraOSimboloAlAzar];
                }
                else
                {
                    clave += numerosAlAzar.ToString();
                }
            }
            return clave;
        }
    }
}
