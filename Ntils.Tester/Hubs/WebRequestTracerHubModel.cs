using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Ntils.Hubs
{
    public class WebRequestTracerHubModel
    {
        public WebRequestTracerHubModel() { }

        public WebRequestTracerRequestModel Request { get; private set; }

        public WebRequestTracerResponseModel Response { get; private set; }

        public void SetRequest(HttpRequest request, string rawBody = null)
        {
            Request = new WebRequestTracerRequestModel
            {
                Headers = request.Headers?.ToDictionary(x => x.Key, x => (string)x.Value),
                Query = request.Query?.ToDictionary(c => c.Key, c => (string)c.Value),
                Form = request.HasFormContentType ? request.Form?.ToDictionary(c => c.Key, c => (string)c.Value) : null,
                Body = rawBody
            };
        }

        public void SetResponse(HttpResponse response, string rawBody = null)
        {
            Response = new WebRequestTracerResponseModel();
        }
    }
}