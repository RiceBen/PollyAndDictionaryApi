using System;

namespace PollyAndDictionaryApi.Models
{
    /// <summary>
    /// Uniform response entity
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Is request without error
        /// </summary>
        public bool Success { get; set;}

        /// <summary>
        /// Error message
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public object Data { get; set; }
    }
}