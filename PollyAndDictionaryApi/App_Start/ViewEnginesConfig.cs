using System.Web.Mvc;

namespace PollyAndDictionaryApi
{
    /// <summary>
    /// Web View Engines Configuration
    /// </summary>
    public class ViewEnginesConfig
    {
        /// <summary>
        /// Regist view engines only needed
        /// </summary>
        /// <param name="enginCollection">ViewEnginCollection</param>
        public static void RegisterViewEngines(ViewEngineCollection enginCollection)
        {
            enginCollection.Clear();

            enginCollection.Add(new RazorViewEngine());
        }
    }
}