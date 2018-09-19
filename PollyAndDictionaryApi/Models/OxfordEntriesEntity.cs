using System.Collections.Generic;
using Newtonsoft.Json;

namespace PollyAndDictionaryApi.Models
{
    public class OxfordEntriesEntity
    {
        /// <summary>
        /// Query Id
        /// </summary>
        [JsonProperty]
        public string Id { get; set; }

        /// <summary>
        /// Word you query
        /// </summary>
        [JsonProperty]
        public string Word { get; set; }

        /// <summary>
        /// Language you choose
        /// </summary>
        [JsonProperty]
        public string Language { get; set; }

        public List<LexicalEntry> LexicalEntries { get; set; }
    }
}