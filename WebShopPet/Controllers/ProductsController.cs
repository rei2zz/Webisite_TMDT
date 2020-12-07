using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace WebShopPet.Controllers
{
    public class ProductsController : Controller
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Products
        public ActionResult Index()
        {
            var pRODUCTS = db.PRODUCTS.Include(p => p.BRAND).Include(p => p.CATEGORy).Include(p => p.USER);
            return View(pRODUCTS.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME");
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME");
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_ID,CATEGORY_ID,BRAND_ID,NAME,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTS.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
            return View(pRODUCT);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
            return View(pRODUCT);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_ID,CATEGORY_ID,BRAND_ID,NAME,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
            return View(pRODUCT);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            db.PRODUCTS.Remove(pRODUCT);
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
