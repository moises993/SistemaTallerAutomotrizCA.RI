using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ApiPrueba.Servicios.Utilidades
{
    public class Cifrado
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public static string GenerarHash(string contra, string sal)
        {
            var hash = KeyDerivation.Pbkdf2
            (
                password: contra,
                salt: Encoding.UTF8.GetBytes(sal),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 1024 / 8
            );

            return Convert.ToBase64String(hash);
        }

        public static string GenerarSal()
        {
            byte[] salt = new byte[1024 / 8];

            const int totalRolls = 100000;

            for (int x = 0; x < totalRolls; x++)
            {
                byte roll = RollDice((byte)salt.Length);
                salt[roll - 1]++;
            }

            return Convert.ToBase64String(salt);
        }

        public static byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            byte[] randomNumber = new byte[1];
            do
            {
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));

            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            int fullSetsOfValues = Byte.MaxValue / numSides;

            return roll < numSides * fullSetsOfValues;
        }
    }
}
