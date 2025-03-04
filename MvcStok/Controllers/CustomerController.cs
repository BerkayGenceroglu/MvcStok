using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index(string p)
        {
            var values = from x in db.TBLMUSTERI select x;
            if (!string.IsNullOrEmpty(p)) // Eğer p değeri null veya boş değilse
            {
                values = values.Where(x => x.MUSTERIAD.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(TBLMUSTERI p1)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.TBLMUSTERI.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERI.Find(id);
            db.TBLMUSTERI.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerGetir(int id)
        {
            var customer = db.TBLMUSTERI.Find(id);
            return View("CustomerGetir", customer);
        }
        public ActionResult Guncelle(TBLMUSTERI p1)
        {
            var customer = db.TBLMUSTERI.Find(p1.MUSTERIID);
            customer.MUSTERIAD = p1.MUSTERIAD;
            customer.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}