using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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

            await Task.WhenAll(voiceTask, consultTask);

            return consultTask.Result;
        }

        [Route("V1/SyncQuery/{word}")]
        [HttpGet]
        public List<Models.OxfordEntriesEntity> SyncQuery(string word)
        {
            this.dictionaryService.GetVoice(word);

            var consultResult = this.dictionaryService.GetDictionaryConsultResult(word);

            this.dictionaryService.GetVoice(word);

            return consultResult;
        }
    }
}