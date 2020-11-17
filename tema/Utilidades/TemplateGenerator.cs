using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema.Models;

namespace tema.Utilidades
{
    public class TemplateGenerator
    {
        //public static string ObtenerHTML(IEnumerable<Expediente> exp)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    Expediente expmod = exp.First();

        //    sb.Append(@"
        //    <style>
        //        * {
        //            font-family: Arial;
        //        }
        //    </style>
        //    <link href='~/lib/bootstrap/dist/css/bootstrap.css' rel='stylesheet'' />
        //    < img src ='~/Imagenes/WhatsApp_Image_2020-11-12_at_22.55.32_2_30.jpeg' />
        //    <br />
        //    <br />
        //    <p >
        //        Taller Automotriz CA.RI<br />
        //        León Cortés, Zona de los Santos, San José<br />
        //        Teléfono: 8888-8888<br />
        //        Fecha: @DateTime.Now.Date.ToString('dd/MM/yyyy')<br />
        //    </p>
        //    <h1 align ='center'> Expediente </ h1 >

        //    <div>
        //        <h4> Datos del vehículo</h4>
        //        <hr />");

        //    if (expmod != null)
        //    {
        //        sb.AppendFormat(@"
        //             < dl class='row'>
        //                <dt class='col-sm-5'>
        //                    Código del expediente
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {0}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Nombre del técnico responsable
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {2}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Asunto de la primera cita
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {3}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Descripción
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {4}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Fecha de creación del documento
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {5}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Nombre del cliente
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {6}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Marca del vehículo
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {7}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Modelo
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {8}
        //                </dd>
        //                <dt class='col-sm-5'>
        //                    Placa
        //                </dt>
        //                <dd class='col-sm-7'>
        //                    {9}
        //                </dd>
        //            </dl>
        //        </div>
        //        ", expmod.IDExpediente, 
        //        expmod.nombreTecnico, 
        //        expmod.asunto, 
        //        expmod.descripcion, 
        //        expmod.fechaCreacionExp.Date.ToString("dd/MM/yyyy"),
        //        expmod.nombreCliente,
        //        expmod.marca,
        //        expmod.modelo,
        //        expmod.placa);
        //    }
        //    return sb.ToString();
        //}
    }
}
