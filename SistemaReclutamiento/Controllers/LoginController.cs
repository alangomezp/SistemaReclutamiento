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

            //string password = EncryptEngine.Encriptar(u.password);

            //var datos = (from a in db.Usuarios
            //             where a.Usuario == u.usuraio
            //             && a.Password == password
            //             select new Users
            //             {
            //                 id = a.Id,
            //                 usuraio = a.Usuario,
            //                 password = a.Password
            //             }).FirstOrDefault();

            //if (datos.password == null)
            //    return RedirectToAction("Index", "Home");
            //if (password != datos.password)
            //    return RedirectToAction("Index", "Home");

            Session[Sesiones.Usuario] = "AlanGomez"/*datos.usuraio*/;

            //var perfil = (from a in db.PerfilesUsuarios
            //              where a.UsuarioId == datos.id
            //              select a.PerfilId).DefaultIfEmpty().FirstOrDefault();

            //Session[Sesiones.Perfil] = perfil;

            return Json(Url.Action("VistaGeneral", "Reclutamiento"));

        }


    }
}