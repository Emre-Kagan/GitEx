using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OgrenciKayit.Models;

namespace OgrenciKayit.Controllers
{
    public class OrganizasyonsController : Controller
    {
        private DBOgrenciEntities1 db = new DBOgrenciEntities1();

        // GET: Organizasyons
        public ActionResult Index()
        {



            return View(db.Organizasyon.ToList());


        }

        // GET: Organizasyons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizasyon organizasyon = db.Organizasyon.Find(id);
            if (organizasyon == null)
            {
                return HttpNotFound();
            }
            return View(organizasyon);
        }

        // GET: Organizasyons/Create
        public ActionResult Create()
        {

            ViewBag.parent = db.Organizasyon.ToList();


            return View();
        }

        // POST: Organizasyons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParentID,Ad,KisaAd,Aktif")] Organizasyon organizasyon)
        {

            if (ModelState.IsValid)
            {
                db.Organizasyon.Add(organizasyon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organizasyon);
        }

        // GET: Organizasyons/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizasyon organizasyon = db.Organizasyon.Find(id);

            ViewBag.parent = db.Organizasyon.ToList();

            if (organizasyon == null)
            {
                return HttpNotFound();
            }
            return View(organizasyon);
        }

        // POST: Organizasyons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentID,Ad,KisaAd,Aktif")] Organizasyon organizasyon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizasyon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizasyon);
        }

        // GET: Organizasyons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizasyon organizasyon = db.Organizasyon.Find(id);
            if (organizasyon == null)
            {
                return HttpNotFound();
            }

            foreach (var item in db.Organizasyon)
            {
                if (item.ParentID == organizasyon.ID /*|| organizasyon.ParentID == null*/)
                {
                    ViewBag.alarm = "parent";
                    break;

                }

            }

            //if (organizasyon.ParentID == null)
            //{
            //    ViewBag.alarm = "parent";
            //}

            return View(organizasyon);
        }

        // POST: Organizasyons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Organizasyon organizasyon = db.Organizasyon.Find(id);
            db.Organizasyon.Remove(organizasyon);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
