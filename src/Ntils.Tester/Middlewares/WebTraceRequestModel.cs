using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ntils.Middlewares
{
    public class WebTraceRequestModel
    {
        [JsonProperty(PropertyName = "method")]
        public string Method { get; internal set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; internal set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; internal set; }

        [JsonProperty(PropertyName = "protocol")]
        public string Protocol { get; internal set; }

        [JsonProperty(PropertyName = "scheme")]
        public string Scheme { get; internal set; }

        [JsonProperty(PropertyName = "host")]
        public string Host { get; internal set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; internal set; }

        [JsonProperty(PropertyName = "query")]
        public IDictionary<string, string> Query { get; internal set; }

        [JsonProperty(PropertyName = "form")]
        public IDictionary<string, string> Form { get; internal set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; internal set; }

        [JsonProperty(PropertyName = "raw")]
        public string Raw
        {
            get
            {
                var builder = new StringBuilder();

                builder.AppendLine($"{Method} {Url} {Protocol}");

                builder.AppendLine($"Host: {Host}");

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