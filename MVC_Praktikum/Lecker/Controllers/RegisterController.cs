using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lecker.Models;

namespace Lecker.Controllers
{
    public class RegisterController : Controller
    {
        public Register m;
        public RegisterController()
        {
            m = new Register();
        }

        // GET: Register
        public ActionResult Index()
        {

            ViewData["lastuName"] = m.form_username;

            List<string> roleList = new List<string>();
            roleList.Add("Mitarbeiter");
            roleList.Add("Student");
            

            ViewData["roleList"] = new SelectList(roleList);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string uname, string passwd, string passwdRpt, string roleList,string fachbereich,string studiengang,string buero, string eMail)
        {


            List<string> rList = new List<string>();
            rList.Add("Mitarbeiter");
            rList.Add("Student");


            ViewData["roleList"] = new SelectList(rList);



            m.form_username = uname;
            m.form_password = passwd;
            m.form_passwordAgain = passwdRpt;
            m.form_role = roleList;
            m.form_fachbereich = fachbereich;
            m.form_studiengang = studiengang;
            m.form_buero = buero;
            m.form_email = eMail;



            if (!passwd.Equals(passwdRpt))
            {
                ViewData["lastuName"] = m.form_username;
                return View();
            }

           

            PasswordAdvisor pwSecurityAdvisor = new PasswordAdvisor();
            if (PasswordAdvisor.CheckStrength(passwd) < PasswordScore.Strong)
            {
                Session["PasswordFail"] = "weak";
                return View();
            }


            m.registerUser();


            return View();

            
        }



        // GET: Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Register/Edit/5
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

        // GET: Register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Register/Delete/5
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
