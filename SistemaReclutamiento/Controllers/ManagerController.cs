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

        public void CreateUser(Usuarios usuarios)
        {

        }
    }
}