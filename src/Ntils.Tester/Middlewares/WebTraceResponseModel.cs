using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ntils.Middlewares
{
    public class WebTraceResponseModel
    {
        [JsonProperty(PropertyName = "protocol")]
        public string Protocol { get; internal set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; internal set; }

        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; internal set; }

        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; internal set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; internal set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; internal set; }

        [JsonProperty(PropertyName = "raw")]
        public string Raw
        {
            get
            {
                var builder = new StringBuilder();

                builder.AppendLine($"{Protocol} {Status} {StatusCode}");

                builder.AppendLine($"Content-Type: {ContentType}");

                foreach (var item in Headers)
                {
                    builder.AppendLine($"{item.Key}: {item.Value}");
                }

                builder.AppendLine();
                builder.Append(Body);

                return builder.ToString();
            }
        }

        public override string ToString() => Raw;
    }
}