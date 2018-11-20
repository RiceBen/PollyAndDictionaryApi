using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PollyAndDictionaryApi.Services;

namespace PollyAndDictionaryApi.Controllers
{
    [RoutePrefix("api/Dictionary")]
    public class DictionaryController : ApiController
    {
        private IDictionaryService dictionaryService;

        /// <summary>
        /// Constractor of Dictionary Controller inheritance <see cref="ApiController"/>.
        /// </summary>
        /// <param name="awsService">IDictionaryService</param>
        public DictionaryController(IDictionaryService awsService)
        {
            this.dictionaryService = awsService;
        }

        [Route("V1/Query/{word}")]
        [HttpGet]
        public async Task<List<Models.OxfordEntriesEntity>> Query(string word)
        {
            var voiceTask = this.dictionaryService.GetVoiceAsync(word);

            var consultTask = this.dictionaryService.GetDictionaryConsultResultAsync(word);

            var consultResult = await consultTask;

            return consultResult;
        }
    }
}