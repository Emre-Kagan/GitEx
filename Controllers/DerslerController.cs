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
    public class DerslerController : Controller
    {
        
   
        private DBOgrenciEntities1 db = new DBOgrenciEntities1();

        // GET: Dersler
        public ActionResult Index()
        {
            return View(db.Dersler.ToList());
        }

        // GET: Dersler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // GET: Dersler/Create

        public ActionResult Create()
        {


            return View();
        }

        // POST: Dersler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DersID,Ad,Kodu,Aktif,Uygulama,Teorik,Laboratuvar,Kredi,EctsKredi,ZorunluSecmeli,YıllıkDonemlik,Donemi")] Dersler dersler)
        {
            
            if (ModelState.IsValid)
            {
                
                    
                    db.Dersler.Add(dersler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            
        
            return View(dersler);
        }

        // GET: Dersler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: Dersler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DersID,Ad,Kodu,Aktif,Uygulama,Teorik,Laboratuvar,Kredi,EctsKredi,ZorunluSecmeli,YıllıkDonemlik")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dersler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dersler);
        }

        // GET: Dersler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: Dersler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dersler dersler = db.Dersler.Find(id);
            db.Dersler.Remove(dersler);
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
