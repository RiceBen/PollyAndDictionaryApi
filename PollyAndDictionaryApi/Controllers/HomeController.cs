using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PollyAndDictionaryApi.Services;

namespace PollyAndDictionaryApi.Controllers
{
    public class HomeController : Controller
    {
        private AWSService _awsService;

        public HomeController()
        {
            this._awsService = new AWSService();
        }

        public AWSService Service { get { return _awsService; } }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Query(string word)
        {
            this.Service.GetVoice("My Polly Hello World");

            return Json(new { Data = "My Polly " }, JsonRequestBehavior.AllowGet);
        }
    }
}