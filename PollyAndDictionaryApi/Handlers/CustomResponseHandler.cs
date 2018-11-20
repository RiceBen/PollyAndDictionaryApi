using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PollyAndDictionaryApi.Models;

namespace PollyAndDictionaryApi.Handlers
{
    public class CustomResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            try
            {
                return this.GenerateResponse(request, response);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private HttpResponseMessage GenerateResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;

            if (IsResponseValid(response) == false)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, "Invalid response.");
            }

            string errorMessage = null;
            object responseContent;
            if (response.TryGetContentValue(out responseContent))
            {
                HttpError httpError = responseContent as HttpError;
                if (httpError != null)
                {
                    errorMessage = httpError.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    responseContent = null;
                }
            }

            var responseModel = new ResponseModel()
            {
                Data = responseContent,
                Msg = errorMessage,
                Success = string.IsNullOrEmpty(errorMessage)
            };

            var result = request.CreateResponse(statusCode, responseModel);

            return result;
        }

        /// <summary>
        /// Is response valid or not
        /// </summary>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns>If response is valid return <c>true</c>, otherwise <c>false</c></returns>
        private bool IsResponseValid(HttpResponseMessage response)
        {
            if((response != null) && (response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                return true;
            }

            return false;
        }
    }
}