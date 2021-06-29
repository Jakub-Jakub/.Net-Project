using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class DiscloneController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}