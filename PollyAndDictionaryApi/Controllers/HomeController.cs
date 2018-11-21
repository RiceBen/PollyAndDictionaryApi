using System.Web.Mvc;
using PollyAndDictionaryApi.Services;

namespace PollyAndDictionaryApi.Controllers
{
    public class HomeController : Controller
    {
        private AWSService awsService;

        public HomeController()
        {
            this.awsService = new AWSService();
        }

        public AWSService Service { get { return awsService; } }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}