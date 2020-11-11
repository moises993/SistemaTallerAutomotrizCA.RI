﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Utilidades
{
    public class ListaFabricantes
    {
        static readonly string[] marcas =
        {
            "Abarth",
              "Alfa Romeo",
              "Aston Martin",
              "Audi",
              "Bentley",
              "BMW",
              "Bugatti",
              "Cadillac",
              "Chevrolet",
              "Chrysler",
              "Citroën",
              "Dacia",
              "Daewoo",
              "Daihatsu",
              "Dodge",
              "Donkervoort",
              "DS",
              "Ferrari",
              "Fiat",
              "Fisker",
              "Ford",
              "Honda",
              "Hummer",
              "Hyundai",
              "Infiniti",
              "Iveco",
              "Jaguar",
              "Jeep",
              "Kia",
              "KTM",
              "Lada",
              "Lamborghini",
              "Lancia",
              "Land Rover",
              "Landwind",
              "Lexus",
              "Lotus",
              "Maserati",
              "Maybach",
              "Mazda",
              "McLaren",
              "Mercedes-Benz",
              "MG",
              "Mini",
              "Mitsubishi",
              "Morgan",
              "Nissan",
              "Opel",
              "Peugeot",
              "Porsche",
              "Renault",
              "Rolls-Royce",
              "Rover",
              "Saab",
              "Seat",
              "Skoda",
              "Smart",
              "SsangYong",
              "Subaru",
              "Suzuki",
              "Tesla",
              "Toyota",
              "Volkswagen",
              "Volvo"
        };

        public static List<string> ObtenerMarcas() => marcas.ToList();
    }
}
