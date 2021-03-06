﻿using ApiPrueba.Entidades;
using ApiPrueba.Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Servicios.Interfaces
{
    public interface ITecnicoServicio
    {
        List<Tecnico> VerTecnicos();
        Tecnico ConsultarTecnicoCedula(string ced);
        //List<TecnicoCita> MostrarTecnicosConCita();
        bool ValidarCedula(string cedula);
        bool RegistrarTecnico(string nmb, string ap1, string ap2, string ced);
        bool ActualizarTecnico(string nmb, string ap1, string ap2, string ced);
        bool BorrarTecnico(string id);
        List<DetallesTecnico> VerDetallesTecnico(string cedula);
        DetallesTecnico VerDetalleIndividual(int id);
        bool RegistrarDetalleTecnico(int id, string pdireccion, string ptelefono, string pcorreo);
        bool ActualizarDetalleTecnico(int idtecnico, string pdireccion, string ptelefono, string pcorreo, int id);
        bool BorrarDetalleTecnico(int id);
    }
}
