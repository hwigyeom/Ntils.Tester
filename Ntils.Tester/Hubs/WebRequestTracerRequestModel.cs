using System;
using System.Collections.Generic;
using System.Text;

namespace Ntils.Hubs
{
    public class WebRequestTracerRequestModel
    {
        public IDictionary<string, string> Headers { get; internal set; }

        public IDictionary<string, string> Query { get; internal set; }

        public IDictionary<string, string> Form { get; internal set; }

        public string Body { get; internal set; }

        public string Raw
        {
            get
            {
                var builder = new StringBuilder();

                foreach (var item in Headers)
                {
                    builder.AppendLine($"{item.Key}: {item.Value}");
                }

                builder.AppendLine(Environment.NewLine);
                builder.Append(Body);

                return builder.ToString();
            }
        }

        public override string ToString() => Raw;
    }
}