using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lecker.Controllers
{
    public class ImpressumController : Controller
    {
        // GET: Impressum
        public ActionResult Index()
        {
            return View();
        }

        // GET: Impressum/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Impressum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Impressum/Create
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

        // GET: Impressum/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Impressum/Edit/5
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

        // GET: Impressum/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Impressum/Delete/5
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
