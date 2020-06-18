using SistemaReclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers
{
    public class ManagerController : Controller
    {
        SistemaReclutamientoDBEntities db = new SistemaReclutamientoDBEntities();
        // GET: Manager
        public ActionResult CrearUsuario()
        {
            return View();
        }

        public JsonResult GetPerfiles()
        {
            var perfiles = (from a in db.Perfiles
                            select new
                            {
                                Id = a.Id,
                                Perfil = a.Perfil

                            }).ToList();

            return new JsonResult { Data = perfiles, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult CreateUser(Usuarios usuarios)
        {

            var password = EncryptEngine.Encriptar(usuarios.Password);

            try
            {
                Usuarios user = new Usuarios();

                user.Usuario = usuarios.Usuario;
                user.Password = password;
                user.Correo = usuarios.Correo;
                user.Descripcion = usuarios.Descripcion;
                user.PerfilId = usuarios.PerfilId;

                db.Usuarios.Add(user);
                db.SaveChanges();

                return new JsonResult { Data = "Usuario creado correctamente!", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception e)
            {

                return new JsonResult { Data = "Lo sentimos ha ocurrido un error.", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }

        }
    }
}