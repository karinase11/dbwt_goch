using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lecker.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
    Session["authenticated"] = 0;
    Session["uName"] = "";
    Session["test"] = "TestOK";
    Session["role"] = "";

    Session["LoginError"] = 0;


    return RedirectToAction("Index", "Start");


            //return View();
        }

        
    }
}
