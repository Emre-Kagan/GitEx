using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OgrenciKayit.Models;
using System.IO;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OgrenciKayit.Controllers
{
    public class OgrencisController : Controller
    {

        
        private DBOgrenciEntities1 db = new DBOgrenciEntities1();

        // GET: Ogrencis
        public ActionResult Index()
        {

            Getir();

            return View(db.Ogrenci.ToList());

        }
        public ActionResult OgrenciBelgesi(int id)
        {

            Getir();
            var ogrenci = db.Ogrenci.Where(a => a.OgrenciID == id).FirstOrDefault();
            return View(ogrenci);

        }
        public ActionResult OgrenciRaporu(int id)
        {
            var rapor = new Rotativa.ActionAsPdf("OgrenciBelgesi", new { id = id });
            return rapor;
        }


        // GET: Ogrencis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public ActionResult Create()
        {
            Getir();


            return View();
        }

        // POST: Ogrencis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciID,OgrenciNo,Ad,Soyad,TcKimlikNo,DogumTarihi,Telefon,Email,Adres,Uyrugu,Fakulte,Bolum,Sinif,Mufredat,MezuniyetTarihi,BitirdigiUniversite,Yariyili,EgitimTuru,BorclulukDurumu,KulupUyeligi,KayitlanmaSekli,KayitlanmaPuani,KayitlanmaYili,KayitDonemi,LiseMezuniyeti,LiseMezuniyetiPuani,Cinsiyet,AskerlikDurumu,EngellilikDurumu,OrganizasyonID,Fotograf")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Ogrenci.Add(ogrenci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            Getir();
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciID,OgrenciNo,Ad,Soyad,TcKimlikNo,DogumTarihi,Telefon,Email,Adres,Uyrugu,Fakulte,Bolum,Sinif,Mufredat,MezuniyetTarihi,BitirdigiUniversite,Yariyili,EgitimTuru,BorclulukDurumu,KulupUyeligi,KayitlanmaSekli,KayitlanmaPuani,KayitlanmaYili,KayitDonemi,LiseMezuniyeti,LiseMezuniyetiPuani,Cinsiyet,AskerlikDurumu,EngellilikDurumu,OrganizasyonID,Fotograf")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Delete/5
        public ActionResult Delete(int? id)
        {
            //var ogrDers = db.OgrenciDers.Where(i => i.OgrenciID == id).FirstOrDefault();
            //if (ogrDers != null)
            //{
            //    return RedirectToAction("Index");
            //}
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
           
            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                Ogrenci ogrenci = db.Ogrenci.Find(id);
                db.Ogrenci.Remove(ogrenci);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                var msj=ex.Message;
                if (ex.HResult.ToString() == "-2146233087")
                    msj = "Öğrenciye ait ilişkili kayıtlar olduğu için silinemez!";
                return Json(new { result = false, error = msj });

            }





        }

        //public JsonResult DeleteJson(int id)
        //{

        //    try
        //    {
        //        Ogrenci ogrenci = db.Ogrenci.Find(id);
        //        db.Ogrenci.Remove(ogrenci);
        //        db.SaveChanges();
        //        //return RedirectToAction("Index");
        //        int basari = 1;
        //        return Json(basari, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        int hata = 0;
        //        return Json(hata, JsonRequestBehavior.AllowGet);
        //        //return RedirectToAction("Delete");

        //    }





        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Sorgula()
        {
            return View(db.Ogrenci.ToList());
        }

        public void Getir()
        {
            DBOgrenciEntities1 db = new DBOgrenciEntities1();
            var organizasyon = db.Organizasyon.ToList();
            List<Aktarma> aktarma = new List<Aktarma>();
            List<Kontrol> check = new List<Kontrol>();
            string kontrol = "0";
            string esit = "0";

            for (int i = 0; i < organizasyon.Count; i++)
            {


                for (int j = 0; j < organizasyon.Count; j++)
                {

                    if (organizasyon[i].ID == organizasyon[j].ParentID)
                    {


                        for (int k = 0; k < organizasyon.Count; k++)
                        {

                            if (organizasyon[j].ID == organizasyon[k].ParentID)
                            {


                                esit = "1";
                                Kontrol ek = new Kontrol();
                                ek.ID = organizasyon[k].ID;
                                check.Add(ek);



                                Aktarma ekle1 = new Aktarma();
                                ekle1.ID = organizasyon[k].ID;
                                ekle1.Ad = organizasyon[i].Ad + '/' + organizasyon[j].Ad + '/' + organizasyon[k].Ad;
                                aktarma.Add(ekle1);


                            }

                        }


                        foreach (var item in check)
                        {
                            if (item.ID == organizasyon[j].ID)
                            {
                                kontrol = "1";
                                break;
                            }

                        }

                        if (kontrol == "0" && esit == "0")
                        {
                            Aktarma ekle = new Aktarma();
                            ekle.ID = organizasyon[j].ID;
                            ekle.Ad = organizasyon[i].Ad + '/' + organizasyon[j].Ad;
                            aktarma.Add(ekle);

                        }
                        else
                        {

                            kontrol = "0";
                            esit = "0";
                        }

                    }

                }

            }


            ViewBag.parent = aktarma.ToList();



        }
    }
}
