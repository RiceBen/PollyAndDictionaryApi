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
            //// clear all render engine
            enginCollection.Clear();

            //// accept only Razor render engine
            enginCollection.Add(new RazorViewEngine());
        }
    }
}