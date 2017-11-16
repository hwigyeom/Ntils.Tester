using System.Linq;
using System.Net;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ntils.Middlewares
{
    public class WebTraceModel
    {
        [JsonProperty(PropertyName = "request")]
        public WebTraceRequestModel Request { get; private set; }

        [JsonProperty(PropertyName = "response")]
        public WebTraceResponseModel Response { get; private set; }

        public void SetRequest(HttpRequest request, string rawBody = null)
        {
            Request = new WebTraceRequestModel
            {
                Method = request.Method.ToString(),
                Path = request.Path.Value,
                Url = request.GetUri().PathAndQuery,
                Protocol = request.Protocol,
                Scheme = request.Scheme,
                Host = request.Host.Value,
                Headers = request.Headers?.ToDictionary(x => x.Key, x => (string)x.Value),
                Query = request.Query?.ToDictionary(c => c.Key, c => (string)c.Value),
                Form = request.HasFormContentType ? request.Form?.ToDictionary(c => c.Key, c => (string)c.Value) : null,
                Body = rawBody
            };
        }

        public void SetResponse(HttpResponse response, string rawBody = null)
        {
            Response = new WebTraceResponseModel
            {
                Protocol = response.HttpContext.Request.Protocol,
                Status = response.StatusCode,
                StatusCode = ((HttpStatusCode)response.StatusCode).ToString(),
                ContentType = response.ContentType,
                Headers = response.Headers?.ToDictionary(x => x.Key, x => (string)x.Value),
                Body = rawBody
            };
        }
    }
}