using HenrikMotors.Data.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HenrikMotors.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Admin/Home
        
        public ActionResult Index()
        {
                return View();
        }
    }
}