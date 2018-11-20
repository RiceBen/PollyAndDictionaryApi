using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollyAndDictionaryApi.Models
{
    public class ResponseModel
    {
        public bool Success { get; set;}

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}