using personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal.Controllers
{
    public class RegistrationController : Controller
    {
        private personalEntities7 db = new personalEntities7();
        // GET: Regristration
        public ActionResult Registration1()
        {
            return View();
        }

        public ActionResult Registration2()
        {
            return View();
        }

        public ActionResult Registration3()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FName,LName,Country,IP,Birthdate,Email,Password,Username,Phone")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.IP = Request.UserHostAddress;
             
                db.Users.Add(users);
                db.SaveChanges();
                Session["UserID"] = users.Iduser.ToString();
                return RedirectToAction("Registration2", "Registration"); ;
            }
            return RedirectToAction("Registration3", "Registration"); ;
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLanguage ([Bind(Include = "idLanguage,Language,idUser")] Languages languages)
        {
            if (ModelState.IsValid)
            {
                languages.idUser = Int32.Parse((string)Session["UserID"]);
                db.Languages.Add(languages);
                db.SaveChanges();
                return View(languages);
            }

            return View(languages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIntrest([Bind(Include = "idintrest,intrest,IdUser")] Intrests intrests)
        {
            if (ModelState.IsValid)
            {
                intrests.IdUser = Int32.Parse((string)Session["UserID"]);
                db.Intrests.Add(intrests);
                db.SaveChanges();
                return View(intrests);
            }

            return View(intrests);
        }
    }
}