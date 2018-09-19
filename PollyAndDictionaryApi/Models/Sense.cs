using System.Collections.Generic;
using Newtonsoft.Json;

namespace PollyAndDictionaryApi.Models
{
    public class Sense
    {
        /// <summary>
        /// Word's definition
        /// </summary>
        public List<string> Definitions { get; set; }

        /// <summary>
        /// Word domain
        /// </summary>
        public List<string> Domains { get; set;}

        /// <summary>
        /// Sentences
        /// </summary>
        public List<Example> Examples { get; set; }
    }

    public class Example
    {
        public string Text { get; set; }
    }
}