using Antlr.Runtime.Misc;
using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace MvcStok.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        // ASP.NET MVC'de bir controller çağrıldığında varsayılan olarak Index() metodu çalışır.
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORI.ToList();
            return View(degerler);
            //Eğer return View(); kullanıyorsan, ASP.NET MVC otomatik olarak action metodu ile aynı isme sahip bir view dosyasını arar.
        }

 
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        //Amaç: Kullanıcı kategori ekleme sayfasına gitmek istediğinde çalışır.
        //Kullanıcının karşısına kategori ekleme formu gelir.
        [HttpPost]
        public ActionResult Ekle(TBLKATEGORI p1)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.TBLKATEGORI.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Amaç: Kullanıcı formu doldurup gönderdiğinde, gelen verileri alıp veritabanına kaydeder.
        //Eğer ekleme işi yaparsan post metodu kullanılır.eğer sayfayı gösterme işi yaparsan get metodu kullanılır.

        public ActionResult SIL(int id)
        {
            // Burada id parametresi, URL’deki sayıyı alıyor.
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GUNCELLE(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            return View("GUNCELLE", kategori);
        }

        public ActionResult Guncellee(TBLKATEGORI p1)
        {
            var kategori = db.TBLKATEGORI.Find(p1.KATEGORIID);
            kategori.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
