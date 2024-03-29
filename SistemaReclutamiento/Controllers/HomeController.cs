﻿using SistemaReclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session[Sesiones.Usuario] = null;
            Session[Sesiones.Perfil] = null;

            return View();
        }

        public ActionResult Error()
        {
            Session[Sesiones.Perfil] = "error";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}