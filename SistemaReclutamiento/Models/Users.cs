using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models
{
    public class Users
    {
        //Modelo para manejar los usuarios
        public int id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public int perfilid { get; set; }
    }
}