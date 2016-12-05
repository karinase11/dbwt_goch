using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lecker.Models;


namespace Lecker.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Index()
        {
            Start sm = new Start();
            sm.test = "hallo";

            


            sm.testfunction();


            ViewData["test"] = sm.test;


            return View();
        }

        // GET: Start/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Start/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Start/Create
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

        // GET: Start/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Start/Edit/5
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

        // GET: Start/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Start/Delete/5
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
