using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models
{
    public class Response
    {
        //Modelo de respuesta del lado del servidor para el controlador en AngularJS
        public string message { get; set; }
        public string messageInfo { get; set; }
        public string messageType { get; set; }

    }
}