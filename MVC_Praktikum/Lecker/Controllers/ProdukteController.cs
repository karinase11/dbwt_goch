using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lecker.Models;
namespace Lecker.Controllers

{
    public class ProdukteController : Controller
    {

        Produkte m; 

        // GET: Produkte
        public ActionResult Index(int? kategorie)
        {


            m = new Produkte();

            m.getProdukte(kategorie);

            ViewData["pName"] = m.productName;
            ViewData["pID"] = m.productID;
            ViewData["pKategorie"] = m.productkategorie;

            return View();
        }

        // GET: Produkte/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produkte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produkte/Create
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

        // GET: Produkte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produkte/Edit/5
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

        // GET: Produkte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produkte/Delete/5
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
