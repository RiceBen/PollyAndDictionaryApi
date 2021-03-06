﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PollyAndDictionaryApi.Models;

namespace PollyAndDictionaryApi.Services
{
    /// <summary>
    /// AWS Service
    /// </summary>
    public class AWSService : IDictionaryService
    {
        private string access_key_id;
        private string secret_access_key;
        private string app_id;
        private string app_key;
        private string oxford_base_api;
        private IAmazonPolly amazonPolly;

        /// <summary>
        /// Constructor of AWSService
        /// </summary>
        public AWSService()
        {
            this.access_key_id = ConfigurationManager.AppSettings["AWS_access_key_id"];
            this.secret_access_key = ConfigurationManager.AppSettings["AWS_secret_access_key"];
            this.app_id = ConfigurationManager.AppSettings["Oxford_app_id"];
            this.app_key = ConfigurationManager.AppSettings["Oxford_app_key"];
            this.oxford_base_api = ConfigurationManager.AppSettings["Oxford_base_api"];
        }

        /// <summary>
        /// Get AWS Amazon Polly Service
        /// </summary>
        public IAmazonPolly awsPollyService
        {
            get
            {
                if (amazonPolly == null)
                {
                    this.amazonPolly = new AmazonPollyClient(
                        this.access_key_id,
                        this.secret_access_key,
                        RegionEndpoint.USEast2);
                }

                return amazonPolly;
            }
        }

        /// <summary>
        /// Get Voice via 3rd Service
        /// Use AWS Polly Service
        /// </summary>
        /// <param name="word">Word</param>
        public void GetVoice(string word)
        {
            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest
            {
                LanguageCode = LanguageCode.EnUS,
                VoiceId = VoiceId.Matthew,
                Text = word,
                TextType = TextType.Text,
                OutputFormat = OutputFormat.Mp3
            };

            string outputFileName = $"{System.Web.HttpContext.Current.Server.MapPath("~")}\\speech.mp3";

            try
            {
                SynthesizeSpeechResponse synthesizeSpeechResult = this.awsPollyService.SynthesizeSpeech(request);
                byte[] buffer = new byte[2 * 1024];
                int position = 0;
                int readBytes = 1024;

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
        public List<Models.OxfordEntriesEntity> GetDictionaryConsultResult(string word)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://{this.oxford_base_api}/entries/en/{word}");

            request.Headers["app_id"] = this.app_id;
            request.Headers["app_key"] = this.app_key;
            request.Method = "Get";
            JObject content = null;
            try
            {
                using (var response = request.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    var sr = new StreamReader(stream).ReadToEnd();
                    content = JObject.Parse(sr);
                }

                var returnData = JsonConvert.DeserializeObject<List<Models.OxfordEntriesEntity>>(content["results"].ToString());

                return returnData;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        return new List<Models.OxfordEntriesEntity>();
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Voice via 3rd Service, asynchronous version.
        /// </summary>
        /// <param name="word">word</param>
        public async Task GetVoiceAsync(string word)
        {
            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest
            {
                LanguageCode = LanguageCode.EnUS,
                VoiceId = VoiceId.Matthew,
                Text = word,
                TextType = TextType.Text,
                OutputFormat = OutputFormat.Mp3
            };

            string outputFileName = $"{System.Web.HttpContext.Current.Server.MapPath("~")}\\speech.mp3";

            try
            {
                SynthesizeSpeechResponse synthesizeSpeechResult = await this.awsPollyService.SynthesizeSpeechAsync(request);
                byte[] buffer = new byte[2 * 1024];
                int position = 0;
                int readBytes = 1024;

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
        /// Get Dictionary Consult Result, asynchronous version.
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>consult result</returns>
        public async Task<List<OxfordEntriesEntity>> GetDictionaryConsultResultAsync(string word)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://{this.oxford_base_api}/entries/en/{word}");

            request.Headers["app_id"] = this.app_id;
            request.Headers["app_key"] = this.app_key;
            request.Method = "Get";
            JObject content = null;
            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    var stream = response.GetResponseStream();
                    var sr = new StreamReader(stream).ReadToEnd();
                    content = JObject.Parse(sr);
                }

                var returnData = JsonConvert.DeserializeObject<List<Models.OxfordEntriesEntity>>(content["results"].ToString());

                return returnData;
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        return new List<Models.OxfordEntriesEntity>();
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}