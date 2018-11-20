using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PollyAndDictionaryApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true)
            {
                return;
            }

            var exception = filterContext.Exception;
            var context = filterContext.HttpContext;

            HttpException httpException = new HttpException(null, exception);
            
            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                context.Response.StatusCode = 404;
            }
            else
            {
                context.Response.StatusCode = 500;
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}