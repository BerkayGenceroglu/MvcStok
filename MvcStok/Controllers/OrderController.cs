using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class OrderController : Controller
    {
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrder(TBLSATIS p1)
        {
            db.TBLSATIS.Add(p1);
            db.SaveChanges();
            return View("Index");
        }
    }
}