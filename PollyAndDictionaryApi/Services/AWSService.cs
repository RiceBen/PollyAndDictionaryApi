using System;
using System.Configuration;
using System.IO;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;

namespace PollyAndDictionaryApi.Services
{
    public class AWSService
    {
        private string _access_key_id;
        private string _secret_access_key;

        public AWSService()
        {
            this._access_key_id = ConfigurationManager.AppSettings["AWS_access_key_id"];
            this._secret_access_key = ConfigurationManager.AppSettings["AWS_secret_access_key"];
        }

        public void GetVoice(string word)
        {
            var client = new AmazonPollyClient(
                this._access_key_id,
                this._secret_access_key,
                RegionEndpoint.USEast2);

            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest
            {
                LanguageCode = LanguageCode.EnUS,
                VoiceId = VoiceId.Matthew,
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
                            position += readBytes;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: " + e);
            }
        }
    }
}