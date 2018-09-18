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

        [HttpGet]
        public JsonResult Query(string word)
        {
            this.Service.GetVoice(word);

            var consultResult = this.Service.GetDictionaryConsultResult(word);

            return Json(new { Data = consultResult.ToObject(typeof(Object)) }, JsonRequestBehavior.AllowGet);
        }
    }
}