using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OgrenciKayit.Models;
using static System.Net.Mime.MediaTypeNames;
using NonFactors.Mvc.Grid;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;

namespace OgrenciKayit.Controllers
{
    public class OgrenciDersController : Controller
    {
        private DBOgrenciEntities1 db = new DBOgrenciEntities1();

        // GET: OgrenciDers
        public ActionResult Index()
        {
            var gitdeneme = 0;
            var ogrenciDers = db.OgrenciDers.Include(o => o.Dersler).Include(o => o.Ogrenci);
            //var ogrenciDers = (from od in db.OgrenciDers
            //                   join d in db.Dersler on od.DersID equals d.DersID
            //                   join o in db.Ogrenci on od.OgrenciID equals o.OgrenciID
            //                   select new
            //                   {
            //                       DersAdi = od.Dersler.Ad,
            //                       OgrenciNo = od.Ogrenci.OgrenciNo,
            //                       Ad = od.Ogrenci.Ad.Trim(),
            //                       Soyad = od.Ogrenci.Soyad.Trim(),
            //                       Yil = od.Yil,
            //                       Donem = od.Donem,
            //                       Vize = od.Vize,
            //                       MazeretVize = od.MazeretVize,
            //                       Final = od.Final,
            //                       Butunleme = od.Butunleme,
            //                       TekDers = od.TekDers,
            //                       BasariNotu = od.BasariNotu,
            //                       HarfNotu = od.HarfNotu



            //                   }).Distinct().ToList();



            return View(ogrenciDers.ToList());
        }
        public ActionResult Transkript(int id)
        {

            Getir();
            var transkriptModel = new TranskriptModel();


            var ogrenciDers = (from od in db.OgrenciDers
                               where od.OgrenciID == id
                               select new OdModel
                               {

                                   DrsAd = od.Dersler.Ad,
                                   DrsKod = od.Dersler.Kodu,
                                   DrsKredi = od.Dersler.Kredi,
                                   DrsEcts = od.Dersler.EctsKredi,
                                   HrfNotu = od.HarfNotu.Trim(),
                                   Yil = od.Yil.Trim(),
                                   Donem = od.Donem.Trim(),

                                   //Yil = (
                                   //         od.Yil == "1" ? "2022-2023" :
                                   //         od.Yil == "2" ? "2023-2024" :
                                   //         od.Yil == "3" ? "2024-2025" :
                                   //         od.Yil == "4" ? "2025-2026" :
                                   //         od.Yil == "5" ? "2026-2027" : "Unknown"
                                   //      ),
                                   //Donem = (
                                   //           od.Donem == "0" ? "Güz" :
                                   //           od.Donem == "1" ? "Bahar" :
                                   //           od.Donem == "2" ? "Yaz" : "Unknown"

                                   //        )

                               }).Distinct().ToList();


            var yilDonemList = (from od in db.OgrenciDers
                                where od.OgrenciID == id
                                select new YilDonem
                                {

                                    Yil = od.Yil.Trim(),
                                    Donem = od.Donem.Trim()



                                }).Distinct().ToList();

            var OgrTrans = (from od in db.OgrenciDers
                            join o in db.Ogrenci on od.OgrenciID equals o.OgrenciID
                            join org in db.Organizasyon on o.OrganizasyonID equals org.ID
                            where od.OgrenciID == id
                            select new OgrTrans
                            {
                                TcKimlikNo = od.Ogrenci.TcKimlikNo,
                                OgrenciNo = od.Ogrenci.OgrenciNo,
                                Ad = od.Ogrenci.Ad,
                                Soyad = od.Ogrenci.Soyad,
                                KayitlanmaYili = od.Ogrenci.KayitlanmaYili,
                                OrganizasyonID = org.ID


                            }).FirstOrDefault();

            transkriptModel.OgrenciDersList = ogrenciDers;
            transkriptModel.YilDonemList = yilDonemList;
            transkriptModel.OgrTrans = OgrTrans;



            return View(transkriptModel);

        }
        public ActionResult Trapor(int id)
        {
            var trapor = new Rotativa.ActionAsPdf("Transkript", new { id = id });
            return trapor;
        }
        //[HttpGet]
        //public PartialViewResult IndexGrid(string search)
        //{
        //    DBOgrenciEntities1 db = new DBOgrenciEntities1();

