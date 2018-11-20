using System;
using System.Web.Mvc;
using PollyAndDictionaryApi.Filters;

namespace PollyAndDictionaryApi
{
    public class FilterConfig
    {
        public static void RegisterFilter(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
        }
    }
}