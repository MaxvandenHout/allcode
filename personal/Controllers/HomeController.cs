using personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace personal.Controllers
{
    public class HomeController : Controller
    {
        private personalEntities7 db = new personalEntities7();
        public ActionResult Index()
        {
            try
            {
                string ip = Request.UserHostAddress;
                Users user = db.Users.FirstOrDefault(u => u.IP == ip);
           
                if (user == null)
                {
                    return View();
                }
                else
                {
                    UserSession.Current["Id"] = user.Iduser;
                    return RedirectToAction("Index", "MainPage");
                }

            }
            catch
            {
                return View();
            }
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Users objUser)
        {

            if (ModelState.IsValid)
            {
                using (personalEntities7 db = new personalEntities7())
                {

                    var obj = db.Users.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Iduser.ToString();
                        Session["UserName"] = obj.Username.ToString();

                        return RedirectToAction("Index", "Mainpage");


                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Registration1()
        {
            return RedirectToAction("Registration1", "Registration");
        }

    }
}