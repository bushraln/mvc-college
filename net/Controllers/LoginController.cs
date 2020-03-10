using net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net.Controllers
{
    public class LoginController : Controller
    {
        manageEntities db = new manageEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user objchk)
        {
            if(ModelState.IsValid)
            {
                using(manageEntities db=new manageEntities())
                {
                    var obj = db.users.Where(a => a.username.Equals(objchk.username) && a.password.Equals(objchk.password)).FirstOrDefault();

                    if (obj != null)
                    {

                        Session["userID"] = obj.Id.ToString();
                        Session["userName"] = obj.username.ToString();
                        return RedirectToAction("Index", "Home");


                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password Inncrorect");

                    }
                }

            }
            return View(objchk);
            


        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}