        //    var ogrenciDers = (from od in db.OgrenciDers
        //                       where od.Dersler.Ad.Contains(search) || od.Ogrenci.OgrenciNo.Contains(search)
        //                       || od.Ogrenci.Ad.Contains(search) || od.Ogrenci.Soyad.Contains(search)
        //                       select new OgrenciDersModel
        //                       {
        //                           DersAdi = od.Dersler.Ad,
        //                           OgrenciNo = od.Ogrenci.OgrenciNo,
        //                           Ad = od.Ogrenci.Ad,
        //                           Soyad = od.Ogrenci.Soyad,
        //                           Yil = od.Yil,
        //                           Donem = od.Donem,
        //                           Vize = od.Vize,
        //                           MazeretVize = od.MazeretVize,
        //                           Final = od.Final,
        //                           Butunleme = od.Butunleme,
        //                           TekDers = od.TekDers,
        //                           BasariNotu = od.BasariNotu,
        //                           HarfNotu = od.HarfNotu


        //                       }).ToList();



        //    return PartialView("_IndexGrid", ogrenciDers);
        //}



        [HttpPost]
        public JsonResult SearchAll(string yazilan)
        {
            DBOgrenciEntities1 db = new DBOgrenciEntities1();
            //var ogrenciDers = db.OgrenciDers.Where(x => x.Dersler.Ad.Contains(customerName)).Select(x => x.Ogrenci).ToList();
            //var ogrenciDers = (from od in db.OgrenciDers
            //                   where od.Dersler.Ad.Contains(customerName)
            //                   select new OgrenciDersModel
            //                   {
            //                       DersAdi = od.Dersler.Ad,
            //                       OgrenciNo = od.Ogrenci.OgrenciNo,
            //                       Ad = od.Ogrenci.Ad,
            //                       Soyad = od.Ogrenci.Soyad,
            //                       Vize = od.Vize ?? 0
            //                   }).ToList();


            //var ogrenciDers = (from o in db.Ogrenci
            //                   join od in db.OgrenciDers on o.OgrenciID equals od.OgrenciID
            //                   join d in db.Dersler on od.DersID equals d.DersID
            //                   where d.Ad.Contains(customerName)
            //                   select new
            //                   {
            //                       d.Ad,
            //                       o.OgrenciNo,
            //                       o.Ad,
            //                       o.Soyad


            //                   }).ToList();

            //return Json(ogrenciDers, JsonRequestBehavior.AllowGet);
            var ogrenciDers = (from od in db.OgrenciDers
                               where od.Dersler.Ad.Contains(yazilan) || od.Ogrenci.OgrenciNo.Contains(yazilan)
                               || od.Ogrenci.Ad.Contains(yazilan) || od.Ogrenci.Soyad.Contains(yazilan)
                               select new OgrenciDersModel
                               {
                                   DersAdi = od.Dersler.Ad,
                                   OgrenciNo = od.Ogrenci.OgrenciNo,
                                   Ad = od.Ogrenci.Ad,
                                   Soyad = od.Ogrenci.Soyad,
                                   Yil = od.Yil,
                                   Donem = od.Donem,
                                   Vize = od.Vize,
                                   MazeretVize = od.MazeretVize,
                                   Final = od.Final,
                                   Butunleme = od.Butunleme,
                                   TekDers = od.TekDers,
                                   BasariNotu = od.BasariNotu,
                                   HarfNotu = od.HarfNotu


                               }).ToList();





            return Json(ogrenciDers, JsonRequestBehavior.AllowGet);
        }

