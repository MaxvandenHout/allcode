using AnonNerd.Models;
using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnonNerd.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            using (var db = new anonnerdEntities())
            {

                //ViewBag.CurrentSort = sortOrder;


                //if (searchString != null)
                //{
                //    page = 1;
                //}
                //else
                //{
                //    searchString = currentFilter;
                //}

                //ViewBag.CurrentFilter = searchString;

                //var posts = from s in db.post

                //                  select s;
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    posts = posts.Where(s => s.LName.Contains(searchString)
                //    || s.Tags.Contains(searchString)
                //    || s.Title.Contains(searchString));
                //}



                //int pageSize = 3;
                //int pageNumber = (page ?? 1);
                //this.HttpContext.Response.AddHeader("refresh", "60; url=" + Url.Action("Index"));
                return View();
             
            }
        }

        
    }
}