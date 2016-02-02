using System.Web.Mvc;

namespace medis.Api.Controllers
{
    [RoutePrefix("/")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            // only used to send initial view file to Index.html
            return View();
        }
    }
}