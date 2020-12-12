using System.Web.Mvc;

namespace SignalRTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string name)
        {
            UserSession.Current["Name"] = name;
            return RedirectToAction("Main");
        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Main()
        {
            if (
                UserSession.Current.ContainsKey("Name") && 
                !string.IsNullOrWhiteSpace(UserSession.Current["Name"].ToString())
            )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}