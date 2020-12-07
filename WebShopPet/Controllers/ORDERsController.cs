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
    public class ORDERsController : Controller
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: ORDERs
        // GET: Orders
        public ActionResult Index()
        {
            var order = Session["Order"];
            var list = new List<ORDER_DETAILS>();
            if (order != null)
            {
                list = (List<ORDER_DETAILS>)order;
            }
            return View(list);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // GET: Orders/Create
        public ActionResult Create(int ProductID, int Quantity)
        {
            var order = Session["Order"];
            PRODUCT product = (PRODUCT)db.PRODUCTS.Single(x => x.ID == ProductID);
            if (order != null)
            {
                var list = (List<ORDER_DETAILS>)order;
                if (list.Exists(x => x.PRODUCT.ID == ProductID))
                {
                    foreach (var item in list)
                    {
                        if (item.PRODUCT == product)
                        {
                            item.QUANTITY = item.QUANTITY + Quantity;
                        }
                    }
                }
                else
                {
                    var item = new ORDER_DETAILS();
                    item.PRODUCT = product;
                    item.PRODUCT = product;
                    item.QUANTITY = Quantity;
                    list.Add(item);
                }

            }
            else
            {
                var item = new ORDER_DETAILS();
                item.PRODUCT = product;
                item.QUANTITY = Quantity;
                var list = new List<ORDER_DETAILS>();
                list.Add(item);
                Session["Order"] = list;
            }
            return RedirectToAction("Index");
        }

        // POST: ORDERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_ID,TOTAL_AMOUNT,STATUS,ADDRESS,PHONE,CREATED")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.ORDERS.Add(oRDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // GET: ORDERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // POST: ORDERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_ID,TOTAL_AMOUNT,STATUS,ADDRESS,PHONE,CREATED")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // GET: ORDERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // POST: ORDERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER oRDER = db.ORDERS.Find(id);
            db.ORDERS.Remove(oRDER);
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
