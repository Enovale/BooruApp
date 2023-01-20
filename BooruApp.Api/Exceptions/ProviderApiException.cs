using System;
using System.Runtime.Serialization;

namespace BooruApp.Api.Exceptions
{
    public class ProviderApiException : Exception
    {
        public ProviderApiException()
        {
        }

        protected ProviderApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProviderApiException(string? message) : base(message)
        {
        }

        public ProviderApiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}