        // GET: OgrenciDers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            if (ogrenciDers == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciDers);
        }
        public void Dropdownders()
        {
            var dersview = (from d in db.Dersler
                            join od in db.OgrenciDers on d.DersID equals od.DersID
                            select new
                            {
                                d.DersID,
                                d.Ad

                            }).Distinct().ToList();

            ViewBag.Oders = new SelectList(dersview, "DersID", "Ad");
        }
        public void Yildrop()
        {
            List<Yil> yilliste = new List<Yil>()
            {

                new Yil(){ ID = 1, Yillar="2022-2023"},
                new Yil(){ ID = 2, Yillar="2023-2024"},
                new Yil(){ ID = 3, Yillar="2024-2025"},
                new Yil(){ ID=  4, Yillar="2025-2026"},
                new Yil(){ ID=  5, Yillar="2026-2027"}
            };

            ViewBag.Yilliste = new SelectList(yilliste, "ID", "Yillar");
        }

        // GET: OgrenciDers/Create
        public ActionResult Create()
        {


            //Dropdownders();
            //var ogrview = (from o in db.Ogrenci
            //               join od in db.OgrenciDers on o.OgrenciID equals od.OgrenciID
            //               select new
            //               {
            //                   Value = o.OgrenciID,
            //                   Text = o.Ad + o.Soyad


            //               }).ToList();

            //ViewBag.Ogr = new SelectList(ogrview, "Value", "Text");

            var yilview = (from od in db.OgrenciDers
                           select new
                           {
                               Value = od.Yil,
                               Text = (
                                            od.Yil == "1" ? "2022-2023" :
                                            od.Yil == "2" ? "2023-2024" :
                                            od.Yil == "3" ? "2024-2025" :
                                            od.Yil == "4" ? "2025-2026" :
                                            od.Yil == "5" ? "2026-2027" : "Unknown"
                                         )

                           }).Distinct().ToList();

            ViewBag.Yil = new SelectList(yilview, "Value", "Text");

            //Yildrop();




            //ViewBag.DersID = new SelectList(dersview, "DersID", "Ad");
            //ViewBag.OgrenciID = new SelectList(db.Ogrenci, "OgrenciID", "Ad");
            return View();
        }

