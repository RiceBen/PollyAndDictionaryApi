using System.Web.Mvc;
using PollyAndDictionaryApi.Services;

namespace PollyAndDictionaryApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}