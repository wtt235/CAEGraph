using System.Web.Mvc;

namespace CAEGraph.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "My Chart";
            return View();
        }
    }
}
