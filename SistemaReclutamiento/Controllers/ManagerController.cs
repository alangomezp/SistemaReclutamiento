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

        public JsonResult GetUsuarios()
        {
            var usuarios = (from a in db.Usuarios
                            where a.Activo == true
                            select new
                            {
                                Id = a.Id,
                                Usuario = a.Usuario,
                                Correo = a.Correo,
                                Descripcion = a.Descripcion,
                                PerfilId = a.PerfilId

                            }).ToList();

            return new JsonResult { Data = usuarios, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult DesactivarUsuario (int id)
        {
            Response response = new Response();

            using (SistemaReclutamientoDBEntities dBEntities = new SistemaReclutamientoDBEntities())
            {
                var result = dBEntities.Usuarios.Find(id);
                try
                {
                    if (result != null)
                    {
                        result.Activo = false;
                        dBEntities.SaveChanges();

                    }

                    response.message = "Muy bien!";
                    response.messageInfo = "Desactivado correctamente";
                    response.messageType = "success";

                    return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                catch (Exception e)
                {
                    response.message = "Error!";
                    response.messageInfo = e.Message;
                    response.messageType = "error";

                    return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
            }
            

        }

        public JsonResult ModificarUsuario(Usuarios users)
        {
            Response response = new Response();

            using (SistemaReclutamientoDBEntities dBEntities = new SistemaReclutamientoDBEntities())
            {
                var result = dBEntities.Usuarios.Find(users.Id);
                try
                {
                    if (result != null)
                    {
                        result.Usuario = users.Usuario ?? result.Usuario;
                        result.Password = users.Password ?? result.Password;
                        result.Correo = users.Correo ?? result.Correo;
                        result.Descripcion = users.Descripcion ?? result.Descripcion;
                        result.PerfilId = users.PerfilId;
                        dBEntities.SaveChanges();

                    }

                    response.message = "Muy bien!";
                    response.messageInfo = "Modificado correctamente";
                    response.messageType = "success";

                    return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                catch (Exception e)
                {
                    response.message = "Error!";
                    response.messageInfo = e.Message;
                    response.messageType = "error";

                    return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
            }


        }


        //public JsonResult EditUsuario(Usuarios usuarios)
        //{
        //    Response response = new Response();

        //    var result = db.Usuarios.Where(a => a.Id == usuarios.Id).SingleOrDefault();

        //    string key = "sdr";
        //    var password = EncryptEngine.Encriptar(usuarios.Password, key);
        //    try
        //    {
        //        if (result != null)
        //        {
        //            result.Password = password;
        //            result.Usuario = usuarios.Usuario;
        //            result.Correo = usuarios.Correo;
        //            result.Descripcion = usuarios.Descripcion;
        //            result.PerfilId = usuarios.PerfilId;

        //            db.SaveChanges();

        //        }

        //        response.message = "Muy bien!";
        //        response.messageInfo = "Modificado correctamente";
        //        response.messageType = "success";

        //        return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    catch (Exception e)
        //    {

        //        response.message = "Error!";
        //        response.messageInfo = e.Message;
        //        response.messageType = "error";

        //        return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
           
        //}
        public JsonResult CreateUser(Usuarios usuarios)
        {
            Response response = new Response();
            string key = "sdr";
            var password = EncryptEngine.Encriptar(usuarios.Password, key);

            try
            {
                Usuarios user = new Usuarios();

                user.Usuario = usuarios.Usuario;
                user.Password = password;
                user.Correo = usuarios.Correo;
                user.Descripcion = usuarios.Descripcion;
                user.PerfilId = usuarios.PerfilId;
                user.Activo = true;

                db.Usuarios.Add(user);
                db.SaveChanges();

                response.message = "Muy bien!";
                response.messageInfo = "Usuario creado correctamente";
                response.messageType = "success";

                return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception e)
            {
                response.message = "Error!";
                response.messageInfo = e.Message;
                response.messageType = "error";

                return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
    }
}