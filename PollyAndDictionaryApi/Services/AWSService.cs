using System;
using System.Configuration;
using System.IO;
using System.Net;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Newtonsoft.Json.Linq;

namespace PollyAndDictionaryApi.Services
{
    public class AWSService
    {
        private string access_key_id;
        private string secret_access_key;
        private string app_id;
        private string app_key;
        private string oxford_base_api;

        public AWSService()
        {
            this.access_key_id = ConfigurationManager.AppSettings["AWS_access_key_id"];
            this.secret_access_key = ConfigurationManager.AppSettings["AWS_secret_access_key"];
            this.app_id = ConfigurationManager.AppSettings["Oxford_app_id"];
            this.app_key = ConfigurationManager.AppSettings["Oxford_app_key"];
            this.oxford_base_api = ConfigurationManager.AppSettings["Oxford_base_api"];
        }

        public void GetVoice(string word)
        {
            var client = new AmazonPollyClient(
                this.access_key_id,
                this.secret_access_key,
                RegionEndpoint.USEast2);

            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest
            {
                LanguageCode = LanguageCode.CmnCN,
                VoiceId = VoiceId.Zhiyu,
                Text = word,
                TextType = TextType.Text,
                OutputFormat = OutputFormat.Mp3
            };

            string outputFileName = "D:\\speech.mp3";

            try
            {
                SynthesizeSpeechResponse synthesizeSpeechResult = client.SynthesizeSpeech(request);
                byte[] buffer = new byte[2 * 1024];
                int position = 0;
                int readBytes = 2048;

                using (FileStream fileStream = new FileStream(outputFileName, FileMode.Truncate))
                {
                    using (var audioStream = synthesizeSpeechResult.AudioStream)
                    {
                        while (audioStream.Read(buffer, 0, readBytes) > 0)
                        {
                            fileStream.Write(buffer, 0, readBytes);
                            position += readBytes + 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: " + e);
            }
        }

        /// <summary>
        /// Get Oxford Dictionary Consult Result
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>consult result</returns>
        public JValue GetDictionaryConsultResult(string word)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://{this.oxford_base_api}/entries/en/{word}");

            request.Headers["app_id"] = this.app_id;
            request.Headers["app_key"] = this.app_key;
            request.Method = "Get";
            JValue content = null;
            using (var response = request.GetResponse())
            {
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                content = new JValue(sr.ReadToEnd());
            }

            return content;
        }
    }
}