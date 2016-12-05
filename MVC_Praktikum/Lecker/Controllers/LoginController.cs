using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lecker.Models;

namespace Lecker.Controllers
{
    public class LoginController : Controller
    {

       public Login m;

       public LoginController()
       {
           m = new Login();

       }

        // GET: Login
        public ActionResult Index()
        {
           

            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            
                m.form_username = username;
                m.form_password = password;
                //m.form_stayOnline = (Boolean)collection["stayOnline"];

                Session["LoginError"] = 0;

                    m.getUserData();


                    if (m.isPWOK())
                    {
                        if ((int)Session["authenticated"] == 0)
                        {
                            Session["authenticated"] = m.feID;
                            Session["role"] = "ADMIN";

                            Session["uName"] = m.email;

                        }
                    }

                    if (!m.isUserOK() || !m.isPWOK())
                    {
                        Session["LoginError"] = 1;
                    }

                return View();
            
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
