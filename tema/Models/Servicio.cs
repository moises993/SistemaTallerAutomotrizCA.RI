﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Servicio
    {
        public int IDServicio { get; set; }
        public int IDVehiculo { get; set; }
        public string descripcion { get; set; }
    }
}
