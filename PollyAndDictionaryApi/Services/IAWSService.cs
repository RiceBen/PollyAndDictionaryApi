using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollyAndDictionaryApi.Services
{
    /// <summary>
    /// Dictionary Service Interface
    /// </summary>
    public interface IDictionaryService
    {
        /// <summary>
        /// Get Voice via 3rd Service
        /// </summary>
        /// <param name="word">word</param>
        void GetVoice(string word);

        /// <summary>
        /// Get Dictionary Consult Result
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>consult result</returns>
        List<Models.OxfordEntriesEntity> GetDictionaryConsultResult(string word);

        /// <summary>
        /// Get Voice via 3rd Service, asynchronous version.
        /// </summary>
        /// <param name="word">word</param>
        Task GetVoiceAsync(string word);

        /// <summary>
        /// Get Dictionary Consult Result, asynchronous version.
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>consult result</returns>
        Task<List<Models.OxfordEntriesEntity>> GetDictionaryConsultResultAsync(string word);
    }
}