        // POST: OgrenciDers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OgrenciID,DersID,Yil,Donem,Vize,MazeretVize,Final,Butunleme,TekDers,BasariNotu")] OgrenciDers ogrenciDers)
        {

            //var liste= db.OgrenciDers.Where(i => i.DersID == ogrenciDers.DersID & i.OgrenciID == ogrenciDers.OgrenciID).ToList();

            var liste = (from od in db.OgrenciDers
                         where od.DersID == ogrenciDers.DersID & od.OgrenciID == ogrenciDers.OgrenciID
                         select new
                         {
                             od.ID

                         }).ToList();

            foreach (var item in liste)
            {
                ogrenciDers.ID = item.ID;
            }

            if (ogrenciDers.Vize != null && ogrenciDers.Final == null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4);

            }
            if (ogrenciDers.Vize != null && ogrenciDers.Final != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4 + ogrenciDers.Final * 0.6);
            }

            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final == null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4);

            }
            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4 + ogrenciDers.Final * 0.6);

            }
            if (ogrenciDers.TekDers != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.TekDers);

            }
            if (ogrenciDers.Vize != null && ogrenciDers.Final != null && ogrenciDers.Butunleme != null)
            {
                if (ogrenciDers.BasariNotu < 40)
                {
                    ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4 + ogrenciDers.Butunleme * 0.6);
                }

            }
            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final != null && ogrenciDers.Butunleme != null)
            {
                if (ogrenciDers.BasariNotu < 40)
                {
                    ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4 + ogrenciDers.Butunleme * 0.6);
                }

            }

            if (ogrenciDers.BasariNotu > 0 && ogrenciDers.BasariNotu <= 40)
                ogrenciDers.HarfNotu = "FF";
            if (ogrenciDers.BasariNotu >= 40 && ogrenciDers.BasariNotu < 59)
                ogrenciDers.HarfNotu = "DC";
            if (ogrenciDers.BasariNotu > 60 && ogrenciDers.BasariNotu < 69)
                ogrenciDers.HarfNotu = "CC";
            if (ogrenciDers.BasariNotu > 70 && ogrenciDers.BasariNotu < 79)
                ogrenciDers.HarfNotu = "BB";
            if (ogrenciDers.BasariNotu > 80 && ogrenciDers.BasariNotu < 89)
                ogrenciDers.HarfNotu = "BA";
            if (ogrenciDers.BasariNotu > 90 && ogrenciDers.BasariNotu <= 100)
                ogrenciDers.HarfNotu = "AA";



            if (ModelState.IsValid)
            {

                db.Entry(ogrenciDers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }




            //ViewBag.DersID = new SelectList(db.Dersler, "DersID", "Ad", ogrenciDers.DersID);
            //ViewBag.OgrenciID = new SelectList(db.Ogrenci, "OgrenciID", "OgrenciNo", ogrenciDers.OgrenciID);
            return View(ogrenciDers);
        }

        public JsonResult OgrenciListesi(int d, string y, string dnm)
        {


            var ogrenciler = (from od in db.OgrenciDers
                              where od.DersID == d & od.Yil == y & od.Donem == dnm
                              select new
                              {

                                  Value = od.OgrenciID,
                                  Text = od.Ogrenci.Ad + od.Ogrenci.Soyad


                              }).ToList();


            return Json(ogrenciler, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult DonemKontrol(string yil)
        {

            var yildonem = (from od in db.OgrenciDers
                            where od.Yil == yil
                            select new
                            {
                                Value = od.Donem,
                                Text = (
                                            od.Donem == "0" ? "Güz" :
                                            od.Donem == "1" ? "Bahar" :
                                            od.Donem == "2" ? "Yaz" : "Unknown"
                                         )



                            }).Distinct().ToList();


            //List<Donem> dnmliste = new List<Donem>();

            //foreach (var yd in yildonem)
            //{
            //    if (yd.Donem == "0")
            //    {
            //        dnmliste = new List<Donem>()
            //                  {

            //                         new Donem(){ ID = 0, Dnm="Güz"}


            //                  };

            //    }



            //}



            return Json(yildonem, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetOgrenciDers(string yil, string donem)
        {
            var derslr = (from od in db.OgrenciDers
                          where od.Yil == yil & od.Donem == donem
                          select new
                          {
                              Value = od.DersID,
                              Text = od.Dersler.Ad

                          }).Distinct().ToList();


            return Json(derslr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OgrenciNotlar(int ogrID, int dersID, string yil, string donem)
        {
            var ogrders = (from od in db.OgrenciDers
                           where od.DersID == dersID & od.OgrenciID == ogrID & od.Yil == yil & od.Donem == donem
                           select new
                           {

                               od.Vize,
                               od.MazeretVize,
                               od.Final,
                               od.Butunleme,
                               od.TekDers,
                               od.BasariNotu,
                               od.HarfNotu
                           }).FirstOrDefault();


            return Json(ogrders, JsonRequestBehavior.AllowGet);
        }
        // GET: OgrenciDers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            if (ogrenciDers == null)
            {
                return HttpNotFound();
            }
            var dersview = (from d in db.Dersler
                            join od in db.OgrenciDers on d.DersID equals od.DersID
                            where od.ID == id
                            select new
                            {
                                d.DersID,
                                d.Ad

                            }).Distinct().ToList();

            ViewBag.Oders = new SelectList(dersview, "DersID", "Ad");

            var ogrview = (from o in db.Ogrenci
                           join od in db.OgrenciDers on o.OgrenciID equals od.OgrenciID
                           where od.ID == id
                           select new
                           {
                               Value = o.OgrenciID,
                               Text = o.Ad + o.Soyad,

                           }).Distinct().ToList();

            ViewBag.Ogr = new SelectList(ogrview, "Value", "Text");


            Yildrop();



            //ViewBag.DersID = new SelectList(db.Dersler, "DersID", "Ad", ogrenciDers.DersID);
            //ViewBag.OgrenciID = new SelectList(db.Ogrenci, "OgrenciID", "OgrenciNo", ogrenciDers.OgrenciID);
            return View(ogrenciDers);
        }

        // POST: OgrenciDers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OgrenciID,DersID,Yil,Donem,Vize,MazeretVize,Final,Butunleme,TekDers,BasariNotu")] OgrenciDers ogrenciDers)
        {

            if (ogrenciDers.Vize != null && ogrenciDers.Final == null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4);

            }
            if (ogrenciDers.Vize != null && ogrenciDers.Final != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4 + ogrenciDers.Final * 0.6);
            }

            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final == null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4);

            }
            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4 + ogrenciDers.Final * 0.6);

            }
            if (ogrenciDers.TekDers != null)
            {
                ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.TekDers);

            }
            if (ogrenciDers.Vize != null && ogrenciDers.Final != null && ogrenciDers.Butunleme != null)
            {
                if (ogrenciDers.BasariNotu < 40)
                {
                    ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.Vize * 0.4 + ogrenciDers.Butunleme * 0.6);
                }

            }
            if (ogrenciDers.MazeretVize != null && ogrenciDers.Final != null && ogrenciDers.Butunleme != null)
            {
                if (ogrenciDers.BasariNotu < 40)
                {
                    ogrenciDers.BasariNotu = Convert.ToInt32(ogrenciDers.MazeretVize * 0.4 + ogrenciDers.Butunleme * 0.6);
                }

            }

            if (ogrenciDers.BasariNotu > 0 && ogrenciDers.BasariNotu <= 40)
                ogrenciDers.HarfNotu = "FF";
            if (ogrenciDers.BasariNotu >= 40 && ogrenciDers.BasariNotu < 59)
                ogrenciDers.HarfNotu = "DC";
            if (ogrenciDers.BasariNotu >= 59 && ogrenciDers.BasariNotu < 69)
                ogrenciDers.HarfNotu = "CC";
            if (ogrenciDers.BasariNotu >= 70 && ogrenciDers.BasariNotu < 79)
                ogrenciDers.HarfNotu = "BB";
            if (ogrenciDers.BasariNotu >= 80 && ogrenciDers.BasariNotu < 89)
                ogrenciDers.HarfNotu = "BA";
            if (ogrenciDers.BasariNotu >= 90 && ogrenciDers.BasariNotu <= 100)
                ogrenciDers.HarfNotu = "AA";

            if (ModelState.IsValid)
            {
                db.Entry(ogrenciDers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DersID = new SelectList(db.Dersler, "DersID", "Ad", ogrenciDers.DersID);
            ViewBag.OgrenciID = new SelectList(db.Ogrenci, "OgrenciID", "OgrenciNo", ogrenciDers.OgrenciID);
            return View(ogrenciDers);
        }

        // GET: OgrenciDers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            if (ogrenciDers == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciDers);
        }

        // POST: OgrenciDers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            db.OgrenciDers.Remove(ogrenciDers);
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
        [HttpGet]
        public ActionResult OgrKayit()
        {


            Dropdownders();
            Yildrop();
            var ogrview = (from o in db.Ogrenci
                           select new
                           {
                               Value = o.OgrenciID,
                               Text = o.Ad + o.Soyad


                           }).Distinct().ToList();

            ViewBag.Ogr = new SelectList(ogrview, "Value", "Text");

            var fders = (from o in db.Dersler
                         select new
                         {
                             Value = o.DersID,
                             Text = o.Ad


                         }).Distinct().ToList();

            ViewBag.Fders = new SelectList(fders, "Value", "Text");


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgrKayit([Bind(Include = "ID,OgrenciID,DersID,Yil,Donem")] OgrenciDers ogrenciDers)
        {
            if (ModelState.IsValid)
            {

                db.OgrenciDers.Add(ogrenciDers);
                db.SaveChanges();
                return RedirectToAction("Index");

            }



            return View(ogrenciDers);
        }
        public void Getir()
        {
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
