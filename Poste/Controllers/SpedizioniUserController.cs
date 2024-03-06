using Poste.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poste.Controllers
{
    public class SpedizioniUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterPackageUser()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult RegisterPackageUser()
        //{

        //    return View();
        //}
    }
}