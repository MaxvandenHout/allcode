using System;
using System.Dynamic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using personal.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using System.Web.WebPages;

namespace personal.Controllers
{
    public class MainPageController : Controller
    {
        private personalEntities7 db = new personalEntities7();

        // GET: MainPage
      
        public ActionResult Index()
        {
            if (
                UserSession.Current.ContainsKey("Id") &&
                !string.IsNullOrWhiteSpace(UserSession.Current["Id"].ToString())
            )
            {

                int id = Convert.ToInt32(UserSession.Current["Id"]);
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {

                }
 
                dynamic Chatmodel = new ExpandoObject();
                Chatmodel.Users = GetMatches(null);
                return View(Chatmodel);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private IQueryable<Users> GetMatches(string searchString)
        {
            int id = Convert.ToInt32(UserSession.Current["Id"]);
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {

            }
            

            IQueryable<Users> matchUsers;
            if (String.IsNullOrEmpty(searchString))
            {
                matchUsers = db.Matches
                    .Where(q => q.idUser == id)
                    .Select(m => m.Users1);
            }
            else
            {
                matchUsers = db.Matches
                    .Where(q => q.idUser == id && q.Users.FName.ToLower().Contains(searchString.ToLower()))
                    .Select(m => m.Users1);
            }
            return matchUsers;
        }

        //public ActionResult SearchMatchUser(string searchString)
        //{
        //    return PartialView(Users = GetMatches(searchString));
        //}

      
        //public ActionResult Loadmessages(int sendid)
        //{
        //    int id = Int32.Parse((string)Session["UserID"]);
            
        //    var messages = from r in db.Messages select r;
        //    messages = messages.Where(r => r.idUsersend == id || r.idUserget == id).Where(r => r.idUsersend == id2 || r.idUserget == id2);
            
        //    return View(messages);
        //    //signalr
            
        //}

     

        public ActionResult Find()
        {
            int id = Int32.Parse((string)Session["UserID"]);
            var intrestlist = from e in db.Intrests select e;
            var intrestmatch = from i in db.Intrests select i;
            intrestlist = db.Intrests.Where(e => e.IdUser == id);
            foreach (var item in intrestlist)
            {
                intrestmatch = db.Intrests.Where(s => s.intrest.Contains(item.intrest)).Where(i => i.IdUser != id);
            }
            foreach (var item in intrestmatch)
            {
                var match = new Matches
                {
                    Matchintrest = item.intrest,
                    idUser = id,
                    UseridMatch = item.IdUser,
                };
                match.Matchnumbers = match.Matchnumbers + 1;
                db.Matches.Add(match);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}