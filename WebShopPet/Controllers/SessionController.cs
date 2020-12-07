using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace ShopPet.Controllers
{
    public class SessionController : Controller
    {
        private ShopPetDB db = new ShopPetDB();
        // GET: Session
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string EMAIL, string PASSWORD)
        {
            if (ModelState.IsValid)
            {
                var user = db.USERS.Where(u => u.EMAIL.Equals(EMAIL) && u.PASSWORD.Equals(PASSWORD)).ToList();
                if(user.Count() > 0)
                {
                    Session["ID"] = user.FirstOrDefault().ID;
                    Session["Name"] = user.FirstOrDefault().NAME;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error_login = "Đăng nhập không thành công";
                    return RedirectToAction("create");
                }
            }
            return View();
        }
    }
}