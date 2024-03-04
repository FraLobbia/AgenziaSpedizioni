using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Controllers
{
    public class SpedizioneController : Controller
    {
        // GET: Spedizione
        public ActionResult Index()
        {
            return View();
        }

        // GET: Spedizione/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Spedizione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spedizione/Create
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

        // GET: Spedizione/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Spedizione/Edit/5
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

        // GET: Spedizione/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Spedizione/Delete/5
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
