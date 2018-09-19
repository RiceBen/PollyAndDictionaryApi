using System.Collections.Generic;
using Newtonsoft.Json;

namespace PollyAndDictionaryApi.Models
{
    public class EntryDetail
    {
        [JsonProperty]
        public string HomographNumber { get; set; }
        
        public List<Sense> Senses { get; set; }
    }
}