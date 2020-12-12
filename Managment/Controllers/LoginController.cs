using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managment.Models;

namespace Managment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Models.Employees objUser)
        {

            if (ModelState.IsValid)
            {
                using (managmentEntities3 db = new managmentEntities3())
                {

                    var obj = db.Employees.Where(a => a.Fname.Equals(objUser.Fname) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Employeeid.ToString();
                           
                            
                        if (obj.Rights == true)
                        {
                            return RedirectToAction("Index", "Employees");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Employee");
                        }

                    }
                }
            }
            return View(objUser);
        }
    }
}