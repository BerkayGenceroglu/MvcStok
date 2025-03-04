using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        public ActionResult Index(int sayfa =1)
        {
            var values = db.TBLURUN.ToList().ToPagedList(sayfa,4);
            return View(values);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            //ASP.NET MVC’de dropdown listesi oluştururken kullanılan standart bir veri türüdür. Temel amacı, <select> etiketi içinde <option> öğeleri oluşturmaktır.
            List<SelectListItem> degerler = db.TBLKATEGORI.Select(x => new SelectListItem
            {
                Text = x.KATEGORIAD,
                Value = x.KATEGORIID.ToString()
            }).ToList();
            //VieViewBag, ASP.NET MVC’de Controller(Denetleyici) ile View(Görünüm) arasında veri taşımak için kullanılan dinamik bir nesnedir.

            //ViewBag ile Controller’dan View’e veri aktarabiliriz.
            //Dinamik(dynamic) bir nesnedir, yani içine istediğimiz türde veri koyabiliriz(List, string, int, object vb.).
            //Sadece tek bir request boyunca çalışır(sayfa yenilendiğinde içindeki veri kaybolur).
            //📌 Özetle: ViewBag, Controller’da hazırladığımız veriyi, HTML sayfasında(View) kullanmamıza yardımcı olur.
            ViewBag.dgr = degerler;
            return View();
            /*
             Veritabanındaki TBLKATEGORI tablosundaki tüm kayıtları alıyor.
             Her bir kategori için yeni bir SelectListItem nesnesi oluşturuyor.
             Bu SelectListItem nesnelerine Text ve Value atanıyor.
             Tüm SelectListItem nesnelerinin olduğu bir liste oluşturuyor(degerler).
             */
        }

        [HttpPost]
        public ActionResult Ekle(TBLURUN p1)
        {
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORI = ktg;
            //Kullanıcıdan gelen p1 nesnesi içinde kategori bilgisi olabilir, ama bu bilgi sadece kategori ID'sinden ibaret. Biz, ürünün tam olarak veritabanındaki kategori nesnesiyle bağlantılı olduğundan emin olmak için bu işlemi yapıyoruz.
            //Yani:
            //Ürün, "hangi kategoriye ait?" sorusunun cevabını veriyoruz.
            //            //Genel Bakış
            //Ürün eklenirken, kullanıcının seçtiği kategori ID ile eşleşen kategori veritabanından bulunuyor.
            //Ürünün kategori bilgisi, veritabanındaki kategori nesnesi ile güncelleniyor.
            //Ürün kaydediliyor.
            //Yani ürünü doğru kategoriye bağlamış oluyoruz. Umarım şimdi daha net olmuştur! 🚀

            db.TBLURUN.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var product = db.TBLURUN.Find(id);
            db.TBLURUN.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {

            var value = db.TBLURUN.Find(id);
            //SelectListItem türü, genellikle bir dropdown(açılır liste) veya select HTML elementinde kullanılacak verileri temsil etmek için tercih edilir.Bu tür, her bir öğeyi bir değer(Value) ve görünür metin(Text) olarak saklar.
            List<SelectListItem> degerler = db.TBLKATEGORI.Select(x => new SelectListItem
            {
                Text = x.KATEGORIAD,
                Value = x.KATEGORIID.ToString()
            }).ToList();
            ViewBag.brkay = degerler;
            return View("UrunGetir", value);
        }

        public ActionResult Guncelle(TBLURUN p1)
        {
            var value = db.TBLURUN.Find(p1.URUNID);
            value.URUNAD = p1.URUNAD;
            value.MARKA = p1.MARKA;
            value.FIYAT = p1.FIYAT;
            value.STOK = p1.STOK;
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            value.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
            //İlişkili veriler veritabanından çekilmediği için TBLKATEGORI null oluyor ve ona erişmeye çalışırken hata alıyorsun.
        }
    }
}