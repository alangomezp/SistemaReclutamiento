using Microsoft.Ajax.Utilities;
using SistemaReclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers
{
    public class LoginController : Controller
    {

        SistemaReclutamientoDBEntities db = new SistemaReclutamientoDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUsers(Users u)
        {
            string key = "sdr";
            string password = EncryptEngine.Encriptar(u.password, key);

            var datos = (from a in db.Usuarios
                         where a.Usuario == u.usuario
                         && a.Password == password
                         select new Users
                         {
                             id = a.Id,
                             usuario = a.Usuario,
                             password = a.Password,
                             perfilid = a.PerfilId
                         }).FirstOrDefault();

            if (datos == null || datos.password == null || password != datos.password)
            {
                return new JsonResult { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            Session[Sesiones.Usuario] = datos.usuario;
            Session[Sesiones.Perfil] = datos.perfilid;

            return Json(Url.Action("VistaGeneral", "Reclutamiento"));

        }


    }
}